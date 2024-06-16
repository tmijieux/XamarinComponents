#addin nuget:?package=Cake.XCode&version=5.0.0
// #addin nuget:?package=Cake.Xamarin.Build&version=4.1.2
#addin nuget:?package=Cake.Xamarin&version=4.0.0
#addin nuget:?package=Cake.FileHelpers&version=3.2.1

var TARGET = Argument ("t", Argument ("target", "ci"));

var ANDROID_VERSION = "0.11.1";
var ANDROID_NUGET_VERSION = "0.11.1-alpha.1";

var IOS_VERSION = "1.6.2";
var IOS_NUGET_VERSION = "1.6.2-alpha.4";

var AAR_URL = $"https://repo1.maven.org/maven2/net/openid/appauth/{ANDROID_VERSION}/appauth-{ANDROID_VERSION}.aar";

var PODFILE = new List<string> {
	"platform :ios, '9.0'",
	"install! 'cocoapods', :integrate_targets => false",
	"target 'Xamarin' do",
	string.Format ("  pod 'AppAuth', '{0}'", IOS_VERSION),
	"end",
};

Task("externals-ios")
	.WithCriteria(IsRunningOnUnix())
	//.WithCriteria(!FileExists("./externals/ios/libAppAuth.a"))
	.Does(() => 
{
	EnsureDirectoryExists ("./externals/ios");

	FileWriteLines("./externals/ios/Podfile", PODFILE.ToArray());
	CocoaPodRepoUpdate();
	CocoaPodInstall("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });

	XCodeBuild(new XCodeBuildSettings {
                Clean=true,
		Project = "./externals/ios/Pods/Pods.xcodeproj",
		Scheme = "AppAuth",
                Archive = true,
                ArchivePath = "./externals/ios/archives/my_archive_device.xcarchive",
		Destination = new Dictionary<string,string> {
                  {"generic/platform","iOS"}
                },
		Configuration = "Release",
                SkipUnavailableActions = false,
                BuildSettings=  new Dictionary<string,string> {
                    {"SKIP_INSTALL","NO"}
                },
	});
	//XCodeBuild(new XCodeBuildSettings {
        //        Clean=true,
	//	Project = "./externals/ios/Pods/Pods.xcodeproj",
	//	Scheme = "AppAuth",
        //        Archive = true,
        //        ArchivePath = "./externals/ios/archives/my_archive_simulators_xamarin.xcarchive",
	//	Sdk = "iphonesimulator",
	//	Configuration = "Release",
        //        SkipUnavailableActions = false,
        //        BuildSettings=  new Dictionary<string,string> {
        //            {"SKIP_INSTALL","NO"},
        //            {"ONLY_ACTIVE_ARCH","NO"},
        //            {"ARCHS","x86_64"},
        //        },
	//});
	XCodeBuild(new XCodeBuildSettings {
                Clean=true,
		Project = "./externals/ios/Pods/Pods.xcodeproj",
		Scheme = "AppAuth",
                Archive = true,
                ArchivePath = "./externals/ios/archives/my_archive_simulators_net.xcarchive",
		Destination = new Dictionary<string,string> {
                  {"\"generic/platform","iOS Simulator\""}
                },
		Configuration = "Debug",
                SkipUnavailableActions = false,
                BuildSettings=  new Dictionary<string,string> {
                    {"SKIP_INSTALL","NO"},
                },
	});

        // https://developer.apple.com/documentation/xcode/creating-a-multi-platform-binary-framework-bundle
        // says: Avoid using tools such as lipo to combine architecture slices built for iOS and iOS Simulator into a single binary. 

        // var lipoExitCode = StartProcess("lipo", new ProcessSettings {
        //     Arguments = new ProcessArgumentBuilder()
        //         .Append("-create")
        //         .Append("./externals/ios/archives/my_archive_device.xcarchive/Products/usr/local/lib/libAppAuth.a")
        //         .Append("./externals/ios/archives/my_archive_simulators_xamarin.xcarchive/Products/usr/local/lib/libAppAuth.a")
        //         .Append("-output").Append("./externals/ios/libAppAuth.a")
        // });
        // if (lipoExitCode != 0) {
        //     throw new Exception($"lipo failed with exit code {lipoExitCode}");
        // }

        StartProcess("xcodebuild", new ProcessSettings {
            Arguments = new ProcessArgumentBuilder()
                .Append("-create-xcframework")
                .Append("-archive").Append("./externals/ios/archives/my_archive_device.xcarchive")
                .Append("-library").Append("libAppAuth.a")
                .Append("-archive").Append("./externals/ios/archives/my_archive_simulators_net.xcarchive")
                .Append("-library").Append("libAppAuth.a")
                .Append("-output").Append("./externals/ios/frameworks/AppAuth.xcframework")
        });


	XmlPoke("./iOS/source/OpenId.AppAuth.iOS/OpenId.AppAuth.iOS.csproj", "/Project/PropertyGroup/PackageVersion", IOS_NUGET_VERSION);
	XmlPoke("./iOS/source/OpenId.AppAuth.iOS/OpenId.AppAuth.NET.csproj", "/Project/PropertyGroup/PackageVersion", IOS_NUGET_VERSION);
});

Task("externals-android")
	.WithCriteria(!FileExists("./externals/android/appauth.aar"))
	.Does(() => 
{
	EnsureDirectoryExists("./externals/android");

	DownloadFile(AAR_URL, "./externals/android/appauth.aar");

	XmlPoke("./Android/source/OpenId.AppAuth.Android/OpenId.AppAuth.Android.csproj", "/Project/PropertyGroup/PackageVersion", ANDROID_NUGET_VERSION);
});

Task("libs-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("externals-ios")
	.Does(() =>
{
	MSBuild("./iOS/source/OpenId.AppAuth.iOS.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Rebuild");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/libs-ios.binlog"
		};
	});
	DotNetBuild("./iOS/source/OpenId.AppAuth.iOS/OpenId.AppAuth.NET.csproj", new DotNetBuildSettings {
		Configuration = "Release",
            }
	);
});

Task("libs-android")
	.IsDependentOn("externals-android")
	.Does(() =>
{
	MSBuild("./Android/source/OpenId.AppAuth.Android.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Rebuild");
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/libs-android.binlog"
		};
	});
});

Task("nuget-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("libs-ios")
	.Does(() =>
{
	MSBuild("./iOS/source/OpenId.AppAuth.iOS.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/nuget-ios.binlog"
		};
	});

        var settings = new DotNetMSBuildSettings();
        settings.SetConfiguration("Release");
        settings.Targets.Clear();
        settings.Targets.Add("Pack");

	DotNetBuild("./iOS/source/OpenId.AppAuth.iOS/OpenId.AppAuth.NET.csproj", new DotNetBuildSettings{
            Configuration="Release",
            MSBuildSettings=settings,

        });
});

Task("nuget-android")
	.IsDependentOn("libs-android")
	.Does(() =>
{
	MSBuild("./Android/source/OpenId.AppAuth.Android.sln", c => {
		c.Configuration = "Release";
		c.Targets.Clear();
		c.Targets.Add("Pack");
		c.Properties.Add("PackageOutputPath", new [] { MakeAbsolute(new FilePath("./output")).FullPath });
		c.Properties.Add("PackageRequireLicenseAcceptance", new [] { "true" });
		c.Properties.Add("DesignTimeBuild", new [] { "false" });
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/nuget-android.binlog"
		};
	});
});

Task("samples-ios")
	.WithCriteria(IsRunningOnUnix())
	.IsDependentOn("nuget-ios")
	.Does(() =>
{
	MSBuild("./iOS/samples/OpenIdAuthSampleiOS.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Build");
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/samples-ios.binlog"
		};
	});
});

Task("samples-android")
	.IsDependentOn("nuget-android")
	.Does(() =>
{
	MSBuild("./Android/samples/OpenIdAuthSampleAndroid.sln", c => {
		c.Configuration = "Release";
		c.Restore = true;
		c.Targets.Clear();
		c.Targets.Add("Build");
		c.BinaryLogger = new MSBuildBinaryLogSettings {
			Enabled = true,
			FileName = "./output/samples-android.binlog"
		};
	});
});

Task("externals")
	.IsDependentOn("externals-ios")
	.IsDependentOn("externals-android");

Task("libs")
	.IsDependentOn("libs-ios")
	.IsDependentOn("libs-android");

Task("nuget")
	.IsDependentOn("nuget-ios")
	.IsDependentOn("nuget-android");

Task("samples")
	.IsDependentOn("samples-ios")
	.IsDependentOn("samples-android");

Task("clean")
	.Does(() => 
{
	if(DirectoryExists ("./externals/android"))
		DeleteDirectory ("./externals/android", new DeleteDirectorySettings {
		Recursive = true,
		Force = true
	});

	if(DirectoryExists ("./externals/ios"))
		DeleteDirectory ("./externals/ios", new DeleteDirectorySettings {
		Recursive = true,
		Force = true
	});
});

Task("ci")
	.IsDependentOn("nuget");

RunTarget (TARGET);
