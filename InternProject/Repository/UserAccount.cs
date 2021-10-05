using Dapper;
using InternProject.DatabaseModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.Repository
{
    public class UserAccount : IUserAccount
    {
        private IDbConnection _db;

        public UserAccount(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
        }

        public int CreateUser(string Fullname, string Email, string Password)
        {
            var sql = @"INSERT INTO UserAccount (Fullname, Email, Password, Role) VALUES (@Fullname, @Email, @Password, @Role)";

            return _db.Execute(sql, new { Fullname = Fullname, Email = Email, Password = Password, Role = "Member" });
        }
        
        public User CheckUser(string Email, string Password)
        {
            var sql = @"SELECT * FROM UserAccount WHERE Email = @Email AND Password = @Password";

            return _db.QueryFirstOrDefault<User>(sql, new { Email = Email, Password = Password });
        }
    }
}
