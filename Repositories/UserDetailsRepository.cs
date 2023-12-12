using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserAPI.Entities;
using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly User_DBContext _context;

        public UserDetailsRepository(User_DBContext context)
        {
            _context = context;
        }

        public async Task<List<UserDetails>> GetAllUserDetailsData()
        {
            return await _context.UserDetails.ToListAsync();
        }

        public async Task<List<UserDetails>> GetAllUserDetailsSPData()
        {

            return await _context.UserDetails.FromSqlRaw("sp_get_all_valid_records").ToListAsync();
        }

        public async ValueTask<UserDetails?> GetUserDetailsData(long id)
        {
            var userRecord = await _context.UserDetails.FindAsync(id);

            if (userRecord == null)
            {
                return null;
            }

            return userRecord;

        }

        public async Task<List<UserDetails>> GetUserDetailsDataByEmail(string email)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@email",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = email
                        }
            };

            return await _context.UserDetails.FromSqlRaw($"sp_get_record_by_email @email", param).ToListAsync();
        }

        public async Task<int> deleteUserData(long id)
        {
            return await _context.Database.ExecuteSqlRawAsync($"sp_change_status_code {id}");
        }

        public async Task<int> addUserData(String fName, string lName, string email)
        {
            return await _context.Database.ExecuteSqlRawAsync($"sp_insert_new_record '{fName}', '{lName}', '{email}'");
        }
    }
}
