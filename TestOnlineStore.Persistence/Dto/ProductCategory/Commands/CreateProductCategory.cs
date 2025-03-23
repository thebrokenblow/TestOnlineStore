namespace TestOnlineStore.Persistence.Dto.ProductCategory.Commands;

public class CreateProductCategory
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
