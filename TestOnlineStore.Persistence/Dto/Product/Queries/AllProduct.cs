﻿namespace TestOnlineStore.Persistence.Dto.Product.Queries;

public class AllProduct
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}
