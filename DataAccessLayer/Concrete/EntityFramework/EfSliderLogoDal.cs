using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfSliderLogoDal : GenericRepository<SliderLogo>, ISliderLogoDal
    {
    }
}