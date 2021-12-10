using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class ExpenseDBContext : DbContext
    {
        public virtual DbSet<ExpenseReport> ExpenseReport { get; set; }
        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options) : base(options)
        {

        }

        public ExpenseDBContext()
        {
        }
    }
}
