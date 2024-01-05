// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;

Console.WriteLine("Hello, World! Server");


TcpListener server = null;

try
{
    // Set the TcpListener on a specific port
    int port = 8888;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    // TcpListener
    server = new TcpListener(localAddr, port);

    // Start listening for client requests
    server.Start();

    Console.WriteLine($"Server started on {localAddr}:{port}");

    // Enter the listening loop
    while (true)
    {
        Console.WriteLine("Waiting for a connection...");

        // Perform a blocking call to accept requests
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Connected to {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

        // Handle the client in a separate thread or asynchronously
        HandleClient(client);
    }
}
catch (Exception e)
{
    Console.WriteLine($"Exception: {e.Message}");
}
finally
{
    // Stop listening for new clients
    server?.Stop();
}

static void HandleClient(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];
    int bytesRead;

    try
    {
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Received data: {data}");

            //data = data + " Good Morning !!";
            Console.WriteLine($"Send  data: {data}");


            // Echo the data back to the client
            byte[] responseBuffer = Encoding.ASCII.GetBytes(data);
            stream.Write(responseBuffer, 0, responseBuffer.Length);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception: {e.Message}");
    }
    finally
    {
        client.Close();
    }
}