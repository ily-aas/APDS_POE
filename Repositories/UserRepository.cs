using APDS_POE.Models;
using APDS_POE.Models.System;
using APDS_POE.Services;
using static APDS_POE.Models.System.Enums;

namespace APDS_POE.Repositories
{
 
    public interface IUserRepository
    {
        public AppResponse Login(string Username, string Password, bool IsAdmin = false);
    }
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext DB;

        public UserRepository(AppDbContext dbContext)
        {
            DB = dbContext;
        }

        public AppResponse Login(string Username, string Password, bool IsAdmin = false)
        {

            User? user;
            AppResponse response = new AppResponse();

            try
            {
                if (IsAdmin)
                {
                    user = DB.User.Where(x => x.Username == Username && x.Password == Password && (UserRole)x.UserRole == UserRole.Employee).FirstOrDefault();
                }
                else
                {
                    user = DB.User.Where(x => x.Username == Username && x.Password == Password && (UserRole)x.UserRole == UserRole.Farmer).FirstOrDefault();
                }

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = IsAdmin ? "Admin credentials invalid" : "User credentials invalid";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = IsAdmin ? "Admin login successful" : "User login successful";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = IsAdmin ? "Admin credentials invalid" : "User credentials invalid";
                return response;
            }

        }

    }
}
