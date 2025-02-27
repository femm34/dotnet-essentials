using MiApi.Models.DTOs;

namespace MiApi.Utils;

public abstract class BuilderApiResponse<T>
{
    public static ApiResponse<T> Build(string message, string status, T data, int code)
    {
        return new ApiResponse<T>
        {
            Message = message,
            Status = status,
            Data = data,
            Code = code
        };
    }
}