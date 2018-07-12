using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace WebSocketClient
{
    public class WebSocketClientram
    {
        public static void Main(string[] args)
        {

            var creditentialsFromRest = Helper.GetCreditentialsFromRestAPI();

            var jsonAuthToken = Helper.JsonfyWebAuthenticateUser(creditentialsFromRest.SessionToken);
            //var jsonUserData = Helper.JsonfyUserData();
            //var jsonGetInstruments = Helper.JsonfyGetInstruments();

            var ws = new WebSocket($"wss://wss.b2bx.exchange/WSGateway/?session_token={creditentialsFromRest.SessionToken}");

            var json = "{\"m\":0,\"i\":0,\"n\":\"GetInstruments\",\"o\":\"{\"OMSId\":1}\"}";
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            ws.OnOpen += (sender, e) => { ws.Send(json); };
            ws.CustomHeaders = new Dictionary<string, string> {
                            {"user-agent", "api"}
                        };
            ws.OnError += Ws_OnError;
            ws.OnMessage += Ws_OnMessage;

            ws.OnClose += Ws_OnClose;
            ws.Log.Level = LogLevel.Debug;
            //ws.Log.File = @"C:\WebSockets\WebSocketClientCSharp-master\log.txt";
            ws.Connect();

            ws.Send(json);
            ws.Close();
            Console.ReadKey(true);
        }

        private static void Ws_OnClose(object sender, WebSocketSharp.CloseEventArgs e)
        {
            //Console.WriteLine("Reason: " + e.Reason);
            Console.WriteLine("Closing Code: " + e.Code);
        }

        private static void Ws_OnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            Console.WriteLine("Data: " + e.Data);
            Console.WriteLine("RawData: " + e.RawData);
        }

        private static void Ws_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            Console.WriteLine("Exception: " + e.Exception);
            Console.WriteLine("Message: " + e.Message);

        }
    }
}