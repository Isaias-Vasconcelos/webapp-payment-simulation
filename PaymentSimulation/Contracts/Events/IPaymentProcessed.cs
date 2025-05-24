namespace Contracts.Events;

public interface IPaymentProcessed
{
    public Guid Id { get; }
    public string SocketId { get; }
    public string Email { get; }
    public string Status { get;}
    public decimal Amount { get;}
    public DateTime ProcessedDate { get;}
}