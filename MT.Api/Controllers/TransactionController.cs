using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT.Api.Database;
using MT.Api.Database.Models;
using MT.Api.Helpers;
using MT.Shared.DataTransferModels;
using MT.Shared.DataTransferModels.Pagination;
using MT.Shared.DataTransferModels.Transaction;

namespace MT.Api.Controllers;

[ApiController]
public class TransactionController : ControllerBase
{
    private readonly MoneyTrackerDbContext _context;
    private readonly IHashids _hashids;

    public TransactionController(MoneyTrackerDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    [HttpPost("api/transactions")]
    public async Task<ApiResponse<PaginatedResponse<TransactionModel>>> GetAll(GetAllTransactionsRequestModel model)
    {
        IQueryable<TransactionModel> transactions = from transaction in _context.Transactions
                                                    from category in _context.Categories.Where(c => c.Id == transaction.CategoryId).DefaultIfEmpty()
                                                    where
                                                        transaction.DeletedAt.HasValue == false &&
                                                        category.DeletedAt.HasValue == false &&
                                                        (model.MinAmount.HasValue == false || transaction.Amount >= model.MinAmount) &&
                                                        (model.MaxAmount.HasValue == false || transaction.Amount <= model.MaxAmount) &&
                                                        (model.MinTransactionDate.HasValue == false || transaction.TransactionDate >= model.MinTransactionDate) &&
                                                        (model.MaxTransactionDate.HasValue == false || transaction.TransactionDate <= model.MaxTransactionDate) &&
                                                        (model.CategoryId.HasValue == false || category.Id == model.CategoryId)
                                                    select new TransactionModel
                                                    {
                                                        Id = transaction.Id,
                                                        Amount = transaction.Amount,
                                                        TransactionDate = transaction.TransactionDate,
                                                        Category = category.Title,
                                                        Description = transaction.Description,
                                                    };

        ApiResponse<PaginatedResponse<TransactionModel>> response = new ApiResponse<PaginatedResponse<TransactionModel>>
        {
            Data = await CommonHelpers.GetPaginatedResponse(transactions, t => t.TransactionDate, false, model.PageSize, model.PageNumber),
            StatusCode = HttpStatusCode.OK
        };

        foreach (TransactionModel item in response.Data.Data)
        {
            item.RelativeTransactionDate = item.TransactionDate.ToRelativeTime();
            item.HashId = _hashids.Encode(item.Id);
            item.Id = 0;
        }

        return response;
    }

    [HttpPost("api/transactions/save")]
    public async Task<ApiResponse> Save(SaveTransactionModel model)
    {
        // TODO - handle local date time here
        if (model.TransactionDate.HasValue && model.TransactionDate.Value > DateTime.UtcNow)
        {
            return new ApiResponse(HttpStatusCode.BadRequest, "Transaction date cannot be in future");
        }

        int? categoryId = null;
        if (string.IsNullOrEmpty(model.CategoryId) == false)
        {
            categoryId = _hashids.Decode(model.CategoryId).FirstOrDefault();

            bool isCategoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId && !c.DeletedAt.HasValue);
            if (isCategoryExists == false)
            {
                return new ApiResponse(HttpStatusCode.BadRequest, $"Category could not be found with \"{model.CategoryId}\" Id");
            }
        }

        if (string.IsNullOrEmpty(model.Id) == false)
        {
            int transactionId = _hashids.Decode(model.Id).FirstOrDefault();

            Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId && !t.DeletedAt.HasValue);
            if (transaction == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound, $"Transaction not found with \"{model.Id}\" Id");
            }

            transaction.Amount = model.Amount;
            transaction.TransactionDate = model.TransactionDate ?? transaction.TransactionDate;
            transaction.CategoryId = categoryId;
            transaction.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            Transaction transaction = new Transaction
            {
                Amount = model.Amount,
                TransactionDate = model.TransactionDate ?? DateTime.UtcNow,
                CategoryId = categoryId,
                Description = model.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
        }

        await _context.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.OK, "Transaction has been saved successfully");
    }

    [HttpDelete("api/transactions/{transactionId}")]
    public async Task<ApiResponse> Delete(string transactionId)
    {
        int id = _hashids.Decode(transactionId).FirstOrDefault();

        Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && !t.DeletedAt.HasValue);
        if (transaction == null)
        {
            return new ApiResponse(HttpStatusCode.NotFound, $"Transaction not found with \"{transactionId}\" Id");
        }

        transaction.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.OK, "Transaction has been deleted successfully");
    }
}
