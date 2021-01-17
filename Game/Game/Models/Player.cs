using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Game.Models
{
    public class Player
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Level { get; set; }

        [JsonProperty]
        public int Ruby { get; set; }

        public Player()
        {

        }

        public Player(string name, int level, int ruby)
        {
            Name = name;
            Level = level;
            Ruby = ruby;
        }
    }
}
