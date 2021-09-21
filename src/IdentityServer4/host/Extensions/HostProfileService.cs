using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstAgenda.IdentityServer.Core;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace IdentityServerHost.Extensions
{
    public class HostProfileService : TestUserProfileService
    {
        public HostProfileService(IAccountStore accountStore, ILogger<TestUserProfileService> logger) : base(accountStore, logger)
        {
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var transaction = context.RequestedResources.ParsedScopes.FirstOrDefault(x => x.ParsedName == "transaction");
            if (transaction?.ParsedParameter != null)
            {
                context.IssuedClaims.Add(new Claim("transaction_id", transaction.ParsedParameter));
            }
        }
    }
}