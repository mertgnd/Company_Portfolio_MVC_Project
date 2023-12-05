using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfAddressDal : GenericRepository<Address>, IAddressDal
    {
    }
}
