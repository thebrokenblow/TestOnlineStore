using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestOnlineStore.Persistence.Dto.Product.Commands;
using TestOnlineStore.Persistence.Dto.Product.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IRepositoryProduct repositoryProduct, IValidator<CreateProduct> validatorProduct) : ControllerBase
{
    [HttpGet]
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var products = await repositoryProduct.GetAllAsync();

        return products;
    }

    [HttpGet("{id}")]
    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await repositoryProduct.GetDetailsAsync(id);

        return product;
    }

    [HttpGet("{countSkip}/{countTake}")]
    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeProducts = await repositoryProduct.GetRangeAsync(countSkip, countTake);

        return rangeProducts;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddAsync([FromBody] CreateProduct createProduct)
    {
        validatorProduct.ValidateAndThrow(createProduct);
        var id = await repositoryProduct.AddAsync(createProduct);

        return Ok(id);
    }

    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdateProduct updateProduct)
    {
        await repositoryProduct.UpdateAsync(updateProduct);
    }

    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryProduct.DeleteAsync(id);
    }
}