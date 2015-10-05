using UnityEngine;
using System.Collections;
using Net;
using System.Net;
using UnityEngine.UI;
using Game;
using Util;

public class Main : MonoBehaviour {
    public string ip = "127.0.0.1";
    public int tcpPort = 1255;
    public int udpPort = 1337;

    public Button btn_connect;
    public Button btn_ready;
    public Button btn_skill;
    public int skillId = 1001;

    AsycUdpClient client;
	// Use this for initialization
    void Awake()
    {
        InvokeRepeating("Tick", 1, 0.02f);
    }

    void Tick()
    {
        TimerHeap.Tick();
        FrameTimerHeap.Tick();
    }

	void Start () 
    {
        SceneManager.instance.InitGame(ip, tcpPort, udpPort);
        btn_connect.onClick.RemoveAllListeners();
        btn_connect.onClick.AddListener(SceneManager.instance.viewMap.LogicMap.netManager.Connect);

        btn_ready.onClick.RemoveAllListeners();
        btn_ready.onClick.AddListener(SceneManager.instance.viewMap.LogicMap.netManager.Ready);

        btn_skill.onClick.RemoveAllListeners();
        btn_skill.onClick.AddListener(ClientDoSkill);
	}

    public void ClientDoSkill()
    {
        SceneManager.instance.viewMap.LogicMap.InputCmd(Cmd.UseSkill, skillId.ToString());
    }

    void OnDisable()
    {
        SceneManager.instance.viewMap.LogicMap.netManager.Disconnect();
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.instance.Update();
//        client.Update();      
    }
}
