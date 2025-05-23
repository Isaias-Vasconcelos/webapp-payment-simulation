namespace WebFrontend.Models;

public class CheckoutModel:BaseModel
{
    public ProductModel? Product { get; set; }
    public PaymentModel? Payment { get; set; }
}