using UnityEngine;
using System.Collections;

namespace Game
{
    public class ViewObj
    {
        public GameObj gameObj;
        public GameObject gameGo;
        protected ViewMap _viewMap;
        protected Transform gameTrans;

        public virtual void Create(CharData charData, ViewMap viewMap)
        {
            _viewMap = viewMap;
            gameObj = new GameObj();
            gameObj.Init(charData, viewMap.LogicMap);
            gameGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameGo.name = charData.name;
            gameTrans = gameGo.transform;
        }

        public int Id
        {
            get
            {
                if(gameObj == null)
                {
                    return -1;
                }
                return gameObj.mCharData.roleId;
            }
        }

        public Vector3 Pos
        {
            get
            {
                if (gameTrans == null)
                {
                    return Vector3.zero;
                }
                return gameTrans.position;
            }
            set
            {
                if (gameTrans != null)
                {
                    gameTrans.position = value;
                }
            }
        }

        public Vector3 EulerAngles
        {
            get
            {
                if (gameTrans == null)
                {
                    return Vector3.zero;
                }
                return gameTrans.localEulerAngles;
            }
            set
            {
                gameTrans.localRotation = Quaternion.Euler(value);
            }
        }

        public virtual void Update()
        {
            if(gameObj == null)
            {
                return;
            }
            gameObj.Update();
        }
    }
}

