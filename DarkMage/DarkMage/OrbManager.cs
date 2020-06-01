using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.Events;

namespace DarkMage
{
  public  class OrbManager
    {
        private static int _wobjectnetworkid = -1;

        public static int WObjectNetworkId
        {
            get
            {
                if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).ToggleState == 1)
                    return -1;

                return _wobjectnetworkid;
            }
            set
            {
                _wobjectnetworkid = value;
            }
        }

        public static int tmpQOrbT;
        public static Vector3 tmpQOrbPos = new Vector3();

        public static int tmpWOrbT;
        public static Vector3 tmpWOrbPos = new Vector3();

        public OrbManager()
        {
            AIBaseClient.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;

            // AIBaseClient.OnPauseAnimation += AIBaseClient_OnPauseAnimation;
            //  AIBaseClient.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
        }

        private bool addq,addw;
        private void AIHeroClient_OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
        {
           // EnsoulSharp.t
            if (sender.IsMe && args.SData.Name.Equals("SyndraQ", StringComparison.InvariantCultureIgnoreCase))
            {
           //     tmpQOrbT = TickCount;
                tmpQOrbPos = args.End;
                addq = true;
            }

            if (sender.IsMe && WObject(true) != null && (args.SData.Name.Equals("SyndraW", StringComparison.InvariantCultureIgnoreCase) || args.SData.Name.Equals("syndraw2", StringComparison.InvariantCultureIgnoreCase)))
            {
             //   tmpWOrbT = Utils.TickCount + 250;
                tmpWOrbPos = args.End;
                addw = true;
            }
        }

        void AIBaseClient_OnPauseAnimation(AIBaseClient sender)
        {
            if (sender is AIMinionClient)
            {
                WObjectNetworkId =(int) sender.NetworkId;
            }
        }

    

        public  AIMinionClient WObject(bool onlyOrb)
        {
            if (WObjectNetworkId == -1) return null;
            var obj = ObjectManager.GetUnitByNetworkId<AIBaseClient>((uint)WObjectNetworkId);
            if(obj.GetType()==typeof(AIMinionClient))
            if (obj != null && obj.IsValid() && (obj.Name == "Seed" && onlyOrb || !onlyOrb)) return (AIMinionClient)obj;
            return null;
        }

        public  List<Vector3> GetOrbs(bool toGrab = false)
        {
            var result = new List<Vector3>();
            foreach (
                var obj in
                    ObjectManager.Get<AIMinionClient>()
                        .Where(obj => obj.IsValid && obj.Team == ObjectManager.Player.Team && !obj.IsDead && obj.Name == "Seed"))
            {

                var valid = false;
                if (obj.NetworkId != WObjectNetworkId)
                    if (
                        ObjectManager.Get<GameObject>()
                            .Any(
                                b =>
                                    b.IsValid && b.Name.Contains("_Q_") && b.Name.Contains("Syndra_") &&
                                    b.Name.Contains("idle") && obj.Position.Distance(b.Position) < 50))
                        valid = true;

                if (valid && (!toGrab || !obj.IsMoving))
                    result.Add(obj.Position);
            }
            
       //     if (Utils.TickCount - tmpQOrbT < 400)
        //    {
        if(addq)    
        result.Add(tmpQOrbPos);
          //  }

         //   if (Utils.TickCount - tmpWOrbT < 400 && Utils.TickCount - tmpWOrbT > 0)
          //  {
          if(addw)    
          result.Add(tmpWOrbPos);
            //}

            return result;
        }

        public  Vector3 GetOrbToGrab(int range)
        {
            var list = GetOrbs(true).Where(orb => ObjectManager.Player.Distance(orb) < range).ToList();
            return list.Count > 0 ? list[0] : new Vector3();
        }
    }
}
