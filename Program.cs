
using System.Net;
using System.Net.Http.Json;
using System.Text;

HttpClient client = new HttpClient();
client.Timeout = new TimeSpan(0, 0, 1);

if (!HttpListener.IsSupported)
{
    Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
    return;
}
// URI prefixes are required,
// for example "http://contoso.com:8080/index/".
//if (prefixes == null || prefixes.Length == 0)
//    throw new ArgumentException("prefixes");

// Create a listener.
HttpListener listener = new HttpListener();
//// Add the prefixes.
//foreach (string s in prefixes)
//{
listener.Prefixes.Add("http://localhost:8088/stage/");
//}
listener.Start();
while (true)
{
    Console.WriteLine("Listening...");
    // Note: The GetContext method blocks while waiting for a request.
    HttpListenerContext context = listener.GetContext();
    HttpListenerRequest request = context.Request;
    Console.WriteLine(request.QueryString["action"]);
    Console.WriteLine(request.QueryString["id"]);
    string jsonString = $"{{\"action\":\"{request.QueryString["action"]}\",\"id\":\"{request.QueryString["id"]}\"}}";

    // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
    try
    {
        using HttpResponseMessage httpClientResponse = await client.PostAsync("http://localhost:5506", content);
    }
    catch (Exception e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }

    // Obtain a response object.
    HttpListenerResponse response = context.Response;
    // Construct a response.
    string responseString = "<HTML><BODY>OK</BODY></HTML>";
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    // Get a response stream and write the response to it.
    response.ContentLength64 = buffer.Length;
    System.IO.Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    // You must close the output stream.
    output.Close();
}