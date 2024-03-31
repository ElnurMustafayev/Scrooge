namespace ScroogeApp.Attributes.Http;

using ScroogeApp.Attributes.Http.Base;

public class HttpDeleteAttribute : HttpAttribute
{
    public HttpDeleteAttribute() : base("DELETE")
    {
    }
}