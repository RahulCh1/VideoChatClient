using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class ClientB
{
    static int port = 1235;
    static byte[] buffer;
    static string received;
    static FileStream fis;
    static int framenb = 0;
    public static void runClientReceiver()
    {
        UdpClient udpclient = new UdpClient(port);

        IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);

        try
        {
            while (framenb <= 3000)
            {
                buffer = udpclient.Receive(ref ep);
                fis.Write(buffer, 0, buffer.Length);
                framenb++;
                Console.WriteLine(framenb);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        /*while (true)
         {
             buffer = udpclient.Receive(ref ep);
             received = Encoding.ASCII.GetString(buffer);
             Console.WriteLine(received);
         }*/
    }

    public static int Main(String[] args)
    {
        Console.WriteLine("Starting ClientB...");
        fis = File.Open(@"C:\Users\Rahul\Desktop\lenaStreamed.jpg", FileMode.Append, FileAccess.Write);

        runClientReceiver();
        fis.Close();
        return 0;
    }
}
