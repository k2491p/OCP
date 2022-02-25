using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Good
{
    // 剣士クラス
    class Swordsman : CharacterBase
    {
        protected override int GetSwordAttackDamageSub()
        {
            return GetPowerUpDamage();
        }

        protected override int GetBowAttackDamageSub()
        {
            return Attack;
        }

        protected override int GetMagicAttackDamageSub()
        {
            return GetPowerDownDamage();
        }

    }
}
