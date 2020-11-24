using DAL;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context:DbContext
    {

        public Context() : base("name=CS") { }
    public DbSet<Users> Users { set; get; }
        public DbSet<Likes> Likes { set; get; }
        public DbSet<Messages> Messages { set; get; }
        public DbSet<Notifications> Notifications { set; get; }
        public DbSet<Posts> Posts { set; get; }
        public DbSet<Comments> Comments { set; get; }
       
        public DbSet<Follows> Follows { set; get; }
       
    }
}
