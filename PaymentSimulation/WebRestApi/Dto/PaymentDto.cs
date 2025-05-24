namespace WebRestApi.Dto;

public record PaymentDto(Guid Id, string? SocketId,Guid ProductId, CardDto Card, string? Status);