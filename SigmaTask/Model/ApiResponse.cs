namespace SigmaTask.Model;

public class ApiResponse<T> where T : class
{
    public string? Message { get; set; }
    public T? Data { get; set; }
}
