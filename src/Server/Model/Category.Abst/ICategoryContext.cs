using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Category.Abst
{
    public interface ICategoryContext<TCategory> 
        where TCategory : class
    {
        DbSet<TCategory> Categories { get;set; }
    }
}
