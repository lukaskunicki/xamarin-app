using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApi.Model.Business;

namespace WebApi.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Priority> Priority { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Sprint> Sprint { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
    }
}