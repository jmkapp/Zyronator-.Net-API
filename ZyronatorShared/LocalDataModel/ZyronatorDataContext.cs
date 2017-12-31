using System.Data.Entity;

namespace ZyronatorShared.LocalDataModel
{
    internal class ZyronatorDataContext : DbContext
    {
        //internal ZyronatorDataContext()
        //    : base(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString)
        //{

        //}

        internal ZyronatorDataContext(string connectionString)
            : base(connectionString)
        {
            ApplicationUsers = Set<ApplicationUser>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ZyronatorDataContext>(null);
        }

        internal DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
