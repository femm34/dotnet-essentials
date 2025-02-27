using System.Net;

namespace MiApi.Models.DTOs;

public class ApiResponse<T>
{
    public string Message { get; set; }
    public string Status { get; set; }
    public T Data { get; set; }
    public int Code { get; set; }
}
