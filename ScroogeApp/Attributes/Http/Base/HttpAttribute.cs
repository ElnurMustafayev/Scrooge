namespace ScroogeApp.Attributes.Http.Base;

[AttributeUsage(AttributeTargets.Method)]
public class HttpAttribute : Attribute
{
    public readonly string MethodType;
    public string ActionName = "";

    public HttpAttribute(string methodType)
    {
        this.MethodType = methodType;
    }
}