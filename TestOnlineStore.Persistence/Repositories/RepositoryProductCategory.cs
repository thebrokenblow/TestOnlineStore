using Microsoft.EntityFrameworkCore;
using TestOnlienStore.Domain;
using TestOnlineStore.Persistence.Common.Exceptions;
using TestOnlineStore.Persistence.Dto.ProductCategory.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence.Repositories;

public class RepositoryProductCategory(TestOnlineStoreDBContext context) : IRepositoryProductCategory
{
    public async Task<List<AllProductCategory>> GetAllAsync()
    {
        var productCategories = await context.ProductCategories
            .Select(productCategory => new AllProductCategory
            { 
                Id = productCategory.Id,
                Name = productCategory.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return productCategories;
    }

    public async Task<DetailsProductCategory> GetDetailsByIdAsync(int id)
    {
        var productCategory = await context.ProductCategories
            .Select(productCategory => new DetailsProductCategory
            { 
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(productCategory => productCategory.Id == id)
            ?? throw new NotFoundException(nameof(ProductCategory), id);

        return productCategory;
    }

    public async Task<int> AddAsync(ProductCategory productCategory)
    {
        await context.AddAsync(productCategory);
        await context.SaveChangesAsync();

        return productCategory.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var productCategory = await GetByIdAsync(id);

        context.ProductCategories.Remove(productCategory);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductCategory productCategory)
    {
        var updatedProductCategory = await GetByIdAsync(productCategory.Id);

        updatedProductCategory.Name = productCategory.Name;
        updatedProductCategory.Description = productCategory.Description;

        await context.SaveChangesAsync();
    }

    public async Task<ProductCategory> GetByIdAsync(int id)
    {
        var productCategory = await context.ProductCategories.SingleOrDefaultAsync(productCategory => productCategory.Id == id)
            ?? throw new NotFoundException(nameof(ProductCategory), id);

        return productCategory;
    }
}