using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Good
{
    // 弓使い
    class Archer : CharacterBase
    {
        protected override int GetSwordAttackDamageSub()
        {
            return Attack;
        }

        protected override int GetBowAttackDamageSub()
        {
            return GetPowerUpDamage();
        }

        protected override int GetMagicAttackDamageSub()
        {
            return GetPowerDownDamage();
        }

    }
}
