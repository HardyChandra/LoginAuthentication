using Dapper;
using InternProject.DatabaseModel;
using InternProject.DataContext;
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

        public User CheckEmail(string Email)
        {
            var sql = @"SELECT * FROM UserAccount Where Email = @Email";

            return _db.QueryFirstOrDefault<User>(sql, new { Email = Email });
        }
        
        public User CheckPassword(string Password)
        {
            var sql = @"SELECT * FROM UserAccount WHERE Password = @Password";

            return _db.QueryFirstOrDefault<User>(sql, new { Password = Password });
        }
    }
}
