using FluentValidation;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TestOnlienStore.Domain;
using TestOnlineStore.Persistence.Dto.ProductCategory.Commands;
using TestOnlineStore.Persistence.Dto.ProductCategory.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;
using Asp.Versioning;

namespace TestOnlineStore.WebApi.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductCategoryController(IRepositoryProductCategory repositoryProductCategory, IValidator<CreateProductCategory> validatorProductCategory) : ControllerBase
{
    /// <summary>
    /// Gets product category by id
    /// </summary>
    /// <remarks>
    /// Sample request: 
    /// GET /api/productCategories/5
    /// </remarks>
    /// <returns>
    /// returns product category (productCategory)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="404">If product category is not found</response>
    [HttpGet("{id}", Name = "GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailsProductCategory> GetByIdAsync(int id)
    {
        var productCategory = await repositoryProductCategory.GetDetailsByIdAsync(id);

        return productCategory;
    }

    /// <summary>
    /// Creates product category
    /// </summary>
    /// <remarks>
    /// Sample request: 
    /// POST /api/productCategories
    /// {
    ///     name: "name of product category",
    ///     description: "description of product category",
    /// }
    /// </remarks>
    /// <param name="createProductCategory">CreateProductCategory object</param>
    /// <returns>
    /// returns id (int)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// If name is empty or length exceeds 100 character,
    /// If description is empty or length exceeds 400 character,
    /// </response>
    [HttpPost(Name = "Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> AddAsync([FromBody] CreateProductCategory createProductCategory)
    {
        validatorProductCategory.ValidateAndThrow(createProductCategory);

        var productCategory = new ProductCategory
        {
            Name = createProductCategory.Name,
            Description = createProductCategory.Description
        };

        var id = await repositoryProductCategory.AddAsync(productCategory);

        return Ok(id);
    }

    [HttpPut(Name = "Update")]
    public async Task UpdateAsync([FromBody] ProductCategory productCategory)
    {
        await repositoryProductCategory.UpdateAsync(productCategory);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        await repositoryProductCategory.DeleteAsync(id);
    }
}