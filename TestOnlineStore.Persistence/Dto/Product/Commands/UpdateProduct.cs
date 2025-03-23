namespace TestOnlineStore.Persistence.Dto.Product.Commands;

public class UpdateProduct
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int IdProductCategory { get; set; }
}
