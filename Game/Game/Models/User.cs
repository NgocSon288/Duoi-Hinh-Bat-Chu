using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CurrentLevel { get; set; }

        public int Ruby { get; set; }

        public User()
        {

        }

        public User(int iD, string name, int currentLevel, int ruby)
        {
            ID = iD;
            Name = name;
            CurrentLevel = currentLevel;
            Ruby = ruby;
        }
    }
}
