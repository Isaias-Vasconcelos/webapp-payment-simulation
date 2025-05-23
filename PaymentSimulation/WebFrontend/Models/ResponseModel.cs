namespace WebFrontend.Models;

public class ResponseModel<T>:BaseModel
{
    public IEnumerable<T>? Entities { get; set; } = [];
}