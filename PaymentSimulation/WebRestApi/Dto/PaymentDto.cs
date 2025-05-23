namespace WebRestApi.Dto;

public record PaymentDto(Guid Id, string SocketId, CardInfoDto card, decimal Amount, string Status);