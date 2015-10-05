using UnityEngine;
using System.Collections;

namespace Game
{
    public class Player : GameObj
    {
        public override void Init(CharData charData, GameMap gameMap)
        {
            base.Init(charData, gameMap);
        }

        public void DoSkill(int skillId)
        {
            Debug.LogError("释放技能,技能id = " + skillId);
        }
    }
}
