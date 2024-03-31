namespace ScroogeApp.Attributes.Http;

using ScroogeApp.Attributes.Http.Base;

public class HttpGetAttribute : HttpAttribute
{
    public HttpGetAttribute() : base("GET")
    {
    }
}