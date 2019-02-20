using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using odec.Category.DAL;
using odec.Framework.Infrastructure.Enumerations;
using odec.Framework.Logging;
using odec.Server.Model.Category.Contexts;
using odec.Server.Model.Category.Filters;
//using ICategoryRepo = odec.Category.DAL.Interop.ICategoryRepository<int, System.Data.Entity.DbContext, odec.Server.Model.Category.Category, odec.Server.Model.Category.Filters.CategoryFilter>;

namespace Category.DAL.Tests
{

    public class CategoryRepositoryTester:Tester<CategoryContext>
    {
        //public CategoryRepositoryTester()
        //{
        //    Database.SetInitializer<CategoryContext>(null);
            
        //}
        [Test]
        public void GetCategories()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    //
                    var repository =
                        new CategoryRepository(db);
                    var item = GenerateModel();
                    var item2 = GenerateModel();
                    item2.Name = "Test Unique term";
                    item2.Code = "Unique2";
                    var item3 = GenerateModel();
                    item3.Name = "Test Unique2";
                    item3.Code = "Unique3";
                    var item4 = GenerateModel();
                    item4.Name = "Test Unique terms";
                    item4.Code = "Unique4";
                    var item5 = GenerateModel();
                    item5.Name = "Test Unique2";
                    item5.Code = "Unique5";
                    var item6 = GenerateModel();
                    item6.Name = "Test Unique2";
                    item6.Code = "Unique6";
                    var item7 = GenerateModel();
                    item7.Name = "Test Unique2";
                    item7.Code = "Unique7";
                    item7.IsApproved = true;
                    item7.IsActive = false;
                    IEnumerable<odec.Server.Model.Category.Category> categories = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Save(item2));
                    Assert.DoesNotThrow(() => repository.Save(item3));
                    Assert.DoesNotThrow(() => repository.Save(item4));
                    Assert.DoesNotThrow(() => repository.Save(item5));
                    Assert.DoesNotThrow(() => repository.Save(item6));
                    Assert.DoesNotThrow(() => repository.Save(item7));
                    CategoryFilter filter = new CategoryFilter();
                    filter.Code = "Unique7";
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 1);
                    filter.Code = "Unique";
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories == null || !categories.Any());
                    filter.Code = null;
                    filter.IsOnlyApproved = true;
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null &&categories.Any() && categories.Count() == 1);
                    filter.IsOnlyApproved = false;
                    filter.IsOnlyActive = true;
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 6);
                    filter.IsOnlyActive = false;
                    filter.Name.Target = "Unique";
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 6);
                    filter.Name.Target = "Test Unique";
                    filter.Name.CompareOperation = StringCompareOperation.Prefix;
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 6);

                    filter.Name.Target = "Unique2";
                    filter.Name.CompareOperation = StringCompareOperation.Postfix;
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 4);
                    filter.Name.Target = "Test Unique terms";
                    filter.Name.CompareOperation = StringCompareOperation.Equals;
                    Assert.DoesNotThrow(() => categories = repository.Get(filter));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 1);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetForAutoComplete()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository =
                        new CategoryRepository(db);
                    var item = GenerateModel();
                    var item2 = GenerateModel();
                    item2.Name = "Test Unique term";
                    item2.Code = "Unique2";
                    var item3 = GenerateModel();
                    item3.Name = "Test Unique2";
                    item3.Code = "Unique3";
                    var item4 = GenerateModel();
                    item4.Name = "Test Unique terms";
                    item4.Code = "Unique4";
                    var item5 = GenerateModel();
                    item5.Name = "Test Unique2";
                    item5.Code = "Unique5";
                    var item6 = GenerateModel();
                    item6.Name = "Test Unique2";
                    item6.Code = "Unique6";
                    var item7 = GenerateModel();
                    item7.Name = "Test Unique2";
                    item7.Code = "Unique7";
                    IEnumerable<odec.Server.Model.Category.Category> categories = null;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Save(item2));
                    Assert.DoesNotThrow(() => repository.Save(item3));
                    Assert.DoesNotThrow(() => repository.Save(item4));
                    Assert.DoesNotThrow(() => repository.Save(item5));
                    Assert.DoesNotThrow(() => repository.Save(item6));
                    Assert.DoesNotThrow(() => repository.Save(item7));
                    Assert.DoesNotThrow(() => categories = repository.GetForAutoComplete("Test"));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 7);
                    Assert.DoesNotThrow(() => categories =repository.GetForAutoComplete("Test",5));
                    Assert.True(categories!= null && categories.Any() && categories.Count()==5);
                    Assert.DoesNotThrow(() => categories =repository.GetForAutoComplete("Test Unique"));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 6);
                    Assert.DoesNotThrow(() => categories = repository.GetForAutoComplete("Test Unique2"));
                    Assert.True(categories != null && categories.Any() && categories.Count() == 4);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        private odec.Server.Model.Category.Category GenerateModel()
        {
            return new odec.Server.Model.Category.Category
            {
                Name = "Test 2",
                Code = "Test 2",
                IsActive = true,
                DateCreated = DateTime.Now,
                SortOrder = 0,
                IsApproved = false
            };
        }

        [Test]
        public void SaveCategory()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository =
                        new CategoryRepository(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeleteCategory()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);;
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeleteCategoryById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item.Id));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateCategory()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Deactivate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateCategoryById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                
                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Deactivate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateCategory()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Activate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateCategoryById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Activate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetCategoryById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new CategoryContext(options))
                {
                    
                    var repository = new CategoryRepository(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));

                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}
