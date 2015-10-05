using UnityEngine;
using System.Collections;
using Game.Util;
using UnityEngine.UI;

namespace Game
{
    public class SceneManager : Singleton<SceneManager>
    {
        private ViewMap _viewMap;
        public ViewMap viewMap
        {
            get
            {
                return _viewMap;
            }
        }

        // Update is called once per frame
        public void Update()
        {
            if (_viewMap == null)
            {
                return;
            }
            _viewMap.Update();
        }

        public void InitGame(string ip, int tcpPort, int udpPort)
        {
            _viewMap = new ViewMap();
            _viewMap.Init();
            _viewMap.LogicMap.Init();
            _viewMap.LogicMap.InitNet(ip, tcpPort, udpPort);
        }
    }
}
