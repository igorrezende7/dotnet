using Microsoft.AspNetCore.Mvc;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => "Igor Rezende");
app.MapPost("/user", () => new {Name = "Igor", Age=23});
app.MapGet("/AddHeader", (HttpResponse response) =>  {
    response.Headers.Add("teste", "igor");
    return "Olá";
});
app.MapPost("/products", (Product product) =>
{
    ProductRespository.Add(product);
    return product;
});



app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRespository.GetBy(code);
    return product;
});



app.MapPut("/products", (Product product) =>
{
    var productSaved = ProductRespository.GetBy(product.Code);
    productSaved.Name = product.Name;

});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var productSaved = ProductRespository.GetBy(code);
    ProductRespository.Remove(productSaved);
});








app.Run();


public static class ProductRespository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product) { 
        if(Products == null)
            Products = new List<Product>();
        Products.Add(product);
        
    }

    public static Product GetBy(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }
    
    public static void Remove(Product product)
    {
        Products.Remove(product);
    }
}

public class Product
{
    public string Code { get; set; }

    public string Name { get; set; }
}


