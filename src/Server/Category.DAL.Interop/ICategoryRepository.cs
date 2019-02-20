using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.Category.DAL.Interop
{
    public interface ICategoryRepository<in TKey, TContext, TCategory, in TCategoryFilter> :
        IEntityOperations<TKey, TCategory>,
        IActivatableEntity<TKey, TCategory>,
        IContextRepository<TContext>
        where TKey : struct 
        where TCategory : class
    {
        IEnumerable<TCategory> Get(TCategoryFilter filter);
        IEnumerable<TCategory> GetForAutoComplete(string term, int? maxRows = null);

        void ApproveCategory(TCategory category);
    }
}