using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Good
{
    // 魔法使い
    class Wizard : CharacterBase
    {
        protected override int GetSwordAttackDamageSub()
        {
            return GetPowerDownDamage();
        }

        protected override int GetBowAttackDamageSub()
        {
            return Attack;
        }

        protected override int GetMagicAttackDamageSub()
        {
            return GetPowerUpDamage();
        }

    }
}
