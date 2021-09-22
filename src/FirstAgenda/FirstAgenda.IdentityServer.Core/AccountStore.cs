// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FirstAgenda.IdentityServer.Core.Models;
using IdentityModel;
using Microsoft.EntityFrameworkCore;

namespace FirstAgenda.IdentityServer.Core
{
    public interface IAccountStore
    {
        public Task<bool> ValidateCredentials(string incomingLoginId, string incomingPassword);
        Task<Account> FindByUserLoginId(string loginId);
        Task<Account> FindByExternalProvider(string externalProvider, string externalProviderSubjectId);

        Task<Account> AutoProvisionUserFromExternalProvider(string externalProviderName, string externalProviderUserId,
            List<Claim> claims);

        Task<Account> FindBySubjectId(string subjectId);
    }

    public class AccountStore : IAccountStore
    {
        private readonly FirstAgendaIdentityStoreContext _context;

        public AccountStore(FirstAgendaIdentityStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateCredentials(string incomingLoginId, string incomingPassword)
        {
            var account = await FindByUserLoginId(incomingLoginId);

            if (account != null)
            {
                var hashedIncomingPassword = GetHashedPassword(incomingPassword, account.Salt);

                return account.PasswordHash.Equals(hashedIncomingPassword);
            }

            return false;
        }

        private const string SystemSalt = ";2m_æp2AIEN92j,aq!8(/2.LIKDNkvvemnv,.ÅW22c1jhjaik";
        private const string CodeHash = "jklg@sbjk//(bk#j!hjkhjdfk##!!hjkdjkhjfhjsjhjjhh3hhj7782mkjnk2j34JJJk2kkk3K";

        public string GetHashedPassword(string password, string salt)
        {
            return Hash(SystemSalt + CodeHash + password, salt);
        }

        private string Hash(string value, string salt)
        {
            var i = salt.IndexOf('.');
            var iters = int.Parse(salt.Substring(0, i), System.Globalization.NumberStyles.HexNumber);
            salt = salt.Substring(i + 1);

            using (var pbkdf2 =
                new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(value), Convert.FromBase64String(salt), iters))
            {
                var key = pbkdf2.GetBytes(24);

                return Convert.ToBase64String(key);
            }
        }

        public async Task<Account> FindBySubjectId(string subjectId)
        {
            return await _context.Accounts.SingleOrDefaultAsync(a => a.SubjectId == subjectId);
        }

        public async Task<Account> FindByUserLoginId(string loginId)
        {
            var loginIdLowered = loginId.ToLower();

            return await _context.Accounts.SingleOrDefaultAsync(x => x.LoginId == loginIdLowered);
        }

        public async Task<Account> FindByExternalProvider(string externalProvider, string externalProviderSubjectId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x =>
                x.ExternalProviderName == externalProvider &&
                x.ExternalProviderSubjectId == externalProviderSubjectId);
        }

        public async Task<Account> AutoProvisionUserFromExternalProvider(string externalProviderName,
            string externalProviderUserId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(
                        new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            var sub = CryptoRandom.CreateUniqueId(format: CryptoRandom.OutputFormat.Hex);

            // check if a display name is available, otherwise fallback to subject id
            var userFullName = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;

            // create new user
            var user = new Account
            {
                SubjectId = sub,
                ExternalProviderName = externalProviderName,
                ExternalProviderSubjectId = externalProviderUserId,
                PasswordHash = "some",
                Salt = "some",
                IsActive = true,
                LastPasswordChangeDateUtc = DateTimeOffset.UtcNow,
                CreatedDateUtc = DateTimeOffset.UtcNow,
                Uid = Guid.NewGuid(),
                PasswordExpired = false,
                FailedLoginAttempts = 0,
                AccountProfiles = new List<AccountProfile>()
                {
                    new AccountProfile()
                    {
                        UserFullName = userFullName,
                        // ...
                    }
                }
                // Claims = filtered
            };

            // add user to in-memory store
            _context.Accounts.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}