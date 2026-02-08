using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using App.Infra.Cache;
using App.Infra.Cache.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Service
{
    public class CategoryService(ICategoryRepository _categoryRepository , ICategoryRepositoryDapper _categoryRepositoryDapper , ICacheService _cacheService) : ICategoryService
    {
        public async Task<Result<CategoryPagedDto>> GetAll(int pageSize, int pageNumber, string? search, CancellationToken cancellationToken)
        {

          var allCategoryDtos= await _categoryRepositoryDapper.GetAll(cancellationToken);

            if (allCategoryDtos == null || !allCategoryDtos.Any())
            {
                return Result<CategoryPagedDto>.Failure("داده‌ای یافت نشد.");
            }

            IEnumerable<CategoryDto> filteredData = allCategoryDtos;
            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredData = filteredData.Where(c => c.Title.Contains(search));
            }

            var totalCount = filteredData.Count();
            var pagedData = filteredData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return Result<CategoryPagedDto>.Success(new CategoryPagedDto
            {
                CategoryDtos = pagedData,
                TotalCount = totalCount
            }, "عملیات با موفقیت انجام شد.");


        }
        public async Task<Result<CategoryWithHomeServicesPagedDto>> GetAll(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var categoryWithHomeServicesDtos = await  _categoryRepository.GetAllWithHomeServices(cancellationToken);
            if (categoryWithHomeServicesDtos == null || !categoryWithHomeServicesDtos.Any())
            {
                return Result<CategoryWithHomeServicesPagedDto>.Failure("داده‌ای یافت نشد.");
            }
            IEnumerable<CategoryWithHomeServices> filteredData = categoryWithHomeServicesDtos;
            var totalCount = filteredData.Count();

            var pagedData = filteredData.
                Skip((pageNumber - 1) * pageSize) .Take(pageSize).ToList();

            return Result<CategoryWithHomeServicesPagedDto>.Success(new CategoryWithHomeServicesPagedDto
            {
                CategoryWithHomeservicesDtos = pagedData,
                TotalCount = totalCount
            }, "عملیات با موفقیت انجام شد.");
        }

        public async Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {

             var categoryDtos = await _categoryRepositoryDapper.GetAll(cancellationToken);

             if (categoryDtos == null || categoryDtos.Count == 0)
             {
                 return Result<List<CategoryDto>>.Failure("دسته‌بندی‌ای یافت نشد");
             }

             return Result<List<CategoryDto>>.Success(categoryDtos);
           
        }

        public async Task<Result<bool>> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var isUpdated = await _categoryRepository.Update(categoryDto, cancellationToken);

            if (isUpdated)
            {
                _cacheService.Remove(CacheKeys.Categories);
                return Result<bool>.Success(true, "دسته بندی با موفقیت بروزرسانی شد.");
            }

            return Result<bool>.Failure("عملیات بروزرسانی با شکست مواجه شد.");
        }

        public async Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
           
            
            var category = await _categoryRepository.GetById(id, cancellationToken);

            if (category == null)
            {
                return Result<CategoryDto>.Failure("دسته‌بندی مورد نظر یافت نشد.");
            }

            return Result<CategoryDto>.Success(category);
        }

        public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _categoryRepository.Delete(id, cancellationToken);

            if (isDeleted)
            {
                _cacheService.Remove(CacheKeys.Categories);
                return Result<bool>.Success(true, "دسته‌بندی با موفقیت حذف (غیرفعال) شد.");
            }

            return Result<bool>.Failure("عملیات حذف با شکست مواجه شد. ممکن است دسته‌بندی یافت نشده باشد.");
        }

        public async Task<Result<int>> Create(string title, string imagePath, CancellationToken cancellationToken)
        {

            if (imagePath==null)
            {
                return Result<int>.Failure("کتگوری باید تصویر داشته باشد.");
            }
            var affectedRows = await _categoryRepository.Create(title, imagePath, cancellationToken);

            if (affectedRows > 0)
            {
                _cacheService.Remove(CacheKeys.Categories);
                return Result<int>.Success(affectedRows, "دسته‌بندی جدید با موفقیت ایجاد شد.");
            }

            return Result<int>.Failure("خطایی در ذخیره‌سازی دسته‌بندی رخ داد.");
        }

       
    }
}
