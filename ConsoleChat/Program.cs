using ConsoleChat.Net;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

IPAddress Ip = IPAddress.Any;
int Port = 8888;


ServerObject server = new ServerObject(Ip, Port);// создаем сервер
await server.ListenAsync(); // запускаем сервер




