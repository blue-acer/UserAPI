using UserAPI.Entities;
using UserAPI.Models;

namespace UserAPI.Interfaces
{
    public interface IUserDetailsRepository
    {
        Task<List<UserDetails>> GetAllUserDetailsData();
        Task<List<UserDetails>> GetAllUserDetailsSPData();
        ValueTask<UserDetails?> GetUserDetailsData(long id);
        Task<List<UserDetails>> GetUserDetailsDataByEmail(string email);
        Task<int> deleteUserData(long id);
        Task<int> addUserData(String fName, string lName, string email);
    }
}
