using System.Net;

var httpListener = new HttpListener();

httpListener.Prefixes.Add($"http://*:8080/");

httpListener.Start();

System.Console.WriteLine("Server started...");

var client = await httpListener.GetContextAsync();

System.Console.WriteLine(client);