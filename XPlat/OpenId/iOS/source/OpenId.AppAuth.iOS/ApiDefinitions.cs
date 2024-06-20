
using System;
using CoreFoundation;
using Foundation;
using UIKit;

using ObjCRuntime;

#if !NET
using NativeHandle = System.IntPtr;
#endif

namespace OpenId.AppAuth
{

    //       [Static]
    //       //[Verify (ConstantsInterfaceAssociation)]
    //       partial interface Constants
    //       {
    //          // extern double AppAuthVersionNumber;
    //          [Field ("AppAuthVersionNumber", "__Internal")]
    //          double AppAuthVersionNumber { get; }
    //
    //          // extern const unsigned char[] AppAuthVersionString;
    //          [Field ("AppAuthVersionString", "__Internal")]
    //          byte[] AppAuthVersionString { get; }
    //       }

    // typedef void (^OIDAuthStateAction)(NSString * _Nullable, NSString * _Nullable, NSError * _Nullable);
    delegate void AuthStateAction ([NullAllowed] string arg0, [NullAllowed] string arg1, [NullAllowed] NSError arg2);

    // typedef void (^OIDAuthStateAuthorizationCallback)(OIDAuthState * _Nullable, NSError * _Nullable);
    delegate void AuthStateAuthorizationCallback ([NullAllowed] AuthState arg0, [NullAllowed] NSError arg1);

    // @interface OIDAuthState : NSObject <NSSecureCoding>
    [BaseType (typeof(NSObject), Name="OIDAuthState")]
    [DisableDefaultCtor]
    interface AuthState : INSSecureCoding
    {
        // @property (readonly, nonatomic) NSString * _Nullable refreshToken;
        [NullAllowed, Export ("refreshToken")]
        string RefreshToken { get; }

        // @property (readonly, nonatomic) NSString * _Nullable scope;
        [NullAllowed, Export ("scope")]
        string Scope { get; }

        // @property (readonly, nonatomic) OIDAuthorizationResponse * _Nonnull lastAuthorizationResponse;
        [Export ("lastAuthorizationResponse")]
        AuthorizationResponse LastAuthorizationResponse { get; }

        // @property (readonly, nonatomic) OIDTokenResponse * _Nullable lastTokenResponse;
        [NullAllowed, Export ("lastTokenResponse")]
        TokenResponse LastTokenResponse { get; }

        // @property (readonly, nonatomic) OIDRegistrationResponse * _Nullable lastRegistrationResponse;
        [NullAllowed, Export ("lastRegistrationResponse")]
        RegistrationResponse LastRegistrationResponse { get; }

        // @property (readonly, nonatomic) NSError * _Nullable authorizationError;
        [NullAllowed, Export ("authorizationError")]
        NSError AuthorizationError { get; }

        // @property (readonly, nonatomic) BOOL isAuthorized;
        [Export ("isAuthorized")]
        bool IsAuthorized { get; }

        [Wrap ("WeakStateChangeDelegate")]
        [NullAllowed]
        AuthStateChangeDelegate StateChangeDelegate { get; set; }

        // @property (nonatomic, weak) id<OIDAuthStateChangeDelegate> _Nullable stateChangeDelegate;
        [NullAllowed, Export ("stateChangeDelegate", ArgumentSemantic.Weak)]
        NSObject WeakStateChangeDelegate { get; set; }

        [Wrap ("WeakErrorDelegate")]
        [NullAllowed]
        AuthStateErrorDelegate ErrorDelegate { get; set; }

        // @property (nonatomic, weak) id<OIDAuthStateErrorDelegate> _Nullable errorDelegate;
        [NullAllowed, Export ("errorDelegate", ArgumentSemantic.Weak)]
        NSObject WeakErrorDelegate { get; set; }

        // +(id<OIDExternalUserAgentSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest presentingViewController:(UIViewController * _Nonnull)presentingViewController callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback;
        // iOS only !!!
        [Static]
        [Export ("authStateByPresentingAuthorizationRequest:presentingViewController:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest(AuthorizationRequest authorizationRequest, UIViewController presentingViewController, AuthStateAuthorizationCallback callback);

        // +(id<OIDExternalUserAgentSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest presentingViewController:(UIViewController * _Nonnull)presentingViewController prefersEphemeralSession:(BOOL)prefersEphemeralSession callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback __attribute__((availability(ios, introduced=13)));
        //[iOS (13,0)]
        [Static]
        [Export ("authStateByPresentingAuthorizationRequest:presentingViewController:prefersEphemeralSession:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest(AuthorizationRequest authorizationRequest, UIViewController presentingViewController, bool prefersEphemeralSession, AuthStateAuthorizationCallback callback);

        // +(id<OIDExternalUserAgentSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback __attribute__((availability(ios, introduced=11))) __attribute__((availability(maccatalyst, unavailable))) __attribute__((deprecated("This method will not work on iOS 13. Use authStateByPresentingAuthorizationRequest:presentingViewController:callback:")));
        //[NoMacCatalyst, iOS (11,0)]
        [NoMacCatalyst]
        [Static]
        [Export ("authStateByPresentingAuthorizationRequest:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest(AuthStateAuthorizationCallback callback);


        // +(id<OIDExternalUserAgentSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest externalUserAgent:(id<OIDExternalUserAgent> _Nonnull)externalUserAgent callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback;
        [Static]
        [Export ("authStateByPresentingAuthorizationRequest:externalUserAgent:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest (AuthorizationRequest authorizationRequest, IExternalUserAgent externalUserAgent, AuthStateAuthorizationCallback callback);

        // -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nonnull)authorizationResponse;
        [Export ("initWithAuthorizationResponse:")]
        NativeHandle Constructor (AuthorizationResponse authorizationResponse);

        // -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nonnull)authorizationResponse tokenResponse:(OIDTokenResponse * _Nullable)tokenResponse;
        [Export ("initWithAuthorizationResponse:tokenResponse:")]
        NativeHandle Constructor (AuthorizationResponse authorizationResponse, [NullAllowed] TokenResponse tokenResponse);

        // -(instancetype _Nonnull)initWithRegistrationResponse:(OIDRegistrationResponse * _Nonnull)registrationResponse;
        [Export ("initWithRegistrationResponse:")]
        NativeHandle Constructor (RegistrationResponse registrationResponse);

        // -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nullable)authorizationResponse tokenResponse:(OIDTokenResponse * _Nullable)tokenResponse registrationResponse:(OIDRegistrationResponse * _Nullable)registrationResponse __attribute__((objc_designated_initializer));
        [Export ("initWithAuthorizationResponse:tokenResponse:registrationResponse:")]
        [DesignatedInitializer]
        NativeHandle Constructor ([NullAllowed] AuthorizationResponse authorizationResponse, [NullAllowed] TokenResponse tokenResponse, [NullAllowed] RegistrationResponse registrationResponse);

        // -(void)updateWithAuthorizationResponse:(OIDAuthorizationResponse * _Nullable)authorizationResponse error:(NSError * _Nullable)error;
        [Export ("updateWithAuthorizationResponse:error:")]
        void UpdateWithAuthorizationResponse ([NullAllowed] AuthorizationResponse authorizationResponse, [NullAllowed] NSError error);

        // -(void)updateWithTokenResponse:(OIDTokenResponse * _Nullable)tokenResponse error:(NSError * _Nullable)error;
        [Export ("updateWithTokenResponse:error:")]
        void UpdateWithTokenResponse ([NullAllowed] TokenResponse tokenResponse, [NullAllowed] NSError error);

        // -(void)updateWithRegistrationResponse:(OIDRegistrationResponse * _Nullable)registrationResponse;
        [Export ("updateWithRegistrationResponse:")]
        void UpdateWithRegistrationResponse ([NullAllowed] RegistrationResponse registrationResponse);

        // -(void)updateWithAuthorizationError:(NSError * _Nonnull)authorizationError;
        [Export ("updateWithAuthorizationError:")]
        void UpdateWithAuthorizationError (NSError authorizationError);

        // -(void)performActionWithFreshTokens:(OIDAuthStateAction _Nonnull)action;
        [Export ("performActionWithFreshTokens:")]
        void PerformActionWithFreshTokens (AuthStateAction action);

        // -(void)performActionWithFreshTokens:(OIDAuthStateAction _Nonnull)action additionalRefreshParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("performActionWithFreshTokens:additionalRefreshParameters:")]
        void PerformActionWithFreshTokens (AuthStateAction action, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(void)performActionWithFreshTokens:(OIDAuthStateAction _Nonnull)action additionalRefreshParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters dispatchQueue:(dispatch_queue_t _Nonnull)dispatchQueue;
        [Export ("performActionWithFreshTokens:additionalRefreshParameters:dispatchQueue:")]
        void PerformActionWithFreshTokens (AuthStateAction action, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters, DispatchQueue dispatchQueue);

        // -(void)setNeedsTokenRefresh;
        [Export ("setNeedsTokenRefresh")]
        void SetNeedsTokenRefresh ();

        // -(OIDTokenRequest * _Nullable)tokenRefreshRequest;
        [NullAllowed, Export ("tokenRefreshRequest")]
        TokenRequest TokenRefreshRequest { get; }

        // -(OIDTokenRequest * _Nullable)tokenRefreshRequestWithAdditionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("tokenRefreshRequestWithAdditionalParameters:")]
        [return: NullAllowed]
        TokenRequest TokenRefreshRequestWithAdditionalParameters ([NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(OIDTokenRequest * _Nullable)tokenRefreshRequestWithAdditionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters additionalHeaders:(NSDictionary<NSString *,NSString *> * _Nullable)additionalHeaders;
        [Export ("tokenRefreshRequestWithAdditionalParameters:additionalHeaders:")]
        [return: NullAllowed]
        TokenRequest TokenRefreshRequestWithAdditionalParameters ([NullAllowed] NSDictionary<NSString, NSString> additionalParameters, [NullAllowed] NSDictionary<NSString, NSString> additionalHeaders);

        // -(OIDTokenRequest * _Nullable)tokenRefreshRequestWithAdditionalHeaders:(NSDictionary<NSString *,NSString *> * _Nullable)additionalHeaders;
        [Export ("tokenRefreshRequestWithAdditionalHeaders:")]
        [return: NullAllowed]
        TokenRequest TokenRefreshRequestWithAdditionalHeaders ([NullAllowed] NSDictionary<NSString, NSString> additionalHeaders);
    }

    // @protocol OIDAuthStateChangeDelegate <NSObject>
    [Protocol(Name="OIDAuthStateChangeDelegate"), Model]
    [BaseType (typeof(NSObject), Name="OIDAuthStateChangeDelegate")]
    interface AuthStateChangeDelegate
    {
        // @required -(void)didChangeState:(OIDAuthState * _Nonnull)state;
        [Abstract]
        [Export ("didChangeState:")]
        void DidChangeState (AuthState state);
    }

    // @protocol OIDAuthStateErrorDelegate <NSObject>
    [Protocol(Name="OIDAuthStateErrorDelegate"), Model]
    [BaseType (typeof(NSObject), Name="OIDAuthStateErrorDelegate")]
    interface AuthStateErrorDelegate
    {
        // @required -(void)authState:(OIDAuthState * _Nonnull)state didEncounterAuthorizationError:(NSError * _Nonnull)error;
        [Abstract]
        [Export ("authState:didEncounterAuthorizationError:")]
        void DidEncounterAuthorizationError (AuthState state, NSError error);

        // @optional -(void)authState:(OIDAuthState * _Nonnull)state didEncounterTransientError:(NSError * _Nonnull)error;
        [Export ("authState:didEncounterTransientError:")]
        void DidEncounterTransientError (AuthState state, NSError error);
    }

    // @protocol OIDExternalUserAgentRequest
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */

    [Protocol(Name="OIDExternalUserAgentRequest")]
    interface ExternalUserAgentRequest
    {
        // @required -(NSURL *)externalUserAgentRequestURL;
        [Abstract]
        [Export ("externalUserAgentRequestURL")]
        NSUrl ExternalUserAgentRequestURL { get; }

        // @required -(NSString *)redirectScheme;
        [Abstract]
        [Export ("redirectScheme")]
        string RedirectScheme { get; }
    }
    interface IExternalUserAgentRequest {}


    [Static]
    partial interface ResponseType
    {
        // extern NSString *const OIDResponseTypeCode;
        [Field ("OIDResponseTypeCode", "__Internal")]
        NSString Code { get; }

        // extern NSString *const OIDResponseTypeToken;
        [Field ("OIDResponseTypeToken", "__Internal")]
        NSString Token { get; }

        // extern NSString *const OIDResponseTypeIDToken;
        [Field ("OIDResponseTypeIDToken", "__Internal")]
        NSString IDToken { get; }
    }

    [Static]
    partial interface Scope
    {
        // extern NSString *const OIDScopeOpenID;
        [Field ("OIDScopeOpenID", "__Internal")]
        NSString OpenID { get; }

        // extern NSString *const OIDScopeProfile;
        [Field ("OIDScopeProfile", "__Internal")]
        NSString Profile { get; }

        // extern NSString *const OIDScopeEmail;
        [Field ("OIDScopeEmail", "__Internal")]
        NSString Email { get; }

        // extern NSString *const OIDScopeAddress;
        [Field ("OIDScopeAddress", "__Internal")]
        NSString Address { get; }

        // extern NSString *const OIDScopePhone;
        [Field ("OIDScopePhone", "__Internal")]
        NSString Phone { get; }
    }

    // @interface OIDAuthorizationRequest : NSObject <NSCopying, NSSecureCoding, OIDExternalUserAgentRequest>
    [BaseType (typeof(NSObject), Name="OIDAuthorizationRequest")]
    [DisableDefaultCtor]
    interface AuthorizationRequest : INSCopying, INSSecureCoding, ExternalUserAgentRequest
    {
        // extern NSString *const _Nonnull OIDOAuthorizationRequestCodeChallengeMethodS256;
        [Static]
        [Field ("OIDOAuthorizationRequestCodeChallengeMethodS256", "__Internal")]
        NSString CodeChallengeMethodS256 { get; }

        // @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
        [Export ("configuration")]
        ServiceConfiguration Configuration { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull responseType;
        [Export ("responseType")]
        string ResponseType { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull clientID;
        [Export ("clientID")]
        string ClientID { get; }

        // @property (readonly, nonatomic) NSString * _Nullable clientSecret;
        [NullAllowed, Export ("clientSecret")]
        string ClientSecret { get; }

        // @property (readonly, nonatomic) NSString * _Nullable scope;
        [NullAllowed, Export ("scope")]
        string Scope { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable redirectURL;
        [NullAllowed, Export ("redirectURL")]
        NSUrl RedirectURL { get; }

        // @property (readonly, nonatomic) NSString * _Nullable state;
        [NullAllowed, Export ("state")]
        string State { get; }

        // @property (readonly, nonatomic) NSString * _Nullable nonce;
        [NullAllowed, Export ("nonce")]
        string Nonce { get; }

        // @property (readonly, nonatomic) NSString * _Nullable codeVerifier;
        [NullAllowed, Export ("codeVerifier")]
        string CodeVerifier { get; }

        // @property (readonly, nonatomic) NSString * _Nullable codeChallenge;
        [NullAllowed, Export ("codeChallenge")]
        string CodeChallenge { get; }

        // @property (readonly, nonatomic) NSString * _Nullable codeChallengeMethod;
        [NullAllowed, Export ("codeChallengeMethod")]
        string CodeChallengeMethod { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSString> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID scopes:(NSArray<NSString *> * _Nullable)scopes redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("initWithConfiguration:clientId:scopes:redirectURL:responseType:additionalParameters:")]
        NativeHandle Constructor (ServiceConfiguration configuration, string clientID, [NullAllowed] string[] scopes, NSUrl redirectURL, string responseType, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID scopes:(NSArray<NSString *> * _Nullable)scopes redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType nonce:(NSString * _Nullable)nonce additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("initWithConfiguration:clientId:scopes:redirectURL:responseType:nonce:additionalParameters:")]
        NativeHandle Constructor (ServiceConfiguration configuration, string clientID, [NullAllowed] string[] scopes, NSUrl redirectURL, string responseType, [NullAllowed] string nonce, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scopes:(NSArray<NSString *> * _Nullable)scopes redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("initWithConfiguration:clientId:clientSecret:scopes:redirectURL:responseType:additionalParameters:")]
        NativeHandle Constructor (ServiceConfiguration configuration, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string[] scopes, NSUrl redirectURL, string responseType, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scope:(NSString * _Nullable)scope redirectURL:(NSURL * _Nullable)redirectURL responseType:(NSString * _Nonnull)responseType state:(NSString * _Nullable)state nonce:(NSString * _Nullable)nonce codeVerifier:(NSString * _Nullable)codeVerifier codeChallenge:(NSString * _Nullable)codeChallenge codeChallengeMethod:(NSString * _Nullable)codeChallengeMethod additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
        [Export ("initWithConfiguration:clientId:clientSecret:scope:redirectURL:responseType:state:nonce:codeVerifier:codeChallenge:codeChallengeMethod:additionalParameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (ServiceConfiguration configuration, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string scope, [NullAllowed] NSUrl redirectURL, string responseType, [NullAllowed] string state, [NullAllowed] string nonce, [NullAllowed] string codeVerifier, [NullAllowed] string codeChallenge, [NullAllowed] string codeChallengeMethod, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(NSURL * _Nonnull)authorizationRequestURL;
        [Export ("authorizationRequestURL")]
        NSUrl AuthorizationRequestURL { get; }

        // +(NSString * _Nullable)generateState;
        [Static]
        [NullAllowed, Export ("generateState")]
        string GenerateState { get; }

        // +(NSString * _Nullable)generateCodeVerifier;
        [Static]
        [NullAllowed, Export ("generateCodeVerifier")]
        string GenerateCodeVerifier { get; }

        // +(NSString * _Nullable)codeChallengeS256ForVerifier:(NSString * _Nullable)codeVerifier;
        [Static]
        [Export ("codeChallengeS256ForVerifier:")]
        [return: NullAllowed]
        string CodeChallengeS256ForVerifier ([NullAllowed] string codeVerifier);
    }

    // @interface OIDAuthorizationResponse : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject), Name="OIDAuthorizationResponse")]
    [DisableDefaultCtor]
    interface AuthorizationResponse : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) AuthorizationRequest * _Nonnull request;
        [Export ("request")]
        AuthorizationRequest Request { get; }

        // @property (readonly, nonatomic) NSString * _Nullable authorizationCode;
        [NullAllowed, Export ("authorizationCode")]
        string AuthorizationCode { get; }

        // @property (readonly, nonatomic) NSString * _Nullable state;
        [NullAllowed, Export ("state")]
        string State { get; }

        // @property (readonly, nonatomic) NSString * _Nullable accessToken;
        [NullAllowed, Export ("accessToken")]
        string AccessToken { get; }

        // @property (readonly, nonatomic) NSDate * _Nullable accessTokenExpirationDate;
        [NullAllowed, Export ("accessTokenExpirationDate")]
        NSDate AccessTokenExpirationDate { get; }

        // @property (readonly, nonatomic) NSString * _Nullable tokenType;
        [NullAllowed, Export ("tokenType")]
        string TokenType { get; }

        // @property (readonly, nonatomic) NSString * _Nullable idToken;
        [NullAllowed, Export ("idToken")]
        string IdToken { get; }

        // @property (readonly, nonatomic) NSString * _Nullable scope;
        [NullAllowed, Export ("scope")]
        string Scope { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, INSCopying> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithRequest:(OIDAuthorizationRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
        [Export ("initWithRequest:parameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (AuthorizationRequest request, NSDictionary<NSString, INSCopying> parameters);

        // -(OIDTokenRequest * _Nullable)tokenExchangeRequest;
        [NullAllowed, Export ("tokenExchangeRequest")]
        TokenRequest TokenExchangeRequest { get; }

        // -(OIDTokenRequest * _Nullable)tokenExchangeRequestWithAdditionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters additionalHeaders:(NSDictionary<NSString *,NSString *> * _Nullable)additionalHeaders;
        [Export ("tokenExchangeRequestWithAdditionalParameters:additionalHeaders:")]
        [return: NullAllowed]
        TokenRequest TokenExchangeRequestWithAdditionalParameters ([NullAllowed] NSDictionary<NSString, NSString> additionalParameters, [NullAllowed] NSDictionary<NSString, NSString> additionalHeaders);
    }

    // typedef void (^OIDDiscoveryCallback)(OIDServiceConfiguration * _Nullable, NSError * _Nullable);
    delegate void DiscoveryCallback ([NullAllowed] ServiceConfiguration arg0, [NullAllowed] NSError arg1);

    // typedef void (^OIDAuthorizationCallback)(OIDAuthorizationResponse * _Nullable, NSError * _Nullable);
    delegate void AuthorizationCallback ([NullAllowed] AuthorizationResponse arg0, [NullAllowed] NSError arg1);

    // typedef void (^OIDEndSessionCallback)(OIDEndSessionResponse * _Nullable, NSError * _Nullable);
    delegate void EndSessionCallback ([NullAllowed] EndSessionResponse arg0, [NullAllowed] NSError arg1);

    // typedef void (^OIDTokenCallback)(OIDTokenResponse * _Nullable, NSError * _Nullable);
    delegate void TokenCallback ([NullAllowed] TokenResponse arg0, [NullAllowed] NSError arg1);

    // typedef void (^OIDRegistrationCompletion)(OIDRegistrationResponse * _Nullable, NSError * _Nullable);
    delegate void RegistrationCompletion ([NullAllowed] RegistrationResponse arg0, [NullAllowed] NSError arg1);

    // @interface OIDAuthorizationService : NSObject
    [BaseType (typeof(NSObject), Name="OIDAuthorizationService")]
    [DisableDefaultCtor]
    interface AuthorizationService
    {
        // @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
        [Export ("configuration")]
        ServiceConfiguration Configuration { get; }

        // +(void)discoverServiceConfigurationForIssuer:(NSURL * _Nonnull)issuerURL completion:(OIDDiscoveryCallback _Nonnull)completion;
        [Static]
        [Async]
        [Export ("discoverServiceConfigurationForIssuer:completion:")]
        void DiscoverServiceConfigurationForIssuer (NSUrl issuerURL, DiscoveryCallback completion);

        // +(void)discoverServiceConfigurationForDiscoveryURL:(NSURL * _Nonnull)discoveryURL completion:(OIDDiscoveryCallback _Nonnull)completion;
        [Static]
        [Async]
        [Export ("discoverServiceConfigurationForDiscoveryURL:completion:")]
        void DiscoverServiceConfigurationForDiscoveryURL (NSUrl discoveryURL, DiscoveryCallback completion);

        // +(id<OIDExternalUserAgentSession> _Nonnull)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request externalUserAgent:(id<OIDExternalUserAgent> _Nonnull)externalUserAgent callback:(OIDAuthorizationCallback _Nonnull)callback;
        [Static]
        [Export ("presentAuthorizationRequest:externalUserAgent:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest (AuthorizationRequest request, IExternalUserAgent externalUserAgent, AuthorizationCallback callback);

        // +(id<OIDExternalUserAgentSession> _Nonnull)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request presentingViewController:(UIViewController * _Nonnull)presentingViewController callback:(OIDAuthorizationCallback _Nonnull)callback;
        [Static]
        [Export ("presentAuthorizationRequest:presentingViewController:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest (AuthorizationRequest request, UIViewController presentingViewController, AuthorizationCallback callback);

        // +(id<OIDExternalUserAgentSession> _Nonnull)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request presentingViewController:(UIViewController * _Nonnull)presentingViewController prefersEphemeralSession:(BOOL)prefersEphemeralSession callback:(OIDAuthorizationCallback _Nonnull)callback __attribute__((availability(ios, introduced=13)));
        //[iOS (13,0)]
        [Static]
        [Export ("presentAuthorizationRequest:presentingViewController:prefersEphemeralSession:callback:")]
        IExternalUserAgentSession PresentAuthorizationRequest (AuthorizationRequest request, UIViewController presentingViewController, bool prefersEphemeralSession, AuthorizationCallback callback);

        // +(id<OIDExternalUserAgentSession> _Nonnull)presentEndSessionRequest:(OIDEndSessionRequest * _Nonnull)request externalUserAgent:(id<OIDExternalUserAgent> _Nonnull)externalUserAgent callback:(OIDEndSessionCallback _Nonnull)callback;
        [Static]
        [Export ("presentEndSessionRequest:externalUserAgent:callback:")]
        IExternalUserAgentSession PresentEndSessionRequest (EndSessionRequest request, IExternalUserAgent externalUserAgent, EndSessionCallback callback);


        // +(void)performTokenRequest:(OIDTokenRequest * _Nonnull)request callback:(OIDTokenCallback _Nonnull)callback;
        [Static]
        [Async]
        [Export ("performTokenRequest:callback:")]
        void PerformTokenRequest (TokenRequest request, TokenCallback callback);

        // +(void)performTokenRequest:(OIDTokenRequest * _Nonnull)request originalAuthorizationResponse:(OIDAuthorizationResponse * _Nullable)authorizationResponse callback:(OIDTokenCallback _Nonnull)callback;
        [Static]
        [Async]
        [Export ("performTokenRequest:originalAuthorizationResponse:callback:")]
        void PerformTokenRequest (TokenRequest request, [NullAllowed] AuthorizationResponse authorizationResponse, TokenCallback callback);

        // +(void)performRegistrationRequest:(OIDRegistrationRequest * _Nonnull)request completion:(OIDRegistrationCompletion _Nonnull)completion;
        [Static]
        [Async]
        [Export ("performRegistrationRequest:completion:")]
        void PerformRegistrationRequest (RegistrationRequest request, RegistrationCompletion completion);
    }

    [Static]
    partial interface Error
    {
        // extern NSString *const _Nonnull OIDGeneralErrorDomain;
        [Field ("OIDGeneralErrorDomain", "__Internal")]
        NSString GeneralErrorDomain { get; }

        // extern NSString *const _Nonnull OIDOAuthAuthorizationErrorDomain;
        [Field ("OIDOAuthAuthorizationErrorDomain", "__Internal")]
        NSString OAuthAuthorizationErrorDomain { get; }

        // extern NSString *const _Nonnull OIDOAuthTokenErrorDomain;
        [Field ("OIDOAuthTokenErrorDomain", "__Internal")]
        NSString OAuthTokenErrorDomain { get; }

        // extern NSString *const _Nonnull OIDOAuthRegistrationErrorDomain;
        [Field ("OIDOAuthRegistrationErrorDomain", "__Internal")]
        NSString OAuthRegistrationErrorDomain { get; }

        // extern NSString *const _Nonnull OIDResourceServerAuthorizationErrorDomain;
        [Field ("OIDResourceServerAuthorizationErrorDomain", "__Internal")]
        NSString ResourceServerAuthorizationErrorDomain { get; }

        // extern NSString *const _Nonnull OIDHTTPErrorDomain;
        [Field ("OIDHTTPErrorDomain", "__Internal")]
        NSString HTTPErrorDomain { get; }

        // extern NSString *const _Nonnull OIDOAuthErrorResponseErrorKey;
        [Field ("OIDOAuthErrorResponseErrorKey", "__Internal")]
        NSString OAuthErrorResponseErrorKey { get; }

        // extern NSString *const _Nonnull OIDOAuthErrorFieldError;
        [Field ("OIDOAuthErrorFieldError", "__Internal")]
        NSString OAuthErrorFieldError { get; }

        // extern NSString *const _Nonnull OIDOAuthErrorFieldErrorDescription;
        [Field ("OIDOAuthErrorFieldErrorDescription", "__Internal")]
        NSString OAuthErrorFieldErrorDescription { get; }

        // extern NSString *const _Nonnull OIDOAuthErrorFieldErrorURI;
        [Field ("OIDOAuthErrorFieldErrorURI", "__Internal")]
        NSString OAuthErrorFieldErrorURI { get; }

        // extern NSString *const _Nonnull OIDOAuthExceptionInvalidAuthorizationFlow;
        [Field ("OIDOAuthExceptionInvalidAuthorizationFlow", "__Internal")]
        NSString OAuthExceptionInvalidAuthorizationFlow { get; }

        // extern NSString *const _Nonnull OIDOAuthExceptionInvalidTokenRequestNullRedirectURL;
        [Field ("OIDOAuthExceptionInvalidTokenRequestNullRedirectURL", "__Internal")]
        NSString OAuthExceptionInvalidTokenRequestNullRedirectURL { get; }
    }

    // @interface OIDErrorUtilities : NSObject
    [BaseType (typeof(NSObject), Name="OIDErrorUtilities")]
    interface ErrorUtilities
    {
        // +(NSError * _Nonnull)errorWithCode:(OIDErrorCode)code underlyingError:(NSError * _Nullable)underlyingError description:(NSString * _Nullable)description;
        [Static]
        [Export ("errorWithCode:underlyingError:description:")]
        NSError ErrorWithCode (ErrorCode code, [NullAllowed] NSError underlyingError, [NullAllowed] string description);

        // +(NSError * _Nonnull)OAuthErrorWithDomain:(NSString * _Nonnull)OAuthErrorDomain OAuthResponse:(NSDictionary * _Nonnull)errorResponse underlyingError:(NSError * _Nullable)underlyingError;
        [Static]
        [Export ("OAuthErrorWithDomain:OAuthResponse:underlyingError:")]
        NSError OAuthErrorWithDomain (string OAuthErrorDomain, NSDictionary errorResponse, [NullAllowed] NSError underlyingError);

        // +(NSError * _Nonnull)resourceServerAuthorizationErrorWithCode:(NSInteger)code errorResponse:(NSDictionary * _Nullable)errorResponse underlyingError:(NSError * _Nullable)underlyingError;
        [Static]
        [Export ("resourceServerAuthorizationErrorWithCode:errorResponse:underlyingError:")]
        NSError ResourceServerAuthorizationErrorWithCode (nint code, [NullAllowed] NSDictionary errorResponse, [NullAllowed] NSError underlyingError);

        // +(NSError * _Nonnull)HTTPErrorWithHTTPResponse:(NSHTTPURLResponse * _Nonnull)HTTPURLResponse data:(NSData * _Nullable)data;
        [Static]
        [Export ("HTTPErrorWithHTTPResponse:data:")]
        NSError HTTPErrorWithHTTPResponse (NSHttpUrlResponse HTTPURLResponse, [NullAllowed] NSData data);

        // +(void)raiseException:(NSString * _Nonnull)name;
        [Static]
        [Export ("raiseException:")]
        void RaiseException (string name);

        // +(void)raiseException:(NSString * _Nonnull)name message:(NSString * _Nonnull)message;
        [Static]
        [Export ("raiseException:message:")]
        void RaiseException (string name, string message);

        // +(OIDErrorCodeOAuth)OAuthErrorCodeFromString:(NSString * _Nonnull)errorCode;
        [Static]
        [Export ("OAuthErrorCodeFromString:")]
        ErrorCodeOAuth OAuthErrorCodeFromString (string errorCode);

        // +(BOOL)isOAuthErrorDomain:(NSString * _Nonnull)errorDomain;
        [Static]
        [Export ("isOAuthErrorDomain:")]
        bool IsOAuthErrorDomain (string errorDomain);
    }

    // @protocol OIDExternalUserAgent <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol]
    [BaseType (typeof(NSObject), Name="OIDExternalUserAgent")]
    interface ExternalUserAgent
    {
        // @required -(BOOL)presentExternalUserAgentRequest:(id<OIDExternalUserAgentRequest> _Nonnull)request session:(id<OIDExternalUserAgentSession> _Nonnull)session;
        [Abstract]
        [Export ("presentExternalUserAgentRequest:session:")]
        bool PresentExternalUserAgentRequest (IExternalUserAgentRequest request, IExternalUserAgentSession session);

        // @required -(void)dismissExternalUserAgentAnimated:(BOOL)animated completion:(void (^ _Nonnull)(void))completion;
        [Abstract]
        [Export ("dismissExternalUserAgentAnimated:completion:")]
        void DismissExternalUserAgentAnimated (bool animated, Action completion);
    }
    interface IExternalUserAgent{}

    // @protocol OIDExternalUserAgentSession <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol]
    [BaseType (typeof(NSObject), Name="OIDExternalUserAgentSession")]
    interface ExternalUserAgentSession
    {
        // @required -(void)cancel;
        [Abstract]
        [Export ("cancel")]
        void Cancel ();

        // @required -(void)cancelWithCompletion:(void (^ _Nullable)(void))completion;
        [Abstract]
        [Export ("cancelWithCompletion:")]
        void CancelWithCompletion ([NullAllowed] Action completion);

        // @required -(BOOL)resumeExternalUserAgentFlowWithURL:(NSURL * _Nonnull)URL;
        [Abstract]
        [Export ("resumeExternalUserAgentFlowWithURL:")]
        bool ResumeExternalUserAgentFlowWithURL (NSUrl URL);

        // @required -(void)failExternalUserAgentFlowWithError:(NSError * _Nonnull)error;
        [Abstract]
        [Export ("failExternalUserAgentFlowWithError:")]
        void FailExternalUserAgentFlowWithError (NSError error);
    }

    interface IExternalUserAgentSession {}

    [Static]
    partial interface GrantType
    {
        // extern NSString *const OIDGrantTypeAuthorizationCode;
        [Field ("OIDGrantTypeAuthorizationCode", "__Internal")]
        NSString AuthorizationCode { get; }

        // extern NSString *const OIDGrantTypeRefreshToken;
        [Field ("OIDGrantTypeRefreshToken", "__Internal")]
        NSString RefreshToken { get; }

        // extern NSString *const OIDGrantTypePassword;
        [Field ("OIDGrantTypePassword", "__Internal")]
        NSString Password { get; }

        // extern NSString *const OIDGrantTypeClientCredentials;
        [Field ("OIDGrantTypeClientCredentials", "__Internal")]
        NSString ClientCredentials { get; }
    }

    // @interface OIDIDToken : NSObject
    [BaseType (typeof(NSObject), Name="OIDIDToken")]
    [DisableDefaultCtor]
    interface IDToken
    {
        // -(instancetype _Nullable)initWithIDTokenString:(NSString * _Nonnull)idToken;
        [Export ("initWithIDTokenString:")]
        NativeHandle Constructor (string idToken);

        // @property (readonly, nonatomic) NSDictionary * _Nonnull header;
        [Export ("header")]
        NSDictionary Header { get; }

        // @property (readonly, nonatomic) NSDictionary * _Nonnull claims;
        [Export ("claims")]
        NSDictionary Claims { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull issuer;
        [Export ("issuer")]
        NSUrl Issuer { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull subject;
        [Export ("subject")]
        string Subject { get; }

        // @property (readonly, nonatomic) NSArray * _Nonnull audience;
        [Export ("audience")]
        string[] Audience { get; }

        // @property (readonly, nonatomic) NSDate * _Nonnull expiresAt;
        [Export ("expiresAt")]
        NSDate ExpiresAt { get; }

        // @property (readonly, nonatomic) NSDate * _Nonnull issuedAt;
        [Export ("issuedAt")]
        NSDate IssuedAt { get; }

        // @property (readonly, nonatomic) NSString * _Nullable nonce;
        [NullAllowed, Export ("nonce")]
        string Nonce { get; }
    }

    // @interface OIDRegistrationRequest : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject), Name="OIDRegistrationRequest")]
    [DisableDefaultCtor]
    interface RegistrationRequest : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
        [Export ("configuration")]
        ServiceConfiguration Configuration { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull initialAccessToken;
        [Export ("initialAccessToken")]
        string InitialAccessToken { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull applicationType;
        [Export ("applicationType")]
        string ApplicationType { get; }

        // @property (readonly, nonatomic) NSArray<NSURL *> * _Nonnull redirectURIs;
        [Export ("redirectURIs")]
        NSUrl[] RedirectURIs { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable responseTypes;
        [NullAllowed, Export ("responseTypes")]
        string[] ResponseTypes { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable grantTypes;
        [NullAllowed, Export ("grantTypes")]
        string[] GrantTypes { get; }

        // @property (readonly, nonatomic) NSString * _Nullable subjectType;
        [NullAllowed, Export ("subjectType")]
        string SubjectType { get; }

        // @property (readonly, nonatomic) NSString * _Nullable tokenEndpointAuthenticationMethod;
        [NullAllowed, Export ("tokenEndpointAuthenticationMethod")]
        string TokenEndpointAuthenticationMethod { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSString> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration redirectURIs:(NSArray<NSURL *> * _Nonnull)redirectURIs responseTypes:(NSArray<NSString *> * _Nullable)responseTypes grantTypes:(NSArray<NSString *> * _Nullable)grantTypes subjectType:(NSString * _Nullable)subjectType tokenEndpointAuthMethod:(NSString * _Nullable)tokenEndpointAuthMethod additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("initWithConfiguration:redirectURIs:responseTypes:grantTypes:subjectType:tokenEndpointAuthMethod:additionalParameters:")]
        NativeHandle Constructor (ServiceConfiguration configuration, NSUrl[] redirectURIs, [NullAllowed] string[] responseTypes, [NullAllowed] string[] grantTypes, [NullAllowed] string subjectType, [NullAllowed] string tokenEndpointAuthMethod, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration redirectURIs:(NSArray<NSURL *> * _Nonnull)redirectURIs responseTypes:(NSArray<NSString *> * _Nullable)responseTypes grantTypes:(NSArray<NSString *> * _Nullable)grantTypes subjectType:(NSString * _Nullable)subjectType tokenEndpointAuthMethod:(NSString * _Nullable)tokenEndpointAuthMethod initialAccessToken:(NSString * _Nullable)initialAccessToken additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
        [Export ("initWithConfiguration:redirectURIs:responseTypes:grantTypes:subjectType:tokenEndpointAuthMethod:initialAccessToken:additionalParameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (ServiceConfiguration configuration, NSUrl[] redirectURIs, [NullAllowed] string[] responseTypes, [NullAllowed] string[] grantTypes, [NullAllowed] string subjectType, [NullAllowed] string tokenEndpointAuthMethod, [NullAllowed] string initialAccessToken, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(NSURLRequest * _Nonnull)URLRequest;
        [Export ("URLRequest")]
        NSUrlRequest URLRequest { get; }
    }


    [Static]
    partial interface RegistrationResponseParameters
    {
        // extern NSString *const _Nonnull OIDClientIDParam;
        [Field ("OIDClientIDParam", "__Internal")]
        NSString ClientID { get; }

        // extern NSString *const _Nonnull OIDClientIDIssuedAtParam;
        [Field ("OIDClientIDIssuedAtParam", "__Internal")]
        NSString ClientIDIssuedAt { get; }

        // extern NSString *const _Nonnull OIDClientSecretParam;
        [Field ("OIDClientSecretParam", "__Internal")]
        NSString ClientSecret { get; }

        // extern NSString *const _Nonnull OIDClientSecretExpirestAtParam;
        [Field ("OIDClientSecretExpirestAtParam", "__Internal")]
        NSString ClientSecretExpirestAt { get; }

        // extern NSString *const _Nonnull OIDRegistrationAccessTokenParam;
        [Field ("OIDRegistrationAccessTokenParam", "__Internal")]
        NSString RegistrationAccessToken { get; }

        // extern NSString *const _Nonnull OIDRegistrationClientURIParam;
        [Field ("OIDRegistrationClientURIParam", "__Internal")]
        NSString RegistrationClientURI { get; }
    }

    // @interface OIDRegistrationResponse : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject), Name="OIDRegistrationResponse")]
    [DisableDefaultCtor]
    interface RegistrationResponse : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) OIDRegistrationRequest * _Nonnull request;
        [Export ("request")]
        RegistrationRequest Request { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull clientID;
        [Export ("clientID")]
        string ClientID { get; }

        // @property (readonly, nonatomic) NSDate * _Nullable clientIDIssuedAt;
        [NullAllowed, Export ("clientIDIssuedAt")]
        NSDate ClientIDIssuedAt { get; }

        // @property (readonly, nonatomic) NSString * _Nullable clientSecret;
        [NullAllowed, Export ("clientSecret")]
        string ClientSecret { get; }

        // @property (readonly, nonatomic) NSDate * _Nullable clientSecretExpiresAt;
        [NullAllowed, Export ("clientSecretExpiresAt")]
        NSDate ClientSecretExpiresAt { get; }

        // @property (readonly, nonatomic) NSString * _Nullable registrationAccessToken;
        [NullAllowed, Export ("registrationAccessToken")]
        string RegistrationAccessToken { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable registrationClientURI;
        [NullAllowed, Export ("registrationClientURI")]
        NSUrl RegistrationClientURI { get; }

        // @property (readonly, nonatomic) NSString * _Nullable tokenEndpointAuthenticationMethod;
        [NullAllowed, Export ("tokenEndpointAuthenticationMethod")]
        string TokenEndpointAuthenticationMethod { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, INSCopying> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithRequest:(OIDRegistrationRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
        [Export ("initWithRequest:parameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (RegistrationRequest request, NSDictionary<NSString, INSCopying> parameters);
    }

    // @interface OIDScopeUtilities : NSObject
    [BaseType (typeof(NSObject), Name="OIDScopeUtilities")]
    [DisableDefaultCtor]
    interface ScopeUtilities
    {
        // +(NSString * _Nonnull)scopesWithArray:(NSArray<NSString *> * _Nonnull)scopes;
        [Static]
        [Export ("scopesWithArray:")]
        string ScopesWithArray (string[] scopes);

        // +(NSArray<NSString *> * _Nonnull)scopesArrayWithString:(NSString * _Nonnull)scopes;
        [Static]
        [Export ("scopesArrayWithString:")]
        string[] ScopesArrayWithString (string scopes);
    }

    // typedef void (^OIDServiceConfigurationCreated)(OIDServiceConfiguration * _Nullable, NSError * _Nullable);
    delegate void ServiceConfigurationCreated ([NullAllowed] ServiceConfiguration arg0, [NullAllowed] NSError arg1);

    // @interface OIDServiceConfiguration : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject), Name="OIDServiceConfiguration")]
    [DisableDefaultCtor]
    interface ServiceConfiguration : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) NSURL * _Nonnull authorizationEndpoint;
        [Export ("authorizationEndpoint")]
        NSUrl AuthorizationEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull tokenEndpoint;
        [Export ("tokenEndpoint")]
        NSUrl TokenEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable issuer;
        [NullAllowed, Export ("issuer")]
        NSUrl Issuer { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable registrationEndpoint;
        [NullAllowed, Export ("registrationEndpoint")]
        NSUrl RegistrationEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable endSessionEndpoint;
        [NullAllowed, Export ("endSessionEndpoint")]
        NSUrl EndSessionEndpoint { get; }

        // @property (readonly, nonatomic) OIDServiceDiscovery * _Nullable discoveryDocument;
        [NullAllowed, Export ("discoveryDocument")]
        ServiceDiscovery DiscoveryDocument { get; }

        // -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint;
        [Export ("initWithAuthorizationEndpoint:tokenEndpoint:")]
        NativeHandle Constructor (NSUrl authorizationEndpoint, NSUrl tokenEndpoint);

        // -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint registrationEndpoint:(NSURL * _Nullable)registrationEndpoint;
        [Export ("initWithAuthorizationEndpoint:tokenEndpoint:registrationEndpoint:")]
        NativeHandle Constructor (NSUrl authorizationEndpoint, NSUrl tokenEndpoint, [NullAllowed] NSUrl registrationEndpoint);

        //    // -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint issuer:(NSURL * _Nullable)issuer;
        //    [Export ("initWithAuthorizationEndpoint:tokenEndpoint:issuer:")]
        //    NativeHandle Constructor (NSUrl authorizationEndpoint, NSUrl tokenEndpoint, [NullAllowed] NSUrl issuer);

        // -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint issuer:(NSURL * _Nullable)issuer registrationEndpoint:(NSURL * _Nullable)registrationEndpoint;
        [Export ("initWithAuthorizationEndpoint:tokenEndpoint:issuer:registrationEndpoint:")]
        NativeHandle Constructor (NSUrl authorizationEndpoint, NSUrl tokenEndpoint, [NullAllowed] NSUrl issuer, [NullAllowed] NSUrl registrationEndpoint);

        // -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint issuer:(NSURL * _Nullable)issuer registrationEndpoint:(NSURL * _Nullable)registrationEndpoint endSessionEndpoint:(NSURL * _Nullable)endSessionEndpoint;
        [Export ("initWithAuthorizationEndpoint:tokenEndpoint:issuer:registrationEndpoint:endSessionEndpoint:")]
        NativeHandle Constructor (NSUrl authorizationEndpoint, NSUrl tokenEndpoint, [NullAllowed] NSUrl issuer, [NullAllowed] NSUrl registrationEndpoint, [NullAllowed] NSUrl endSessionEndpoint);

        // -(instancetype _Nonnull)initWithDiscoveryDocument:(OIDServiceDiscovery * _Nonnull)discoveryDocument;
        [Export ("initWithDiscoveryDocument:")]
        NativeHandle Constructor (ServiceDiscovery discoveryDocument);
    }

    // @interface OIDServiceDiscovery : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject),Name="OIDServiceDiscovery")]
    [DisableDefaultCtor]
    interface ServiceDiscovery : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull discoveryDictionary;
        [Export ("discoveryDictionary")]
        NSDictionary<NSString, NSObject> DiscoveryDictionary { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull issuer;
        [Export ("issuer")]
        NSUrl Issuer { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull authorizationEndpoint;
        [Export ("authorizationEndpoint")]
        NSUrl AuthorizationEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable deviceAuthorizationEndpoint;
        [NullAllowed, Export ("deviceAuthorizationEndpoint")]
        NSUrl DeviceAuthorizationEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull tokenEndpoint;
        [Export ("tokenEndpoint")]
        NSUrl TokenEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable userinfoEndpoint;
        [NullAllowed, Export ("userinfoEndpoint")]
        NSUrl UserinfoEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nonnull jwksURL;
        [Export ("jwksURL")]
        NSUrl JwksURL { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable registrationEndpoint;
        [NullAllowed, Export ("registrationEndpoint")]
        NSUrl RegistrationEndpoint { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable endSessionEndpoint;
        [NullAllowed, Export ("endSessionEndpoint")]
        NSUrl EndSessionEndpoint { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable scopesSupported;
        [NullAllowed, Export ("scopesSupported")]
        string[] ScopesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull responseTypesSupported;
        [Export ("responseTypesSupported")]
        string[] ResponseTypesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable responseModesSupported;
        [NullAllowed, Export ("responseModesSupported")]
        string[] ResponseModesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable grantTypesSupported;
        [NullAllowed, Export ("grantTypesSupported")]
        string[] GrantTypesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable acrValuesSupported;
        [NullAllowed, Export ("acrValuesSupported")]
        string[] AcrValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull subjectTypesSupported;
        [Export ("subjectTypesSupported")]
        string[] SubjectTypesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull IDTokenSigningAlgorithmValuesSupported;
        [Export ("IDTokenSigningAlgorithmValuesSupported")]
        string[] IDTokenSigningAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable IDTokenEncryptionAlgorithmValuesSupported;
        [NullAllowed, Export ("IDTokenEncryptionAlgorithmValuesSupported")]
        string[] IDTokenEncryptionAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable IDTokenEncryptionEncodingValuesSupported;
        [NullAllowed, Export ("IDTokenEncryptionEncodingValuesSupported")]
        string[] IDTokenEncryptionEncodingValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoSigningAlgorithmValuesSupported;
        [NullAllowed, Export ("userinfoSigningAlgorithmValuesSupported")]
        string[] UserinfoSigningAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoEncryptionAlgorithmValuesSupported;
        [NullAllowed, Export ("userinfoEncryptionAlgorithmValuesSupported")]
        string[] UserinfoEncryptionAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoEncryptionEncodingValuesSupported;
        [NullAllowed, Export ("userinfoEncryptionEncodingValuesSupported")]
        string[] UserinfoEncryptionEncodingValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectSigningAlgorithmValuesSupported;
        [NullAllowed, Export ("requestObjectSigningAlgorithmValuesSupported")]
        string[] RequestObjectSigningAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectEncryptionAlgorithmValuesSupported;
        [NullAllowed, Export ("requestObjectEncryptionAlgorithmValuesSupported")]
        string[] RequestObjectEncryptionAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectEncryptionEncodingValuesSupported;
        [NullAllowed, Export ("requestObjectEncryptionEncodingValuesSupported")]
        string[] RequestObjectEncryptionEncodingValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable tokenEndpointAuthMethodsSupported;
        [NullAllowed, Export ("tokenEndpointAuthMethodsSupported")]
        string[] TokenEndpointAuthMethodsSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable tokenEndpointAuthSigningAlgorithmValuesSupported;
        [NullAllowed, Export ("tokenEndpointAuthSigningAlgorithmValuesSupported")]
        string[] TokenEndpointAuthSigningAlgorithmValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable displayValuesSupported;
        [NullAllowed, Export ("displayValuesSupported")]
        string[] DisplayValuesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimTypesSupported;
        [NullAllowed, Export ("claimTypesSupported")]
        string[] ClaimTypesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimsSupported;
        [NullAllowed, Export ("claimsSupported")]
        string[] ClaimsSupported { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable serviceDocumentation;
        [NullAllowed, Export ("serviceDocumentation")]
        NSUrl ServiceDocumentation { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimsLocalesSupported;
        [NullAllowed, Export ("claimsLocalesSupported")]
        string[] ClaimsLocalesSupported { get; }

        // @property (readonly, nonatomic) NSArray<NSString *> * _Nullable UILocalesSupported;
        [NullAllowed, Export ("UILocalesSupported")]
        string[] UILocalesSupported { get; }

        // @property (readonly, nonatomic) BOOL claimsParameterSupported;
        [Export ("claimsParameterSupported")]
        bool ClaimsParameterSupported { get; }

        // @property (readonly, nonatomic) BOOL requestParameterSupported;
        [Export ("requestParameterSupported")]
        bool RequestParameterSupported { get; }

        // @property (readonly, nonatomic) BOOL requestURIParameterSupported;
        [Export ("requestURIParameterSupported")]
        bool RequestURIParameterSupported { get; }

        // @property (readonly, nonatomic) BOOL requireRequestURIRegistration;
        [Export ("requireRequestURIRegistration")]
        bool RequireRequestURIRegistration { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable OPPolicyURI;
        [NullAllowed, Export ("OPPolicyURI")]
        NSUrl OPPolicyURI { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable OPTosURI;
        [NullAllowed, Export ("OPTosURI")]
        NSUrl OPTosURI { get; }

        // -(instancetype _Nullable)initWithJSON:(NSString * _Nonnull)serviceDiscoveryJSON error:(NSError * _Nullable * _Nullable)error;
        [Export ("initWithJSON:error:")]
        NativeHandle Constructor (string serviceDiscoveryJSON, [NullAllowed] out NSError error);

        // -(instancetype _Nullable)initWithJSONData:(NSData * _Nonnull)serviceDiscoveryJSONData error:(NSError * _Nullable * _Nullable)error;
        [Export ("initWithJSONData:error:")]
        NativeHandle Constructor (NSData serviceDiscoveryJSONData, [NullAllowed] out NSError error);

        // -(instancetype _Nullable)initWithDictionary:(NSDictionary * _Nonnull)serviceDiscoveryDictionary error:(NSError * _Nullable * _Nullable)error __attribute__((objc_designated_initializer));
        [Export ("initWithDictionary:error:")]
        [DesignatedInitializer]
        NativeHandle Constructor (NSDictionary serviceDiscoveryDictionary, [NullAllowed] out NSError error);
    }

    // @interface OIDTokenRequest : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject),Name="OIDTokenRequest")]
    [DisableDefaultCtor]
    interface TokenRequest : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
        [Export ("configuration")]
        ServiceConfiguration Configuration { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull grantType;
        [Export ("grantType")]
        string GrantType { get; }

        // @property (readonly, nonatomic) NSString * _Nullable authorizationCode;
        [NullAllowed, Export ("authorizationCode")]
        string AuthorizationCode { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable redirectURL;
        [NullAllowed, Export ("redirectURL")]
        NSUrl RedirectURL { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull clientID;
        [Export ("clientID")]
        string ClientID { get; }

        // @property (readonly, nonatomic) NSString * _Nullable clientSecret;
        [NullAllowed, Export ("clientSecret")]
        string ClientSecret { get; }

        // @property (readonly, nonatomic) NSString * _Nullable scope;
        [NullAllowed, Export ("scope")]
        string Scope { get; }

        // @property (readonly, nonatomic) NSString * _Nullable refreshToken;
        [NullAllowed, Export ("refreshToken")]
        string RefreshToken { get; }

        // @property (readonly, nonatomic) NSString * _Nullable codeVerifier;
        [NullAllowed, Export ("codeVerifier")]
        string CodeVerifier { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSString> AdditionalParameters { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalHeaders;
        [NullAllowed, Export ("additionalHeaders")]
        NSDictionary<NSString, NSString> AdditionalHeaders { get; }

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration grantType:(NSString * _Nonnull)grantType authorizationCode:(NSString * _Nullable)code redirectURL:(NSURL * _Nullable)redirectURL clientID:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scopes:(NSArray<NSString *> * _Nullable)scopes refreshToken:(NSString * _Nullable)refreshToken codeVerifier:(NSString * _Nullable)codeVerifier additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters additionalHeaders:(NSDictionary<NSString *,NSString *> * _Nullable)additionalHeaders;
        [Export ("initWithConfiguration:grantType:authorizationCode:redirectURL:clientID:clientSecret:scopes:refreshToken:codeVerifier:additionalParameters:additionalHeaders:")]
        NativeHandle Constructor (ServiceConfiguration configuration, string grantType, [NullAllowed] string code, [NullAllowed] NSUrl redirectURL, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string[] scopes, [NullAllowed] string refreshToken, [NullAllowed] string codeVerifier, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters, [NullAllowed] NSDictionary<NSString, NSString> additionalHeaders);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration grantType:(NSString * _Nonnull)grantType authorizationCode:(NSString * _Nullable)code redirectURL:(NSURL * _Nullable)redirectURL clientID:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scope:(NSString * _Nullable)scope refreshToken:(NSString * _Nullable)refreshToken codeVerifier:(NSString * _Nullable)codeVerifier additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters additionalHeaders:(NSDictionary<NSString *,NSString *> * _Nullable)additionalHeaders __attribute__((objc_designated_initializer));
        [Export ("initWithConfiguration:grantType:authorizationCode:redirectURL:clientID:clientSecret:scope:refreshToken:codeVerifier:additionalParameters:additionalHeaders:")]
        [DesignatedInitializer]
        NativeHandle Constructor (ServiceConfiguration configuration, string grantType, [NullAllowed] string code, [NullAllowed] NSUrl redirectURL, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string scope, [NullAllowed] string refreshToken, [NullAllowed] string codeVerifier, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters, [NullAllowed] NSDictionary<NSString, NSString> additionalHeaders);

        //   // -(instancetype _Nonnull)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
        //   [Export ("initWithCoder:")]
        //   [DesignatedInitializer]
        //   NativeHandle Constructor (NSCoder aDecoder);

        // -(NSURLRequest * _Nonnull)URLRequest;
        [Export ("URLRequest")]
        NSUrlRequest URLRequest { get; }
    }

    // @interface OIDTokenResponse : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject),Name="OIDTokenResponse")]
    [DisableDefaultCtor]
    interface TokenResponse : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) OIDTokenRequest * _Nonnull request;
        [Export ("request")]
        TokenRequest Request { get; }

        // @property (readonly, nonatomic) NSString * _Nullable accessToken;
        [NullAllowed, Export ("accessToken")]
        string AccessToken { get; }

        // @property (readonly, nonatomic) NSDate * _Nullable accessTokenExpirationDate;
        [NullAllowed, Export ("accessTokenExpirationDate")]
        NSDate AccessTokenExpirationDate { get; }

        // @property (readonly, nonatomic) NSString * _Nullable tokenType;
        [NullAllowed, Export ("tokenType")]
        string TokenType { get; }

        // @property (readonly, nonatomic) NSString * _Nullable idToken;
        [NullAllowed, Export ("idToken")]
        string IdToken { get; }

        // @property (readonly, nonatomic) NSString * _Nullable refreshToken;
        [NullAllowed, Export ("refreshToken")]
        string RefreshToken { get; }

        // @property (readonly, nonatomic) NSString * _Nullable scope;
        [NullAllowed, Export ("scope")]
        string Scope { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSCopying> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithRequest:(OIDTokenRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
        [Export ("initWithRequest:parameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (TokenRequest request, NSDictionary<NSString, NSCopying> parameters);
    }

    // @interface OIDTokenUtilities : NSObject
    [BaseType (typeof(NSObject),Name="OIDTokenUtilities")]
    [DisableDefaultCtor]
    interface TokenUtilities
    {
        // +(NSString * _Nonnull)encodeBase64urlNoPadding:(NSData * _Nonnull)data;
        [Static]
        [Export ("encodeBase64urlNoPadding:")]
        string EncodeBase64urlNoPadding (NSData data);

        // +(NSString * _Nullable)randomURLSafeStringWithSize:(NSUInteger)size;
        [Static]
        [Export ("randomURLSafeStringWithSize:")]
        [return: NullAllowed]
        string RandomURLSafeStringWithSize (nuint size);

        // +(NSData * _Nonnull)sha256:(NSString * _Nonnull)inputString;
        [Static]
        [Export ("sha256:")]
        NSData Sha256 (string inputString);

        // +(NSString * _Nullable)redact:(NSString * _Nullable)inputString;
        [Static]
        [Export ("redact:")]
        [return: NullAllowed]
        string Redact ([NullAllowed] string inputString);

        // +(NSString * _Nonnull)formUrlEncode:(NSString * _Nonnull)inputString;
        [Static]
        [Export ("formUrlEncode:")]
        string FormUrlEncode (string inputString);
    }

    // @interface OIDURLSessionProvider : NSObject
    [BaseType (typeof(NSObject),Name="OIDURLSessionProvider")]
    interface URLSessionProvider
    {
        // +(NSURLSession * _Nonnull)session;
        // +(void)setSession:(NSURLSession * _Nonnull)session;
        [Static]
        [Export ("session")]
        NSUrlSession Session { get; set; }
    }

    // @interface OIDEndSessionRequest : NSObject <NSCopying, NSSecureCoding, OIDExternalUserAgentRequest>
    [BaseType (typeof(NSObject),Name="OIDEndSessionRequest")]
    [DisableDefaultCtor]
    interface EndSessionRequest : INSCopying, INSSecureCoding, IExternalUserAgentRequest
    {
        // @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
        [Export ("configuration")]
        ServiceConfiguration Configuration { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable postLogoutRedirectURL;
        [NullAllowed, Export ("postLogoutRedirectURL")]
        NSUrl PostLogoutRedirectURL { get; }

        // @property (readonly, nonatomic) NSString * _Nullable idTokenHint;
        [NullAllowed, Export ("idTokenHint")]
        string IdTokenHint { get; }

        // @property (readonly, nonatomic) NSString * _Nullable state;
        [NullAllowed, Export ("state")]
        string State { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSString> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration idTokenHint:(NSString * _Nonnull)idTokenHint postLogoutRedirectURL:(NSURL * _Nonnull)postLogoutRedirectURL additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
        [Export ("initWithConfiguration:idTokenHint:postLogoutRedirectURL:additionalParameters:")]
        NativeHandle Constructor (ServiceConfiguration configuration, string idTokenHint, NSUrl postLogoutRedirectURL, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration idTokenHint:(NSString * _Nonnull)idTokenHint postLogoutRedirectURL:(NSURL * _Nonnull)postLogoutRedirectURL state:(NSString * _Nonnull)state additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
        [Export ("initWithConfiguration:idTokenHint:postLogoutRedirectURL:state:additionalParameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (ServiceConfiguration configuration, string idTokenHint, NSUrl postLogoutRedirectURL, string state, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

        // -(NSURL * _Nonnull)endSessionRequestURL;
        [Export ("endSessionRequestURL")]
        NSUrl EndSessionRequestURL { get; }
    }

    // @interface OIDEndSessionResponse : NSObject <NSCopying, NSSecureCoding>
    [BaseType (typeof(NSObject),Name="OIDEndSessionResponse")]
    [DisableDefaultCtor]
    interface EndSessionResponse : INSCopying, INSSecureCoding
    {
        // @property (readonly, nonatomic) OIDEndSessionRequest * _Nonnull request;
        [Export ("request")]
        EndSessionRequest Request { get; }

        // @property (readonly, nonatomic) NSString * _Nullable state;
        [NullAllowed, Export ("state")]
        string State { get; }

        // @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
        [NullAllowed, Export ("additionalParameters")]
        NSDictionary<NSString, NSCopying> AdditionalParameters { get; }

        // -(instancetype _Nonnull)initWithRequest:(OIDEndSessionRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
        [Export ("initWithRequest:parameters:")]
        [DesignatedInitializer]
        NativeHandle Constructor (EndSessionRequest request, NSDictionary<NSString, NSCopying> parameters);
    }


    // @interface OIDExternalUserAgentIOS : NSObject <OIDExternalUserAgent>
    [NoMacCatalyst]
    [BaseType (typeof(NSObject), Name="OIDExternalUserAgentIOS")]
    interface ExternalUserAgentIOS : ExternalUserAgent
    {
        // -(instancetype _Nullable)initWithPresentingViewController:(UIViewController * _Nonnull)presentingViewController __attribute__((objc_designated_initializer));
        [Export ("initWithPresentingViewController:")]
        [DesignatedInitializer]
        NativeHandle Constructor (UIViewController presentingViewController);

        // -(instancetype _Nullable)initWithPresentingViewController:(UIViewController * _Nonnull)presentingViewController prefersEphemeralSession:(BOOL)prefersEphemeralSession __attribute__((availability(ios, introduced=13)));
        //[iOS (13,0)]
        [Export ("initWithPresentingViewController:prefersEphemeralSession:")]
        NativeHandle Constructor (UIViewController presentingViewController, bool prefersEphemeralSession);
    }

    // typedef NSURL * _Nullable (^OIDCustomBrowserURLTransformation)(NSURL * _Nullable);
    delegate NSUrl CustomBrowserURLTransformation ([NullAllowed] NSUrl arg0);

    // @interface OIDExternalUserAgentIOSCustomBrowser : NSObject <OIDExternalUserAgent>
    [NoMacCatalyst]
    [BaseType (typeof(NSObject),Name="OIDExternalUserAgentIOSCustomBrowser")]
    [DisableDefaultCtor]
    interface ExternalUserAgentIOSCustomBrowser : ExternalUserAgent
    {
        // @property (readonly, nonatomic) OIDCustomBrowserURLTransformation _Nonnull URLTransformation;
        [Export ("URLTransformation")]
        CustomBrowserURLTransformation URLTransformation { get; }

        // @property (readonly, nonatomic) NSString * _Nullable canOpenURLScheme;
        [NullAllowed, Export ("canOpenURLScheme")]
        string CanOpenURLScheme { get; }

        // @property (readonly, nonatomic) NSURL * _Nullable appStoreURL;
        [NullAllowed, Export ("appStoreURL")]
        NSUrl AppStoreURL { get; }

        // +(instancetype _Nonnull)CustomBrowserChrome;
        [Static]
        [Export ("CustomBrowserChrome")]
        ExternalUserAgentIOSCustomBrowser CustomBrowserChrome ();

        // +(instancetype _Nonnull)CustomBrowserFirefox;
        [Static]
        [Export ("CustomBrowserFirefox")]
        ExternalUserAgentIOSCustomBrowser CustomBrowserFirefox ();

        // +(instancetype _Nonnull)CustomBrowserOpera;
        [Static]
        [Export ("CustomBrowserOpera")]
        ExternalUserAgentIOSCustomBrowser CustomBrowserOpera ();

        // +(instancetype _Nonnull)CustomBrowserSafari;
        [Static]
        [Export ("CustomBrowserSafari")]
        ExternalUserAgentIOSCustomBrowser CustomBrowserSafari ();

        // +(OIDCustomBrowserURLTransformation _Nonnull)URLTransformationSchemeSubstitutionHTTPS:(NSString * _Nonnull)browserSchemeHTTPS HTTP:(NSString * _Nullable)browserSchemeHTTP;
        [Static]
        [Export ("URLTransformationSchemeSubstitutionHTTPS:HTTP:")]
        CustomBrowserURLTransformation URLTransformationSchemeSubstitutionHTTPS (string browserSchemeHTTPS, [NullAllowed] string browserSchemeHTTP);

        // +(OIDCustomBrowserURLTransformation _Nonnull)URLTransformationSchemeConcatPrefix:(NSString * _Nonnull)URLprefix;
        [Static]
        [Export ("URLTransformationSchemeConcatPrefix:")]
        CustomBrowserURLTransformation URLTransformationSchemeConcatPrefix (string URLprefix);

        // -(instancetype _Nullable)initWithURLTransformation:(OIDCustomBrowserURLTransformation _Nonnull)URLTransformation;
        [Export ("initWithURLTransformation:")]
        NativeHandle Constructor (CustomBrowserURLTransformation URLTransformation);

        // -(instancetype _Nullable)initWithURLTransformation:(OIDCustomBrowserURLTransformation _Nonnull)URLTransformation canOpenURLScheme:(NSString * _Nullable)canOpenURLScheme appStoreURL:(NSURL * _Nullable)appStoreURL __attribute__((objc_designated_initializer));
        [Export ("initWithURLTransformation:canOpenURLScheme:appStoreURL:")]
        [DesignatedInitializer]
        NativeHandle Constructor (CustomBrowserURLTransformation URLTransformation, [NullAllowed] string canOpenURLScheme, [NullAllowed] NSUrl appStoreURL);
    }

    // @interface OIDExternalUserAgentCatalyst : NSObject <OIDExternalUserAgent>
    [NoiOS, MacCatalyst (13,0)]
    [BaseType (typeof(NSObject),Name="OIDExternalUserAgentCatalyst")]
    [DisableDefaultCtor]
    interface ExternalUserAgentCatalyst : ExternalUserAgent
    {
        // -(instancetype _Nullable)initWithPresentingViewController:(UIViewController * _Nonnull)presentingViewController __attribute__((objc_designated_initializer));
        [Export ("initWithPresentingViewController:")]
        [DesignatedInitializer]
        NativeHandle Constructor (UIViewController presentingViewController);

        // -(instancetype _Nullable)initWithPresentingViewController:(UIViewController * _Nonnull)presentingViewController prefersEphemeralSession:(BOOL)prefersEphemeralSession;
        [Export ("initWithPresentingViewController:prefersEphemeralSession:")]
        NativeHandle Constructor (UIViewController presentingViewController, bool prefersEphemeralSession);
    }

}
