using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatServer.Net;

namespace ConsoleChat.Net
{
    class ServerObject
    {
        TcpListener tcpListener;
        ClientObject clientObject;

        public ServerObject(IPAddress ip, int port)
        {
            tcpListener = new TcpListener(ip, port); // сервер для прослушивания
        }

         protected internal void RemoveConnection(ClientObject client)
        {
            //удаляем клиент
            client?.Close();
        }
        
        // прослушивание входящих подключений
        protected internal async Task ListenAsync()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    clientObject = new ClientObject(tcpClient, this);
                    Task.Run(clientObject.ProcessAsync);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        // трансляция сообщения подключенным клиентам
        protected internal async Task BroadcastMessageAsync(string message)
        {
                    await clientObject.Writer.WriteLineAsync(message); //передача данных
                    await clientObject.Writer.FlushAsync();
        }
        
        // отключение всех клиентов
        protected internal void Disconnect()
        {
            clientObject.Close(); //отключение клиента
            tcpListener.Stop(); //остановка сервера
        }
    }

}
