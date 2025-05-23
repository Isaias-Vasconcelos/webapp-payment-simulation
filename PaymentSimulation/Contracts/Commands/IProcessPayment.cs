namespace Contracts.Commands;

public interface IProcessPayment
{
    public Guid Id { get; }
    public string SocketId { get; set; }
    public string CardNumber { get;}
    public string CardName { get;}
    public string CardExpiry { get;}
    public string CardCvv { get;}
    public decimal Amount { get;}
}