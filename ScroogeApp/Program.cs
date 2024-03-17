using System.Net;
using System.Text.Json;
using ScroogeApp.Extensions;
using ScroogeApp.Models;

async Task LayoutAsync(HttpListenerResponse response, string bodyHtml, string layoutName = "layout") {
    response.ContentType = "text/html";
    using var streamWriter = new StreamWriter(response.OutputStream);

    var html = (await File.ReadAllTextAsync($"{layoutName}.html"))
        .Replace("{{body}}", bodyHtml);

    await streamWriter.WriteLineAsync(html);
    response.StatusCode = (int)HttpStatusCode.OK;
}

async Task NotFoundAsync(HttpListenerResponse response, string resourceName) {
    Dictionary<string, object>? viewValues = new() {
        {"resource", resourceName}
    };

    await WriteViewAsync(response, "notfound", viewValues);
}

async Task WriteViewAsync(HttpListenerResponse response, string viewName, Dictionary<string, object>? viewValues = null, string? layoutName = null)
{
    var html = await File.ReadAllTextAsync($"{viewName}.html");

    if (viewValues is not null)
    {
        foreach (var viewValue in viewValues)
        {
            html = html.Replace("{{" + viewValue.Key + "}}", viewValue.Value.ToString());
        }
    }
    
    await LayoutAsync(response, html, layoutName ?? "layout");
}

var httpListener = new HttpListener();

var prexif = "http://*:8080/";

httpListener.Prefixes.Add(prexif);

httpListener.Start();

System.Console.WriteLine($"Server started... {prexif.Replace("*", "localhost")}");

while (true)
{
    var client = await httpListener.GetContextAsync();

    string? endpoint = client.Request.RawUrl;

    switch (endpoint)
    {
        case "/":
            {
                await WriteViewAsync(client.Response, "index");
                break;
            }
        case "/Users":
            {
                var usersJson = await File.ReadAllTextAsync("users.json");
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(usersJson);
                
                if(users is not null && users.Any()) {
                    var html = users.AsHtml();
                    await LayoutAsync(client.Response, html);
                }
                else {
                    await NotFoundAsync(client.Response, nameof(users));
                }

                break;
            }
        default:
            {
                await NotFoundAsync(client.Response, endpoint!);

                break;
            }
    }

    client.Response.Close();
}