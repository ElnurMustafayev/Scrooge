namespace ScroogeApp.Controllers;

using System.Net;
using ScroogeApp.Attributes.Http;
using ScroogeApp.Controllers.Base;

public class HomeController : ControllerBase
{
    [HttpGet]
    public async Task Index() {
        await WriteViewAsync("index");
    }
}