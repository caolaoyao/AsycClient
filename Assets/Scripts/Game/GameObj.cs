using UnityEngine;
using System.Collections;

namespace Game
{
    public class GameObj 
    {
        protected int Id = 10000;
        protected Vector3 pos = Vector3.zero;
        protected Vector3 direction = Vector3.zero;
        protected GameMap _gameMap;
        protected CharData _charData;
        public virtual void Init(CharData charData, GameMap gameMap)
        {
            _charData = charData;
            _gameMap = gameMap;
            Id = charData.roleId;
        }

        public CharData mCharData
        {
            get
            {
                return _charData;
            }
        }

        public virtual void Update()
        {

        }
    }
}
