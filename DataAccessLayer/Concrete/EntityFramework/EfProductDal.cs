using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
    }
}