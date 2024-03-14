using System.Net;
using System.Text;
using ScroogeApp.Models;

var userHtmlTemplate = @"<div>
    <p><i>Name: </i>{{name}}</p>
    <p><i>Surname: </i>{{surname}}</p>
    <p><i>Birthdate: </i>{{birthdate}}</p>
</div>";

List<User> users = new List<User>() {
    new() {
        Name = "Bob",
        Surname = "Marley",
        BirthDate = new DateTime(1998, 7, 7)
    },
    new() {
        Name = "Ann",
        Surname = "Brown",
        BirthDate = new DateTime(2002, 5, 11)
    }
};

var httpListener = new HttpListener();

var prexif = "http://*:8080/";

httpListener.Prefixes.Add(prexif);

httpListener.Start();

System.Console.WriteLine($"Server started... {prexif}");

while (true)
{
    var client = await httpListener.GetContextAsync();

    string? endpoint = client.Request.RawUrl;

    switch (endpoint)
    {
        // Get all users
        case "/Users":
            {
                // // JSON
                // client.Response.ContentType = "application/json";
                // using var streamWriter = new StreamWriter(client.Response.OutputStream);

                // await streamWriter.WriteLineAsync(JsonSerializer.Serialize(users));
                // break;


                // HTML
                var sb = new StringBuilder(capacity: userHtmlTemplate.Length * users.Count);

                foreach (var user in users)
                {
                    var userHtml = userHtmlTemplate
                        .Replace("{{name}}", user.Name)
                        .Replace("{{surname}}", user.Surname)
                        .Replace("{{birthdate}}", user.BirthDate.ToString());
                    
                    sb.Append(userHtml + "<hr>");
                }

                client.Response.ContentType = "text/html";
                using var streamWriter = new StreamWriter(client.Response.OutputStream);

                await streamWriter.WriteLineAsync(sb.ToString());
                client.Response.StatusCode = (int)HttpStatusCode.OK;
                break;
            }
        default:
            {
                client.Response.ContentType = "text/html";
                using var streamWriter = new StreamWriter(client.Response.OutputStream);

                var html = (await File.ReadAllTextAsync("notfound.html"))
                    .Replace("{{endpoint}}", endpoint);

                await streamWriter.WriteLineAsync(html);
                client.Response.StatusCode = (int)HttpStatusCode.OK;
                break;
            }
    }

    client.Response.Close();
}