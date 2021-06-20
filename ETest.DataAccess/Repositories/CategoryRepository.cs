using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Entities.Concrete;

namespace ETest.DataAccess.Repositories
{
    public class CategoryRepository:EfRepositoryBase<Category,ETestContext>,ICategoryDal
    {
        public CategoryRepository(ETestContext context) : base(context)
        {
        }
    }
}