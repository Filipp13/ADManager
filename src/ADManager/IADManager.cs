using System.Threading.Tasks;

namespace ADManager
{
    public interface IADManager
    {

        public Task<bool> IsUsersInsideGroupAsync(string groupName, string login);

        public Task<bool> IsUsersInsideServiceGroupAsync(string groupName, string login);

    }
}
