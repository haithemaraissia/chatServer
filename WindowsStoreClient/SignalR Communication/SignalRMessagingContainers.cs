using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsStoreClient
{
    public class MapClient
    {
        public string ClientId { get; set; }
        public Location ClientLocation { get; set; }

        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }

    public class ChatClient
    {
        public string ClientId { get; set; }
        public string ChatUserName { get; set; }
    }

    public class GameScoreClient
    {
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
    }

    public class CustomClass
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }
        public bool Property3 { get; set; }
    }
}
