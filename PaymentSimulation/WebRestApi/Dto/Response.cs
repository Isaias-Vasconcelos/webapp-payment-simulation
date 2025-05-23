namespace WebRestApi.Dto;

public class Response<T> where T : class
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; } = "Operation carried out successfully!";
    public IEnumerable<T>? Entities { get; set; } = [];

    public Response(IEnumerable<T> data)
    {
        Entities = data;
    }
    public Response(T entity)
    {
        Entities = [entity];
    }

    public Response(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}