using Microsoft.Extensions.Options;
using System;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Threading.Tasks;

namespace ADManager
{
    public class ADManagerBase
    {
        private readonly LdapDirectoryIdentifier identifier;
        private readonly NetworkCredential credential;

        public ADManagerBase(
            IOptions<ADManagerOptions> aDManagmentOptions,
            IOptions<ADManagerSecurityOptions> aDManagerSecurityOptions)
        {
            _ = aDManagmentOptions
                ?? throw new ArgumentNullException(nameof(aDManagmentOptions));
            identifier = new LdapDirectoryIdentifier(aDManagmentOptions.Value.Address, aDManagmentOptions.Value.Port);

            _ = aDManagerSecurityOptions
                ?? throw new ArgumentNullException(nameof(aDManagerSecurityOptions));
            credential = new NetworkCredential(
                aDManagerSecurityOptions.Value.Login,
                aDManagerSecurityOptions.Value.Password);
        }

        protected async Task<SearchResponse> SendADRequestAsync(
            string distinguishedName,
            string filter,
            string[] attributeList)
        {
            using (LdapConnection ldap = new LdapConnection(identifier, credential))
            {
                try
                {
                    var searchRequest = new SearchRequest(
                        distinguishedName,
                        filter,
                        SearchScope.Subtree,
                        attributeList);

                    searchRequest.SizeLimit = int.MaxValue;

                    var response = await Task<DirectoryResponse>.Factory.FromAsync(
                        ldap.BeginSendRequest,
                        (iar) => ldap.EndSendRequest(iar),
                        searchRequest,
                        PartialResultProcessing.NoPartialResultSupport,
                        null) as SearchResponse;

                    return response!;

                }
                catch
                {
                    //log
                    throw;
                }
            }
        }
    }
}