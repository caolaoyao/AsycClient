using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class GameMap
    {
        public int curFrameCount = 1;
        public int curRoleId = 0;
        public List<GameObj> gameObjList = new List<GameObj>();
        public GameObj curObj;
        private NetManager _netManager;
        public NetManager netManager
        {
            get
            {
                return _netManager;
            }
        }

        public void InitNet(string ip, int tcpPort, int udpPort)
        {
            _netManager = new NetManager();
            netManager.SetIpInfo(ip, tcpPort, udpPort);
            netManager.InitClient();
        }

        public void InputCmd(Cmd cmd, string param)
        {
            KeyData keyData = new KeyData(cmd, param, curRoleId);
            _netManager.AddKeyPack(keyData);
        }

        public void DoCmd(Cmd cmd, string param, int roleId)
        {
            switch(cmd)
            {
                case Cmd.UseSkill:
                    for (int i = 0; i < gameObjList.Count; ++i)
                    {
                        if(gameObjList[i].mCharData.roleId == roleId)
                        {
                            (gameObjList[i] as Player).DoSkill(int.Parse(param));
                        }
                    }
                    break;
                case Cmd.Move:
                    break;
                case Cmd.Turn:
                    break;
                default:
                    Debug.LogError("无效命令");
                    break;
            }
        }

        public void DoCmd(KeyData keyData)
        {
            Debug.LogError("执行关键帧 "+keyData.ToString());
            DoCmd(keyData.cmd, keyData.data, keyData.roleId);
        }

        public void Init()
        {
            gameObjList = new List<GameObj>();
        }

        public void Update()
        {
            for(int i = 0; i < gameObjList.Count; ++i)
            {
                gameObjList[i].Update();
            }

            if(_netManager != null)
            {
                _netManager.Update();
            }
        }
    }
}

