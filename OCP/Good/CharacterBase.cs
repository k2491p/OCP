using System;
using System.Collections.Generic;
using System.Text;

namespace OCP.Good
{
    abstract class CharacterBase
    {
        // 攻撃力
        public int Attack { get; set; }

        // 得意攻撃上昇率
        private const double POWER_UP_RATE = 1.25;
        protected int GetPowerUpDamage()
        {
            return (int)Math.Round(Attack * POWER_UP_RATE);
        }


        // 苦手攻撃上昇率
        private const double POWER_DOWN_RATE = 0.75;
        protected int GetPowerDownDamage()
        {
            return (int)Math.Round(Attack * POWER_DOWN_RATE);
        }

        // 会心時ダメージ上昇率
        private const double CRITICAL_POWER_UP_RATE = 1.5;

        private int GetCriticalDamage(int damage)
        {
            Random r = new Random();

            if (r.Next(0, 10) == 0)
            {
                return (int)Math.Round(damage * CRITICAL_POWER_UP_RATE);
            }
            return damage;
        }

        // 剣攻撃ダメージ取得
        public int GetSwordAttackDamage()
        {
            int damage = GetSwordAttackDamageSub();
            return GetCriticalDamage(damage);
        }

        protected abstract int GetSwordAttackDamageSub();

        // 弓攻撃ダメージ取得
        public int GetBowAttackDamage()
        {
            int damage = GetBowAttackDamageSub();
            return GetCriticalDamage(damage);
        }

        protected abstract int GetBowAttackDamageSub();

        // 魔法攻撃ダメージ取得
        public int GetMagicAttackDamage()
        {
            int damage = GetMagicAttackDamageSub();
            return GetCriticalDamage(damage);
        }
        protected abstract int GetMagicAttackDamageSub();

    }
}
