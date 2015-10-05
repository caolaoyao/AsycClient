using UnityEngine;
using System.Collections;

namespace Game
{
    public class MePlayer : Player
    {
        public override void Init(CharData charData, GameMap gameMap)
        {
            base.Init(charData, gameMap);
            _gameMap.curObj = this;
        }
    }
}

