using EnsoulSharp.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;

namespace DarkMage
{
    class MinionManager
    {
        internal static List<AIMinionClient> GetMinions(Vector3 position, float range, MinionTypes all, GameObjectTeam team)
        {
            return GameObjects.Minions.Where(x =>
                x.Distance(position) < range &&  x.Team == team).ToList();
        }
    }
}
