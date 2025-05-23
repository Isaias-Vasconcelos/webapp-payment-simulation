namespace WebRestApi.Dto;

public record PaymentDto(Guid Id, string? SocketId,string? ProductId, CardDto Card, string? Status);