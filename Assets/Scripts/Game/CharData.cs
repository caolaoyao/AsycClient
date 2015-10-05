using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class CharData
    {
        public int roleId;
        public string name;

        public CharData(string str)
        {
            string[] playStr = str.Split('#');
            roleId = int.Parse(playStr[0]);
            name = playStr[1];
        }

        public override string ToString()
        {
            string str = roleId + "#" + name;
            return str;
        }
    }
}
