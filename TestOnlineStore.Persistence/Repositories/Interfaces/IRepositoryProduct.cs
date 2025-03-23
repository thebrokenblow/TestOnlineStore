using TestOnlienStore.Domain;
using TestOnlineStore.Persistence.Dto.Product.Commands;
using TestOnlineStore.Persistence.Dto.Product.Queries;

namespace TestOnlineStore.Persistence.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<List<AllProduct>> GetAllAsync();
    Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake);
    Task<DetailsProduct> GetDetailsAsync(int id);
    Task<int> AddAsync(CreateProduct product);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateProduct product);
    Task<Product> GetByIdAsync(int id);
}
