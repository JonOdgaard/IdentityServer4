// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServerHost.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Serilog;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstAgenda.IdentityServer.Core;
using IdentityServer4.Test;
using IdentityServerHost.Extensions;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerHost
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;

            IdentityModelEventSource.ShowPII = true;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // cookie policy to deal with temporary browser incompatibilities
            services.AddSameSiteCookiePolicy();

            services.AddDbContext<FirstAgendaIdentityStoreContext>(options =>
                options.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=local_firstagenda;User Id=sa;Password=S0me_Passw0rd;"));

            IdentityModelEventSource.ShowPII = true; // TODO: Only set if dev

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;

                    options.EmitScopesAsSpaceDelimitedStringInJwt = true;

                    options.MutualTls.Enabled = true;
                    options.MutualTls.DomainName = "mtls";
                    //options.MutualTls.AlwaysEmitConfirmationClaim = true;
                })
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.IdentityResources)
                .AddInMemoryApiScopes(Resources.ApiScopes)
                .AddInMemoryApiResources(Resources.ApiResources)
                .AddSigningCredential()
                .AddExtensionGrantValidator<Extensions.ExtensionGrantValidator>()
                .AddExtensionGrantValidator<Extensions.NoSubjectExtensionGrantValidator>()
                .AddJwtBearerClientAuthentication()
                .AddAppAuthRedirectUriValidator()
                .AddTestUsers()
                .AddProfileService<HostProfileService>()
                .AddCustomTokenRequestValidator<ParameterizedScopeTokenRequestValidator>()
                .AddScopeParser<ParameterizedScopeParser>()
                .AddMutualTlsSecretValidators();

            // use this for persisted grants store
            // var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            // const string connectionString = "DataSource=identityserver.db";
            // builder.AddOperationalStore(options =>
            // {
            //     options.ConfigureDbContext = b => b.UseSqlite(connectionString,
            //         sql => sql.MigrationsAssembly(migrationsAssembly));
            // });


            services.AddExternalIdentityProviders();

            // services.AddAuthentication()
            //     .AddCertificate(options =>
            //     {
            //         options.AllowedCertificateTypes = CertificateTypes.All;
            //         options.RevocationMode = X509RevocationMode.NoCheck;
            //     })
            //     .AddWsFederation(options =>
            //     {
            //         options.MetadataAddress =
            //             "https://adfs1.firstagenda.com/FederationMetadata/2007-06/FederationMetadata.xml";
            //         options.RequireHttpsMetadata = true;
            //
            //         options.Wtrealm = "https://localhost:5001/";
            //
            //         options.CallbackPath = "/adfs/callback";
            //         options.SkipUnrecognizedRequests = true;
            //         options.Events = new WsFederationEvents()
            //         {
            //             OnSecurityTokenReceived = OnSecurityTokenReceived,
            //             OnSecurityTokenValidated = OnSecurityTokenValidated
            //         };
            //         
            //         options.SecurityTokenHandlers.Add(new EncryptedSecurityToken());
            //     });

            services.AddCertificateForwardingForNginx();

            services.AddLocalApiAuthentication(principal =>
            {
                principal.Identities.First().AddClaim(new Claim("additional_claim", "additional_value"));

                return Task.FromResult(principal);
            });
        }



        public void Configure(IApplicationBuilder app)
        {
            // use this for persisted grants store
            // app.InitializePersistedGrantsStore();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCertificateForwarding();
            app.UseCookiePolicy();

            app.UseSerilogRequestLogging();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}