using TestOnlienStore.Domain;
using TestOnlineStore.Persistence.Dto.ProductCategory.Queries;

namespace TestOnlineStore.Persistence.Repositories.Interfaces;

public interface IRepositoryProductCategory
{
    Task<List<AllProductCategory>> GetAllAsync();
    Task<DetailsProductCategory> GetDetailsByIdAsync(int id);
    Task<ProductCategory> GetByIdAsync(int id);
    Task<int> AddAsync(ProductCategory productCategory);
    Task DeleteAsync(int id);
    Task UpdateAsync(ProductCategory productCategory);
}