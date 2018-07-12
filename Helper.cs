using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WebSocketClient
{
    public static class Helper
    {
        public static string JsonfyWebAuthenticateUser(string authToken)
        {
            //var authToken = "asdasdasd";
            var oV = new PocoO() { SessionToken = authToken };
            Request r = new Request()
            {
                m = 0,
                i = 0,
                n = "WebAuthenticateUser",
                o = oV
            };

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Request));
            MemoryStream stream = new MemoryStream();
            js.WriteObject(stream, r);
            stream.Position = 0;
            var result = new StreamReader(stream).ReadToEnd();
            return result;
        }

        public static RestResponse GetCreditentialsFromRestAPI()
        {
            //rest
            var client = new RestClient("https://api.b2bx.exchange");
            var request = new RestRequest("api/v1/b2trade/auth", Method.POST);
            request.AddParameter("email", "stanislav.stanev@tradologic.com"); // adds to POST or URL querystring based on Method
            request.AddParameter("password", "123456Ss"); // adds to POST or URL querystring based on Method
                                                          //request.AddUrlSegment("id", "123");
                                                          // replaces matching token in request.Resource

            client.UserAgent = "api";
            //IRestResponse response = client.Execute(request);
            var response = client.Execute<RootObject>(request);

            var restResponse = new RestResponse()
            {
                UserId = response.Data.Data.User_id,
                Token = response.Data.Data.Token,
                SessionToken = response.Data.Data.Session_token
            };
            //var userId = response2.Data.Data.User_id;
            //var token = response2.Data.Data.Token;
            //var authToken = response2.Data.Data.Session_token;
            return restResponse;
        }

        #region ModelPoco

        public class Meta
        {
            public List<string> Behaviours { get; set; }
            public int Client { get; set; }
            public int Status { get; set; }
        }

        public class Settings
        {
            public string Locale { get; set; }
        }

        public class Data
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public object Country_code { get; set; }
            public Settings Settings { get; set; }
            public int Status { get; set; }
            public DateTime Logged_at { get; set; }
            public DateTime Created_at { get; set; }
            public DateTime Updated_at { get; set; }
            public string Nickname { get; set; }
            public object Photo { get; set; }
            public string Token { get; set; }
            public int User_id { get; set; }
            public string Session_token { get; set; }
        }

        public class RootObject
        {
            public int Status { get; set; }
            public Meta Meta { get; set; }
            public Data Data { get; set; }
            public List<object> Modules { get; set; }
        }
        #endregion
    }
}
