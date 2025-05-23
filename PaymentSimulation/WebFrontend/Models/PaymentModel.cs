namespace WebFrontend.Models;

public class PaymentModel
{
    public Guid Id { get; set; }
    public string? SocketId { get; set; }
    public Guid ProductId { get; set; }
    public CardInfoModel? Card { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
}