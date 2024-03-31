namespace ScroogeApp.Controllers.Base;

using System.Net;

public abstract class ControllerBase
{
    public HttpListenerResponse? Response { get; set; }

    protected async Task LayoutAsync(string bodyHtml, string layoutName = "layout")
    {
        Response.ContentType = "text/html";
        using var streamWriter = new StreamWriter(Response.OutputStream);

        var html = (await File.ReadAllTextAsync($"{layoutName}.html"))
            .Replace("{{body}}", bodyHtml);

        await streamWriter.WriteLineAsync(html);
    }

    protected async Task WriteViewAsync(string viewName, Dictionary<string, object>? viewValues = null, string? layoutName = null)
    {
        var html = await File.ReadAllTextAsync($"{viewName}.html");

        if (viewValues is not null)
        {
            foreach (var viewValue in viewValues)
            {
                html = html.Replace("{{" + viewValue.Key + "}}", viewValue.Value.ToString());
            }
        }

        await LayoutAsync(html, layoutName ?? "layout");
    }
}