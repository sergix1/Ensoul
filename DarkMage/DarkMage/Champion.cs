using EnsoulSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp.SDK;

namespace DarkMage
{
   public class Champion
    {
        public string Name { get; }
        public SpellSlot SpellSlot { get; }
        
        public AIHeroClient Hero { get; private set; }
        public bool InvunerableSpellReady { get; private set; }

        public bool CastRToDat()
        {
            return !InvunerableSpellReady;
        }
        public Champion(SpellSlot spell,string name)
        {
            this.SpellSlot = spell;
            this.Name = name;
            LoadHero();
            Game.OnUpdate += OnUpdate;
        }
        public void LoadHero()
        {
            foreach(var tar in GameObjects.AllyHeroes)
            {
                if(tar.CharacterName.ToLower()==Name.ToLower())
                {
                    Hero = tar;
                    break;
                }
            }
        }
        private void OnUpdate(EventArgs args)
        {
         /*   if (Hero.IsDead) return;
            var spell = Hero.GetSpell(SpellSlot);
            if(spell!=null)
            if (!(spell.CooldownExpires - Game.Time > 0))
            {

                InvunerableSpellReady = true;
            }
            else
            {
                InvunerableSpellReady = false;
            }*/
        }
    }
}
