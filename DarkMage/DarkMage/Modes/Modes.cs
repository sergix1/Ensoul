
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp.SDK;

namespace DarkMage
{
    class Modes
    {
        public virtual void Update(SyndraCore core)
        {
    
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Combo:
                    Combo(core);
                    break;
                case OrbwalkerMode.Harass:
                    Harash(core);
                    break;
                case OrbwalkerMode.LaneClear:
                    Laneclear(core);
                    Jungleclear(core);
                    break;
                case OrbwalkerMode.LastHit:
                    LastHit(core);
                    break;
            
                default:
                    break;
            }
            Keys(core);
        }
        public virtual void Keys(SyndraCore core)
        {

        }
        public virtual void Jungleclear(SyndraCore core)
        {
        }

        public virtual void Laneclear(SyndraCore core)
        {
        }
        public virtual void LastHit(SyndraCore core)
        {

        }
        public virtual void Harash(SyndraCore core)
        {
        }

        public virtual void Combo(SyndraCore core)
        {
        }
    }
}

