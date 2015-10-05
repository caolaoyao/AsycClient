using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class ViewMap
    {
        private GameMap _logicMap;
        public GameMap LogicMap
        {
            get
            {
                return _logicMap;
            }
        }

        private ViewObj _curViewObj = null;
        public ViewObj CurViewObj
        {
            get
            {
                return _curViewObj;
            }
            set
            {
                _curViewObj = value;
            }
        }

        private List<ViewObj> viewOjbList = new List<ViewObj>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _logicMap = new GameMap();
            viewOjbList = new List<ViewObj>();
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="charData"></param>
        public void CreateViewObj(CharData charData)
        {
            ViewObj obj = new ViewObj();
            obj.Create(charData, this);
            viewOjbList.Add(obj);
            _logicMap.gameObjList.Add(obj.gameObj);
        }

        /// <summary>
        /// 创建玩家自己
        /// </summary>
        /// <param name="charData"></param>
        public void CreateMe(CharData charData)
        {
            MeViewPlayer obj = new MeViewPlayer();
            obj.Create(charData, this);
            viewOjbList.Add(obj);
            _logicMap.gameObjList.Add(obj.gameObj);
            CurViewObj = obj;
        }

        /// <summary>
        /// 创建一个玩家
        /// </summary>
        /// <param name="charData"></param>
        public void CreatePlayer(CharData charData)
        {
            ViewPlayer obj = new ViewPlayer();
            obj.Create(charData, this);
            viewOjbList.Add(obj);
            _logicMap.gameObjList.Add(obj.gameObj);
        }

        /// <summary>
        /// 创建玩家
        /// </summary>
        /// <param name="players"></param>
        public void CreateAllPlayer(string players)
        {
            string[] playStr = players.Split(';');
            for(int i = 0; i < playStr.Length; ++i)
            {
                CharData charData = new CharData(playStr[i]);
                if(charData.roleId == LogicMap.curRoleId)
                {
                    CreateMe(charData);
                }
                else
                {
                    CreatePlayer(charData);
                }
            }
        }

        public void Update()
        {
            _logicMap.Update();
            for(int i = 0; i < viewOjbList.Count; ++i)
            {
                viewOjbList[i].Update();
            }
        }

        /// <summary>
        /// 同步位置
        /// </summary>
        public void SyncPos(int roleId, string pos)
        {
            string[] str = pos.Split('#');
            float x = float.Parse(str[0]);
            float y = float.Parse(str[1]);
            float z = float.Parse(str[2]);
            float angleX = float.Parse(str[3]);
            float angleY = float.Parse(str[4]);
            float angleZ = float.Parse(str[5]);
            Vector3 cPos = new Vector3(x,y,z);
            Vector3 cAngle = new Vector3(angleX, angleY, angleZ);
            for(int i =0; i < viewOjbList.Count; i++)
            {
                if(viewOjbList[i].gameObj.mCharData.roleId == roleId)
                {
                    viewOjbList[i].Pos = cPos;
                    viewOjbList[i].EulerAngles = cAngle;
                    break;
                }
            }
        }
    }
}


