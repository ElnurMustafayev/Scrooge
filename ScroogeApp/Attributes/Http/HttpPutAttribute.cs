namespace ScroogeApp.Attributes.Http;

using ScroogeApp.Attributes.Http.Base;

public class HttpPutAttribute : HttpAttribute
{
    public HttpPutAttribute() : base("PUT")
    {
    }
}