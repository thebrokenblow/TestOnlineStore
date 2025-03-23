namespace TestOnlienStore.Domain;

public class ProductCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
