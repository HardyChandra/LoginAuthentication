using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.DataContext
{
    public class InternProjectContext : DbContext
    {
        public InternProjectContext(DbContextOptions<InternProjectContext> options) : base(options)
        {

        }
    }
}
