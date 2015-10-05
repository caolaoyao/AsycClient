using UnityEngine;
using System.Collections;

namespace Game
{
    public class MeViewPlayer : ViewPlayer
    {
        public override void Create(CharData charData, ViewMap viewMap)
        {
            _viewMap = viewMap;
            gameObj = new MePlayer();
            gameObj.Init(charData, viewMap.LogicMap);
//            gameGo = new GameObject();
            gameGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameGo.name = charData.name;
            gameGo.AddComponent<PlayerMoveController>();
            gameTrans = gameGo.transform;
        }
    }
}
