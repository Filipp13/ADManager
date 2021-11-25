using Microsoft.Extensions.Options;
using System.DirectoryServices.Protocols;
using System.Threading.Tasks;

namespace ADManager
{
    public sealed class ADManager : ADManagerBase, IADManager
    {
        public ADManager(IOptions<ADManagerOptions> aDManagmentOptions)
            : base(aDManagmentOptions) { }

        public Task<bool> IsUsersInsideGroupAsync(string groupName, string login)
        => IsUsersInsideGroupAsync("Users", groupName, login);

        public Task<bool> IsUsersInsideServiceGroupAsync(string groupName, string login)
        => IsUsersInsideGroupAsync("CIS IT", groupName, login);

        private async Task<bool> IsUsersInsideGroupAsync(string ou, string groupName, string login)
        {
            string group = groupName;

            if (groupName.Split(',').Length == 2)
            {
                group = groupName.Split(',')[1];
            }

            string DistinguishedName = $"OU={ou},OU=CIS,DC=atrema,DC=deloitte,DC=com";
            string Filter = $"(&(objectCategory=person)(sAMAccountName={login})(memberOf=CN={group},OU=Distribution Groups,OU=CIS,DC=atrema,DC=deloitte,DC=com))";

            return await SendADRequestAsync(DistinguishedName, Filter, new string[0]) switch
            {
                SearchResponse searchResponse when
                                            searchResponse is not null
                                            && searchResponse.Entries.Count > 0 => true,
                _ => false
            };

        }
    }
}