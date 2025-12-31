using App.Domain.Core._common;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.CategoryAgg;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.CategoryAgg.Service
{
    public class CategoryService(ICategoryRepository _categoryRepository) : ICategoryService
    {
    

        public async Task<Result<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll(cancellationToken);

            if (categories == null || !categories.Any())
            {
                return Result<List<CategoryDto>>.Failure("هیچ دسته‌بندی در سیستم یافت نشد.");
            }

            return Result<List<CategoryDto>>.Success(categories);
        }

        public async Task<Result<bool>> Update(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var isUpdated = await _categoryRepository.Update(categoryDto, cancellationToken);

            if (isUpdated)
            {
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
                return Result<int>.Success(affectedRows, "دسته‌بندی جدید با موفقیت ایجاد شد.");
            }

            return Result<int>.Failure("خطایی در ذخیره‌سازی دسته‌بندی رخ داد.");
        }
    }
}
