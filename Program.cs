using System;

namespace WebSocketClient
{
    public class WebSocketClientram
    {
        public static void Main(string[] args)
        {

            var creditentialsFromRest = Helper.GetCreditentialsFromRestAPI();

            var jsonAuthToken = Helper.JsonfyWebAuthenticateUser(creditentialsFromRest.SessionToken);

            using (var ws = new WebSocketSharp.WebSocket($"wss://wss.b2bx.exchange/WSGateway/?session_token={creditentialsFromRest.SessionToken}"))
            {
                ws.OnError += (sender, e) => Console.WriteLine("Incoming message: " + e.Exception + "||" + e.Message);
                ws.OnMessage += (sender, e) => Console.WriteLine("Incoming message: " + e.Data);

                //ws.OnOpen += (sender, e) => ws.Send();
                ws.Connect();
                ws.Send(jsonAuthToken);
                
            }
            Console.ReadKey(true);
        }
    }
}