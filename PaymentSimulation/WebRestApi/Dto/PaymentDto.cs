namespace WebRestApi.Dto;

public record PaymentDto(Guid Id, string SocketId, string Email, Guid ProductId, CardDto Card, string? Status);