using UnityEngine;
using System.Collections;

namespace Game
{
    public class ViewPlayer : ViewObj
    {
        public override void Create(CharData charData, ViewMap viewMap)
        {
            _viewMap = viewMap;
            gameObj = new Player();
            gameObj.Init(charData, viewMap.LogicMap);
            gameGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameGo.name = charData.name;
            gameTrans = gameGo.transform;
        }
    }
}

