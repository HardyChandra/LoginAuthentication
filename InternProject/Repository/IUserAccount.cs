using InternProject.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.Repository
{
    public interface IUserAccount
    {
        User CheckUser(string Email, string Password);
    }
}
