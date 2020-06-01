using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;
using EnsoulSharp.SDK;

namespace DarkMage
{
  static  class Ext
    {
        public static bool IsKillable(this Spell spell, AIHeroClient target)
        {
            var damage =spell.GetDamage(target, DamageStage.Default);
            if(target.Health-damage<=0)
                return true;
            return false;
        }

        public static bool Item(this EnsoulSharp.SDK.MenuUI.Menu menu,string name)
        {
            return true;
        }
    }
}
