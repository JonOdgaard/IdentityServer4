// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using FirstAgenda.IdentityServer.Core;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.Test
{
    /// <summary>
    /// Extension methods for the IdentityServer builder
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Adds test users.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="users">The users.</param>
        /// <returns></returns>
        // public static IIdentityServerBuilder AddTestUsers(this IIdentityServerBuilder builder, List<TestUser> users)
        // {
        //     builder.Services.AddSingleton(new TestUserStore(users));
        //     builder.AddProfileService<TestUserProfileService>();
        //     builder.AddResourceOwnerValidator<TestUserResourceOwnerPasswordValidator>();
        //
        //     return builder;
        // }
        
        public static IIdentityServerBuilder AddTestUsers(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IAccountStore, AccountStore>();
            builder.AddProfileService<TestUserProfileService>();
            builder.AddResourceOwnerValidator<TestUserResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}