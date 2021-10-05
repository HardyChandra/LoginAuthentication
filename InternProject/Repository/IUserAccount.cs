using InternProject.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.Repository
{
    public interface IUserAccount
    {
        int CreateUser(string Fullname, string Email, string Password);
        User CheckEmail(string Email);
        User CheckUser(string Email, string Password);
    }
}
