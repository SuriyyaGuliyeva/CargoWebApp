using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class TotalCountService : ITotalCountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TotalCountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetCategoryCount()
        {
            return _unitOfWork.CategoryRepository.GetTotalCount();
        }

        public int GetCountryCount()
        {
            return _unitOfWork.CountryRepository.GetTotalCount();
        }

        public int GetShopCount()
        {
            return _unitOfWork.ShopRepository.GetTotalCount();
        }

        public int GetUserCount()
        {
            return _unitOfWork.UserRepository.GetTotalCount();
        }
    }
}
