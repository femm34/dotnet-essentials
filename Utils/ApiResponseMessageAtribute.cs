namespace MiApi.Utils;

using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ApiResponseMessageAtribute : Attribute
{
    public string Message { get; }

    public ApiResponseMessageAtribute(string message)
    {
        Message = message;
    }
}