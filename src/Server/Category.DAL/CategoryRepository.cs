using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Category.DAL.Interop;
using odec.Entity.DAL;
using odec.Framework.Infrastructure.Enumerations;
using odec.Framework.Logging;
using odec.Framework.Extensions;
using odec.Server.Model.Category.Filters;

namespace odec.Category.DAL
{
    public class CategoryRepository : OrmEntityOperationsRepository<int, Server.Model.Category.Category, DbContext>, ICategoryRepository<int, DbContext, Server.Model.Category.Category, CategoryFilter>
    {
        public CategoryRepository() { }

        public CategoryRepository(DbContext db)
        {
            Db = db;
        }
        public void SetConnection(string connection)
        {
           throw new NotImplementedException("Not available in EF7");
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public IEnumerable<Server.Model.Category.Category> Get(CategoryFilter filter)
        {
            try
            {
                var query = Db.Set<Server.Model.Category.Category>()
                    .Where(it => (!filter.IsOnlyApproved || (it.IsApproved)) && (!filter.IsOnlyActive || (it.IsActive)));

                if (!string.IsNullOrEmpty(filter.Name?.Target))
                {
                    switch (filter.Name.CompareOperation)
                    {
                        case StringCompareOperation.Postfix:
                            query = query.Where(it => it.Name.EndsWith(filter.Name.Target));
                            break;
                        case StringCompareOperation.Prefix:
                            query = query.Where(it => it.Name.StartsWith(filter.Name.Target));
                            break;
                        case StringCompareOperation.Equals:
                            query = query.Where(it => it.Name == filter.Name.Target);
                            break;
                        default:
                            query = query.Where(it => it.Name.ToLower().Contains(filter.Name.Target.ToLower()));
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(filter.Code))
                    query = query.Where(it => it.Code == filter.Code);
                if (!string.IsNullOrEmpty(filter.Sidx))
                    query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(filter.Sidx)
                    : query.OrderBy(filter.Sidx);
                if (filter.Page != 0 && filter.Rows !=0)
                    return query.Skip(filter.Rows * (filter.Page - 1)).Take(filter.Rows);

                return query;

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Server.Model.Category.Category> GetForAutoComplete(string term, int? maxRows = null)
        {
            try
            {
                var query = Db.Set<Server.Model.Category.Category>().Where(it => it.Name.StartsWith(term));
                if (maxRows.HasValue)
                    query = query.Take(maxRows.Value);
                return query;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public void ApproveCategory(Server.Model.Category.Category category)
        {
            try
            {
                var cat = GetById(category.Id);
                cat.IsApproved = true;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
