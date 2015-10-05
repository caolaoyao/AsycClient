using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class KeyData
    {
        public Cmd cmd;
        public string data;
        public int roleId;
        public KeyData(Cmd cmd, string data, int roleId)
        {
            this.cmd = cmd;
            this.data = data;
            this.roleId = roleId;
        }

        public KeyData(string dataStr)
        {
            string[] str = dataStr.Split('#');
            this.cmd = (Cmd)int.Parse(str[0]);
            this.data = str[1];
            this.roleId = int.Parse(str[2]);
        }

        public override string ToString()
        {
            int iCmd = (int)cmd;
            string str = iCmd.ToString() + "#" + data +"#" + roleId.ToString();
            return str;
        }
    }
}
