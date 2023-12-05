using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Concrete
{
    public class SliderLogoManager : ISliderLogoService
    {
        private readonly ISliderLogoDal _sliderLogoDal;

        public SliderLogoManager(ISliderLogoDal sliderLogoDal)
        {
            _sliderLogoDal = sliderLogoDal;
        }

        public void Delete(SliderLogo t)
        {
            _sliderLogoDal.Delete(t);
        }

        public SliderLogo GetById(int id)
        {
            return _sliderLogoDal.GetById(id);
        }

        public List<SliderLogo> GetListAll()
        {
            return _sliderLogoDal.GetListAll();
        }

        public void Insert(SliderLogo t)
        {
            _sliderLogoDal.Insert(t);
        }

        public void Update(SliderLogo t)
        {
            _sliderLogoDal.Update(t);
        }
    }
}