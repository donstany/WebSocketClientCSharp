using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketClient
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public int m;
        [DataMember]
        public int i;
        [DataMember]
        public string n;
        [DataMember]
        public PocoO o;

    }

    [DataContract]
    public class PocoO
    {
        [DataMember]
        public string SessionToken;
    }
}
