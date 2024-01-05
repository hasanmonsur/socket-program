// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;


Console.WriteLine("Hello, World! Client");



try
{
    TcpClient client = new TcpClient("127.0.0.1", 8888);
    Console.WriteLine("Connected to the server...");

    NetworkStream stream = client.GetStream();
    var requestInfo = new RequestInfo();

    
    // Send data to the server
    string message = JsonConvert.SerializeObject(requestInfo); ;// "Hello, server! Hasan Monsur";

    // Create an ISO 8583 message

   
    byte[] data = Encoding.ASCII.GetBytes(message);

    stream.Write(data, 0, data.Length);
    Console.WriteLine($"Sent data: {message}");

    // Receive response from the server
    byte[] responseBuffer = new byte[1024];
    int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);

    string responseData = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
    Console.WriteLine($"Received response: {responseData}");

    // Close the connection
    client.Close();
}
catch (Exception e)
{
    Console.WriteLine($"Exception: {e.Message}");
}



public class RequestInfo
{
    public RequestInfo()
    {
        processCode = "0001";
        processData =new ProcessData();
    }
    public string processCode { get; set; }

    public ProcessData processData { get; set; }

}

public class ProcessData
{
    public ProcessData()
    {
        id = "20230001";
        name = "Md Hasan Monsur";
    }
    public string id  { get; set; }

    public string name { get; set; }

}

