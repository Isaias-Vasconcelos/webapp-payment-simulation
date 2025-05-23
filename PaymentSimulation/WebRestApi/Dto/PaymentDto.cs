namespace WebRestApi.Dto;

public record PaymentDto(Guid Id, string SocketId, CardInfoDto Card, decimal Amount, string Status);