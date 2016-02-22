using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace ClientA
{

    class Program
    {
        static int port = 1234;
        static int toSendPort = 1235;
        static int frameSize = 20;
        static byte[] buffer = new byte[frameSize];
        static int offset = 0, framenb = 0;
        public static void getImage(FileStream fis, int totalFrames, int framenb)
        {
            if (framenb <= totalFrames)
            {
                fis.Read(buffer, offset * framenb, frameSize);
            }
        }

        public static void runClient()
        {
            FileStream fis = File.Open(@"C:\Users\Rahul\Desktop\lena.jpg", FileMode.Open, FileAccess.Read);
            long length = fis.Length;
            int totalFrames = (int)(length / frameSize); //ignores last 20 bytes
            Console.WriteLine(totalFrames);
            UdpClient udpclient = new UdpClient(port);
            byte[] addr = { 127, 0, 0, 1 };
            IPAddress ipAddress = new IPAddress(addr);
            IPEndPoint ep = new IPEndPoint(ipAddress, toSendPort);

            //byte[] msgBytes = Encoding.ASCII.GetBytes("Hey, I am ClientA!");

            for (int i = 0; i < totalFrames; i++)
            {
                //sends each byte to the port and address in ep
                udpclient.Send(buffer, frameSize, ep);
                getImage(fis, totalFrames, i);
            }
            fis.Close();
            Console.WriteLine("Done!");
            //string sent
            /*while (true)
            {
                udpclient.Send(msgBytes, msgBytes.Length, ep);
                Console.WriteLine("Sent!");
                Thread.Sleep(1000);
            }*/
            Console.Read();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Client A");
            runClient();
        }
    }
}
