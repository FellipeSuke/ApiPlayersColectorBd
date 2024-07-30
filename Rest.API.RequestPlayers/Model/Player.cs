using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.API.RequestPlayers.Model
{
    public class Player
    {
        public string name { get; set; }
        public string accountName { get; set; }
        public string playerId { get; set; }
        public string userId { get; set; }
        public string ip { get; set; }
        public double ping { get; set; }
        public double location_x { get; set; }
        public double location_y { get; set; }
        public int level { get; set; }
    }
}
