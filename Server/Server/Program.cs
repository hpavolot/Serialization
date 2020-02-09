using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ClientServer
{
    class Server
    {

        public static Message.Message message = null;
        public static void StartListening()
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(2);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    Socket handler = listener.Accept();

                    MemoryStream stream = new MemoryStream();
                    BinaryFormatter formatter = new BinaryFormatter();

                    //formatter.Context = new StreamingContext(StreamingContextStates.Clone);

                    int byteRec = handler.Receive(bytes);
                    stream.Write(bytes, 0, byteRec);

                    stream.Position = 0;

                    message = (Message.Message)formatter.Deserialize(stream);
                    Console.WriteLine("Name: {0}", message.Name);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        static void Main(string[] args)
        {
            StartListening();
        }
    }
}
