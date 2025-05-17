using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oman_Public_Library_System.Model;

namespace Oman_Public_Library_System.Context
{

    public class ApplicationDbContext : DbContext
    {

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ZADJALI\\MSSQLSERVER02; database = Library ; Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Borrow_Record> Borrow_Records { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }




    }

}