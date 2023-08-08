using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.AdminPanel.ViewModels.Category;
using Cargo.Core.DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(AddCategoryViewModel viewModel)
        {
            var model = viewModel.Category;
            var category = _mapper.Map(model);

            _unitOfWork.CategoryRepository.Add(category);
        }

        public void Delete(int id)
        {
            _unitOfWork.CategoryRepository.Get(id);           

            _unitOfWork.CategoryRepository.Delete(id);
        }

        public AddCategoryViewModel Get(int id)
        {            
            var category = _unitOfWork.CategoryRepository.Get(id);

            var model = _mapper.Map(category);

            var viewModel = new AddCategoryViewModel();
            viewModel.Category = model;

            return viewModel;
        }

        public IList<CategoryModel> GetAll()
        {
            var countries = _unitOfWork.CategoryRepository.GetAll();

            var viewModel = new CategoryViewModel();

            viewModel.Categories = new List<CategoryModel>();

            foreach (var category in countries)
            {
                var model = _mapper.Map(category);

                viewModel.Categories.Add(model);
            }

            return viewModel.Categories;
        }

        public void Update(AddCategoryViewModel viewModel)
        {
            var model = viewModel.Category;
            var category = _mapper.Map(model);

            _unitOfWork.CategoryRepository.Update(category);
        }

        public bool IsExists(CategoryModel model)
        {
            var categoryName = _unitOfWork.CategoryRepository.GetByName(model.Name);

            if (categoryName == null)
            {
                return false;
            }

            return true;
        }

        public int GetTotalCategoryCount()
        {
            return _unitOfWork.CategoryRepository.GetTotalCount();
        }
    }
}
