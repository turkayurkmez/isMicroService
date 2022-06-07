// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
HttpClient client = new HttpClient();
var response =  client.GetStringAsync("http://localhost:5004/api/Catalog").GetAwaiter().GetResult();
Console.WriteLine(response);
