using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StajApiDersi.Models.Concrete;
using StajApiDersi.Repositories.Abstract;

namespace StajApiDersi.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _dbContext;
        public CategoryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(Category item)
        {
            var category=_dbContext.Set<Category>().Add(item);
            return IsSuccess(category);
            
        }

        public bool Edit(Category item)
        {
            return IsSuccess(_dbContext.Set<Category>().Update(item));
        }

        public List<Category> GetAll()
        {
            return _dbContext.Set<Category>().ToList();
        }

        public Category GetById(int id)
        {
            return _dbContext.Set<Category>().Find(id);
        }

        public bool Remove(Category item)
        {
            return IsSuccess(_dbContext.Set<Category>().Remove(item));
        }
        bool IsSuccess(EntityEntry entity)
        {
            if (entity==null)
            {
                return false;
            }
            var ess=_dbContext.SaveChanges(); //SaveChanges'ın try-catch içerisinde yapılması daha iyi olur.
            return ess > 0 ? true : false;
        }
    }
}
