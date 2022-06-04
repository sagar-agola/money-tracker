using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT.Api.Database;
using MT.Api.Database.Models;
using MT.Shared.DataTransferModels;
using MT.Shared.DataTransferModels.Category;

namespace MT.Api.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private readonly MoneyTrackerDbContext _context;
    private readonly IHashids _hashids;

    public CategoryController(MoneyTrackerDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    [HttpGet("api/categories")]
    public async Task<ApiResponse<List<CategoryModel>>> GetAll()
    {
        List<CategoryModel> categories = await _context.Categories
            .Where(c => c.DeletedAt.HasValue == false)
            .Select(c => new CategoryModel
            {
                Id = c.Id,
                Title = c.Title
            }).ToListAsync();

        foreach (CategoryModel category in categories)
        {
            category.HashId = _hashids.Encode(category.Id);
            category.Id = 0;
        }

        return new ApiResponse<List<CategoryModel>>
        {
            Data = categories,
            StatusCode = HttpStatusCode.OK
        };
    }

    [HttpPost("api/categories")]
    public async Task<ApiResponse> Save(CategoryModel model)
    {
        Category category;

        if (string.IsNullOrEmpty(model.HashId) == false)
        {
            int categoryId = _hashids.Decode(model.HashId).FirstOrDefault();

            category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.DeletedAt.HasValue == false);
            if (category == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound, $"Category not found with \"{model.HashId}\" Id");
            }

            bool isDuplicate = await _context.Categories.AnyAsync(c => c.Id != categoryId && c.Title == model.Title && c.DeletedAt.HasValue == false);
            if (isDuplicate)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, $"Category already exists with \"{model.Title}\" Name");
            }

            category.Title = model.Title;
            category.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            bool isDuplicate = await _context.Categories.AnyAsync(c => c.Title == model.Title && c.DeletedAt.HasValue == false);
            if (isDuplicate)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, $"Category already exists with \"{model.Title}\" Name");
            }

            category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(category);
        }

        await _context.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.OK, "Category saved successfully");
    }

    [HttpDelete("api/categories/{categoryId}")]
    public async Task<ApiResponse> Delete(string categoryId)
    {
        int id = _hashids.Decode(categoryId).FirstOrDefault();

        Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt.HasValue == false);
        if (category == null)
        {
            return new ApiResponse(HttpStatusCode.NotFound, $"Category not found with \"{categoryId}\" Id");
        }

        category.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.OK, "Category deleted successfully");
    }
}
