using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServerHost
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddExternalIdentityProviders(this IServiceCollection services)
        {
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            services.AddOidcStateDataFormatterCache("aad", "demoidsrv");

            services.AddAuthentication()
                // .AddOpenIdConnect("Google", "Google", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.ForwardSignOut = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                //
                //     options.Authority = "https://accounts.google.com/";
                //     options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
                //
                //     options.CallbackPath = "/signin-google";
                //     options.Scope.Add("email");
                // })
                // .AddOpenIdConnect("demoidsrv", "IdentityServer", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                //
                //     options.Authority = "https://demo.identityserver.io/";
                //     options.ClientId = "login";
                //     options.ResponseType = "id_token";
                //     options.SaveTokens = true;
                //     options.CallbackPath = "/signin-idsrv";
                //     options.SignedOutCallbackPath = "/signout-callback-idsrv";
                //     options.RemoteSignOutPath = "/signout-idsrv";
                //
                //     options.TokenValidationParameters = new TokenValidationParameters
                //     {
                //         NameClaimType = "name",
                //         RoleClaimType = "role"
                //     };
                // })
                // .AddOpenIdConnect("aad", "Azure AD", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                //
                //     options.Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705";
                //     options.ClientId = "96e3c53e-01cb-4244-b658-a42164cb67a9";
                //     options.ResponseType = "id_token";
                //     options.CallbackPath = "/signin-aad";
                //     options.SignedOutCallbackPath = "/signout-callback-aad";
                //     options.RemoteSignOutPath = "/signout-aad";
                //     options.TokenValidationParameters = new TokenValidationParameters
                //     {
                //         NameClaimType = "name",
                //         RoleClaimType = "role"
                //     };
                // })
                // .AddOpenIdConnect("adfs", "ADFS", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                //
                //     options.Authority = "https://adfs.leastprivilege.vm/adfs";
                //     options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c3c";
                //     options.ResponseType = "id_token";
                //
                //     options.CallbackPath = "/signin-adfs";
                //     options.SignedOutCallbackPath = "/signout-callback-adfs";
                //     options.RemoteSignOutPath = "/signout-adfs";
                //     options.TokenValidationParameters = new TokenValidationParameters
                //     {
                //         NameClaimType = "name",
                //         RoleClaimType = "role"
                //     };
                // }).AddOpenIdConnect("adfsqa", "ADFS QA", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                //
                //     options.Authority = "https://adfs1.firstagenda.com/adfs";
                //     options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c3c";
                //     options.ResponseType = "id_token";
                //
                //     options.CallbackPath = "/signin-adfs";
                //     options.SignedOutCallbackPath = "/signout-callback-adfsqa";
                //     options.RemoteSignOutPath = "/signout-adfsqa";
                //     options.TokenValidationParameters = new TokenValidationParameters
                //     {
                //         NameClaimType = "name",
                //         RoleClaimType = "role"
                //     };
                .AddCertificate(options =>
                {
                    options.AllowedCertificateTypes = CertificateTypes.All;
                    options.RevocationMode = X509RevocationMode.NoCheck;
                }).AddWsFederation("QaAdfs1", "QA Adfs 1", options =>
                {
                    // options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.MetadataAddress =
                        "https://adfs1.firstagenda.com/FederationMetadata/2007-06/FederationMetadata.xml";
                    options.RequireHttpsMetadata = true;

                    options.Wtrealm = "https://localhost:5001/";

                    options.CallbackPath = "/adfs/callback";
                    options.SkipUnrecognizedRequests = true;
                    options.Events = new WsFederationEvents()
                    {
                        OnSecurityTokenReceived = OnSecurityTokenReceived,
                        OnSecurityTokenValidated = OnSecurityTokenValidated
                    };

                    // options.SecurityTokenHandlers.Add(new EncryptedSecurityToken());
                });
            ;

            return services;
        }

        private static Task OnSecurityTokenValidated(SecurityTokenValidatedContext arg)
        {
            return Task.CompletedTask;
        }

        private static Task OnSecurityTokenReceived(SecurityTokenReceivedContext arg)
        {
            return Task.CompletedTask;
        }

        public static void AddCertificateForwardingForNginx(this IServiceCollection services)
        {
            services.AddCertificateForwarding(options =>
            {
                options.CertificateHeader = "X-SSL-CERT";

                options.HeaderConverter = (headerValue) =>
                {
                    X509Certificate2 clientCertificate = null;

                    if (!string.IsNullOrWhiteSpace(headerValue))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(Uri.UnescapeDataString(headerValue));
                        clientCertificate = new X509Certificate2(bytes);
                    }

                    return clientCertificate;
                };
            });
        }
    }
}