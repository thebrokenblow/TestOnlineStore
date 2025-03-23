using System.Net.Http.Json;
using TestConsoleAppOnlineStore;

var responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7257/api/v1/ProductCategory");
var productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<AllProductCategory>>();

foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

int id = int.Parse(Console.ReadLine());
var responseGetProductCategory= await new HttpClient().GetAsync($"https://localhost:7257/api/v1/ProductCategory/{id}");
var currentProductCategory = await responseGetProductCategory.Content.ReadFromJsonAsync<DetailsProductCategory>();

Console.WriteLine($"Id: {currentProductCategory.Id} Name: {currentProductCategory.Name} Description: {currentProductCategory.Description}");

id = int.Parse(Console.ReadLine());
await new HttpClient().DeleteAsync($"https://localhost:7257/api/v1/ProductCategory/{id}");

responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7257/api/v1/ProductCategory");
productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<AllProductCategory>>();

foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

var createProductCategory = new CreateProductCategory
{
    Name = "Телефон",
    Description = "Очень крутой Телефон"
};

responseGetProductCategories = await new HttpClient().PostAsJsonAsync("https://localhost:7257/api/v1/ProductCategory", createProductCategory);
var idProductCategory = await responseGetProductCategories.Content.ReadFromJsonAsync<int>();


responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7257/api/v1/ProductCategory");
productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<AllProductCategory>>();

foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

var updatedProductCategory = new ProductCategory
{
    Id = 14,
    Name = "Планшет",
    Description = "Крутой планшет"
};

responseGetProductCategories = await new HttpClient().PutAsJsonAsync("https://localhost:7257/api/v1/ProductCategory", updatedProductCategory);


responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7257/api/v1/ProductCategory");
productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<AllProductCategory>>();


foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

Console.ReadLine();