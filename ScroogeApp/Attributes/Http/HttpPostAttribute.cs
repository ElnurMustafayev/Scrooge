namespace ScroogeApp.Attributes.Http;

using ScroogeApp.Attributes.Http.Base;

public class HttpPostAttribute : HttpAttribute
{
    public HttpPostAttribute() : base("POST")
    {
    }
}