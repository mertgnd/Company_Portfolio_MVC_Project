using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concrete
{
	public class SocialMediaManager : ISocialMediaService
	{
		ISocialMediaDal _socialMediaDal;

		public SocialMediaManager(ISocialMediaDal socialMediaDal)
		{
			_socialMediaDal = socialMediaDal;
		}

		public void Delete(SocialMedia t)
		{
			_socialMediaDal.Delete(t);
		}

		public SocialMedia GetById(int id)
		{
			return _socialMediaDal.GetById(id);
		}

		public List<SocialMedia> GetListAll()
		{
			return _socialMediaDal.GetListAll();
		}

		public void Insert(SocialMedia t)
		{
			_socialMediaDal.Insert(t);
		}

		public void Update(SocialMedia t)
		{
			_socialMediaDal.Update(t);
		}
	}
}
