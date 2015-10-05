using Game.Util;
using Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using Util;

namespace Game
{
    public class NetManager
    {
        private string _ip = "127.0.0.1";
        private int _tcpPort = 1255;
        private int _udpPort = 1337;
        AsycUdpClient client;
        private List<string> keyPack = new List<string>();

        public void AddKeyPack(KeyData data)
        {
            keyPack.Add(data.ToString());
        }

        public void ClearKeyPack()
        {
            keyPack.Clear();
        }

        public void SetIpInfo(string ip = "127.0.0.1", int tcpPort = 1255, int udpPort = 1337)
        {
            _ip = ip;
            _tcpPort = tcpPort;
            _udpPort = udpPort;
        }

        public void Update()
        {
            client.Update();
        }

        public void InitClient()
        {
            client = new AsycUdpClient();
            client.OnConnect += OnConnect;
            client.OnDisconnect += OnDisconnect;
            client.OnMessage += OnMessage;
        }

        /// <summary>
        /// 同步自己的位置
        /// </summary>
        public void SycMePos()
        {
            Vector3 pos = SceneManager.instance.viewMap.CurViewObj.Pos;
            Vector3 angle = SceneManager.instance.viewMap.CurViewObj.EulerAngles;
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteInt(cProto.SYNC_POS);
            msgBuf.WriteInt(SceneManager.instance.viewMap.CurViewObj.Id);
            string cPos = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", pos.x, pos.y, pos.z, angle.x, angle.y, angle.z);
            msgBuf.WriteString(cPos);
            Send(msgBuf);
        }

        /// <summary>
        /// 发送关键帧
        /// </summary>
        public void SycKey()
        {
            string keyStr = string.Join(";", keyPack.ToArray());
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteInt(cProto.SYNC_KEY);
            msgBuf.WriteInt(SceneManager.instance.viewMap.LogicMap.curFrameCount);
            msgBuf.WriteString(keyStr);
            Send(msgBuf);
            ClearKeyPack();
        }

        /// <summary>
        /// 客户端准备
        /// </summary>
        public void Ready()
        {
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteInt(cProto.READY);
            msgBuf.WriteInt(SceneManager.instance.viewMap.LogicMap.curRoleId);
            Send(msgBuf);
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            Debug.Log("Connecting....");
            client.Connect(_ip, _tcpPort, _udpPort);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        private void Send(string msg)
        {
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteString(msg);
            if (client.Connected)
            {
                client.Send(msgBuf);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cproto"></param>
        private void Send(int cproto)
        {
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteInt(cproto);
            if (client.Connected)
            {
                client.Send(msgBuf);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgBuf"></param>
        private void Send(MessageBuffer msgBuf)
        {
            if (client.Connected)
            {
                client.Send(msgBuf);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cproto"></param>
        /// <param name="msg"></param>
        private void Send(int cproto, string msg)
        {
            MessageBuffer msgBuf = new MessageBuffer();
            msgBuf.WriteInt(cproto);
            msgBuf.WriteString(msg);
            if (client.Connected)
            {
                client.Send(msgBuf);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (client.Connected)
            {
                client.Disconnect();
                Debug.Log("Disconnect.....");
            }
        }

        private void Ping()
        {
            long ping = AsycUdpClient.Ping(new IPEndPoint(IPAddress.Parse(_ip), 1337));
            Debug.Log("Ping....." + ping);
        }

        public void OnConnect()
        {
            Debug.Log("Connected to server!");
        }

        public void OnMessage(MessageBuffer msg)
        {
            int cproto = msg.ReadInt();
            Debug.Log(cproto);
            switch (cproto)
            {
                case cProto.CONNECT:
 //                   SceneManager.instance.InitViewMap();
                    int roleId = msg.ReadInt();
//                    SceneManager.instance.viewMap.CreateMe(roleId);
                    SceneManager.instance.viewMap.LogicMap.curRoleId = roleId;
                    Debug.Log("玩家,id = " + roleId);
                    break;
                case cProto.READY:
                    break;
                case cProto.SYNC_POS:
                    int cRoleId = msg.ReadInt();
                    string pos = msg.ReadString();
                    SceneManager.instance.viewMap.SyncPos(cRoleId, pos);
                    Debug.Log(string.Format("玩家 {0} ,pos = {1}", cRoleId ,pos));
                    break;
                case cProto.SYNC_KEY:
                    int servFrameCount = msg.ReadInt();
                    if (servFrameCount >= SceneManager.instance.viewMap.LogicMap.curFrameCount)
                    {
                        string keyStr = msg.ReadString();
                        string[] keyData = keyStr.Split(';');
                        for (int i = 0; i < keyData.Length; ++i )
                        {
                            if(keyData[i] == "")
                            {
                                continue;
                            }
                            KeyData data = new KeyData(keyData[i]);
                            SceneManager.instance.viewMap.LogicMap.DoCmd(data);
                        }
                        SceneManager.instance.viewMap.LogicMap.curFrameCount += 1;
                    }                   
                    break;
                case cProto.START:
                    string players = msg.ReadString();
                    SceneManager.instance.viewMap.CreateAllPlayer(players);
                    TimerHeap.AddTimer(0, 50, SycMePos);
                    TimerHeap.AddTimer(0, 100, SycKey);
                    break;
            }
        }

        public void OnDisconnect()
        {
            Debug.Log("Disconnected from server!");
        }
    }
}
