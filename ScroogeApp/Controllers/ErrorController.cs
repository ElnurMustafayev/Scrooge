namespace ScroogeApp.Controllers;

using System.Net;
using ScroogeApp.Attributes.Http;
using ScroogeApp.Controllers.Base;

public class ErrorController : ControllerBase
{
    [HttpGet]
    public async Task NotFound(string? resourceName) {
        Dictionary<string, object>? viewValues = new() {
            {"resource", resourceName ?? "/"}
        };

        await WriteViewAsync("notfound", viewValues);

        base.Response.StatusCode = (int)HttpStatusCode.NotFound;
    }
}