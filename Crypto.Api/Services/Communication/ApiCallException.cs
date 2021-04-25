using System;

public class ApiCallException : Exception
{
    public ApiCallException(string url, string message)
        : base(message)
    {
        Url = url;
    }

    public string Url { get; }
}