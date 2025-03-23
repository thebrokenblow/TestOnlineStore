using Microsoft.EntityFrameworkCore;
using TestOnlienStore.Domain;
using TestOnlineStore.Persistence.Common.Exceptions;
using TestOnlineStore.Persistence.Dto.Product.Commands;
using TestOnlineStore.Persistence.Dto.Product.Queries;
using TestOnlineStore.Persistence.Dto.ProductCategory.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence.Repositories;

public class RepositoryProduct(TestOnlineStoreDBContext context, IRepositoryProductCategory repositoryProductCategory) : IRepositoryProduct
{
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var products = await context.Products
            .Select(product => new AllProduct
            { 
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            })
            .AsNoTracking()
            .ToListAsync();

        return products;
    }

    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var products = await context.Products
            .Select(product => new RangeProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            })
            .AsNoTracking()
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync();

        return products;
    }

    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await context.Products
            .Include(product => product.ProductCategory)
            .Select(product => new DetailsProduct
            { 
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdProductCategory = product.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductCategoryDescription = product.ProductCategory.Description,

            })
            .AsNoTracking()
            .SingleOrDefaultAsync(product => product.Id == id)
            ?? throw new NotFoundException(nameof(Product), id);

        return product;
    }

    public async Task<int> AddAsync(CreateProduct createProduct)
    {
        var productCategory = await repositoryProductCategory.GetByIdAsync(createProduct.IdProductCategory);

        var product = new Product
        {
            Name = createProduct.Name,
            Description = createProduct.Description,
            Price = createProduct.Price,
            ProductCategory = productCategory,
        };

        await context.AddAsync(product);
        await context.SaveChangesAsync();

        return product.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);

        context.Remove(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateProduct updateProduct)
    {
        var updatedProduct = await GetByIdAsync(updateProduct.Id);

        updatedProduct.Name = updateProduct.Name;
        updatedProduct.Description = updateProduct.Description;
        updatedProduct.Price = updateProduct.Price;

        var productCategory = await context.ProductCategories.SingleOrDefaultAsync(productCategory => productCategory.Id == updateProduct.IdProductCategory)
            ?? throw new NotFoundException(nameof(ProductCategory), updateProduct.IdProductCategory);

        updatedProduct.ProductCategory = productCategory;

        await context.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await context.Products.SingleOrDefaultAsync(product => product.Id == id) 
            ?? throw new NotFoundException(nameof(Product), id);

        return product;
    }
}