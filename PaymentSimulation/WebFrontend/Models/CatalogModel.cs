namespace WebFrontend.Models;

public class CatalogModel:BaseModel
{
    public IEnumerable<ProductModel>? Products { get; set; }
}