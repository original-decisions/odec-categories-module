using Microsoft.EntityFrameworkCore;
using odec.Server.Model.Category.Abst;

namespace odec.Server.Model.Category.Contexts
{
    public class CategoryContext:DbContext, ICategoryContext<Category>
    {

        private string CategoryScheme = "dbo";
        public CategoryContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories", CategoryScheme);
            base.OnModelCreating(modelBuilder);
        }
    }
}
