using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concrete
{
    public class GalleryManager : IGalleryService
    {
        private readonly IGalleryDal _galleryDal;

        public GalleryManager(IGalleryDal galleryDal)
        {
            _galleryDal = galleryDal;
        }

        public void Delete(Gallery t)
        {
            _galleryDal.Delete(t);
        }

        public Gallery GetById(int id)
        {
            return _galleryDal.GetById(id);
        }

        public List<Gallery> GetListAll()
        {
            return _galleryDal.GetListAll();
        }

        public void Insert(Gallery t)
        {
            _galleryDal.Insert(t);
        }

        public void Update(Gallery t)
        {
            _galleryDal.Update(t);
        }
    }
}
