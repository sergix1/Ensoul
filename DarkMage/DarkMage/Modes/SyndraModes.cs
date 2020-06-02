
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;
using EnsoulSharp.SDK;

namespace DarkMage
{
    class SyndraModes : Modes
    {
        public SyndraModes()
        {
        }
        public override void Combo(SyndraCore core)
        {
            var useQ = core.GetMenu.GetBoolOption("CQ");
            var useW = core.GetMenu.GetBoolOption("CW");
            var useE = core.GetMenu.GetBoolOption("CE");
            var useR = core.GetMenu.GetBoolOption("CR");
            var qeRange = core.GetSpells.GetQ.Range + 500;
            var qeTarget = TargetSelector.GetTarget(qeRange, DamageType.Magical);
            if (qeTarget != null)
            {

               // if (!core.GetSpells.castE(qeTarget))
               // {
                    if (useQ)
                        core.GetSpells.CastQ();
                    if (useW)
                        core.GetSpells.CastW();
              //  }
                if (useE)
                {
                    var eTarget = TargetSelector.GetTarget(core.GetSpells.EQ.Range, DamageType.Magical);
                    if (eTarget != null && core.GetSpells.GetE.IsReady())
                        core.GetSpells.castE(eTarget);
                }

            }
            if (useR)
                core.GetSpells.CastR(core);
            base.Combo(core);
        }
        public override void Harash(SyndraCore core)
        {
            var useQ = core.GetMenu.GetBoolOption("HQ");
            var useW = core.GetMenu.GetBoolOption("HW");
            var useE = core.GetMenu.GetBoolOption("HE");
            if (useQ)
                core.GetSpells.CastQ();
            if (useW)
                core.GetSpells.CastW();
            if (useE)
                core.GetSpells.CastE(core);
            base.Harash(core);
        }
        bool QE,AutoQE;
        public override void Keys(SyndraCore core)
        {
            if (core.GetSpells.GetQ.IsReady() && core.GetSpells.GetE.IsReady())
            {
                QE = false;
                AutoQE = false;
            }
            if (core.GetMenu.Rkey.Active)
            {
                if (core.GetSpells.GetR.IsReady())
                {
                    var rTarget = TargetSelector.GetTarget(core.GetSpells.GetR.Range, DamageType.Magical);
                    if(rTarget!=null)
                    core.GetSpells.GetR.Cast(rTarget);
                }

            }
        
            if (core.GetMenu.HKey.Active)
            {
               // if (!HeroManager.Player.CanAttack)
               // {
                    core.GetSpells.CastQ();
               // }

            }
            if (core.GetMenu.QEkey.Active)
            {
                if (!QE)
                {
                    var gameCursor = Game.CursorPos;
                    core.GetSpells.GetQ.Cast(core.Hero.Position.Extend(Game.CursorPos, core.GetSpells.GetQ.Range));
                    EnsoulSharp.SDK.Utility.DelayAction.Add(250 + Game.Ping, () => core.GetSpells.GetE.Cast(gameCursor));
                    QE = true;
                }
            }
            if (core.GetMenu.AUTOQE.Active)
            {
                if (!AutoQE)
                {
                    var qeRange = core.GetSpells.GetQ.Range + 500;
                    var qeTarget = TargetSelector.GetTarget(qeRange, DamageType.Magical);
                      
                        if (qeTarget != null)
                        {
                            var predpos = TargetSelector.GetTarget(qeRange,DamageType.Magical);
                            if (predpos.Position.Distance(core.Hero.Position) < qeRange)    
                            {
                                var ballPos = core.Hero.Position.Extend(predpos.Position, core.GetSpells.GetQ.Range);
                                core.GetSpells.GetQ.Cast(ballPos);
                               EnsoulSharp.SDK.Utility.DelayAction.Add(250 + Game.Ping, () => core.GetSpells.GetE.Cast(ballPos));
                                AutoQE = true;
                            }
                        }
                    if (qeTarget != null)
                    {
                        core.GetSpells.TryBallE(qeTarget);
                    }
                }
            }
    
            base.Keys(core);
        }
        public override void LastHit(SyndraCore core)
        {

            //Last hit with q when u cant kill minion with aa.
            base.LastHit(core);
        }
        public override void Laneclear(SyndraCore core)
        {
            var useQ = core.GetMenu.GetBoolOption("LQ");
            var useW = core.GetMenu.GetBoolOption("LW");
            var miniumMana = 0; //core.GetMenu.GetMenu.Item("LM").GetValue<Slider>().Value;
            if (core.Hero.ManaPercent < miniumMana) return;
            if (useQ)
            {
                var minionQ =
    MinionManager.GetMinions(
                   ObjectManager.Player.Position,
                     core.GetSpells.GetQ.Range,
                    MinionTypes.All,
                    GameObjectTeam.Chaos);

                if (minionQ != null && core.GetSpells.GetQ.IsReady())
                {
                    //Mejorable
                    var QfarmPos = core.GetSpells.GetQ.GetCircularFarmLocation(minionQ);// minionQ.FirstOrDefault(); //core.GetSpells.GetQ.GetPrediction(minionQ);// core.GetSpells.GetQ.GetCircularFarmLocation(minionQ);
                //    if (QfarmPos.Position.IsValid())
                        if (QfarmPos.MinionsHit >= 2)
                       // {
                          //  Game.Print("no hiteo ni de coña");
                            core.GetSpells.GetQ.Cast(QfarmPos.Position);
                        //}
                }
            }
            if (useW)
            {
                var minionW =
MinionManager.GetMinions(
    core.Hero.Position,
     core.GetSpells.GetW.Range,
   MinionTypes.All,
     GameObjectTeam.Chaos
     );
                if (minionW != null)
                {
                    var WfarmPos = core.GetSpells.GetQ.GetCircularFarmLocation(minionW);
                 //   if (WfarmPos.Position.IsValid())
                  //  {
                        if (WfarmPos.MinionsHit >= 2)
                        {
                            core.GetSpells.CastWToPos(WfarmPos.Position);
                        }
                   // }
                }
            }
            base.Laneclear(core);
        }

        //check jungle
        public override void Jungleclear(SyndraCore core)
        {
            var useQ = core.GetMenu.GetMenu.Item("JQ");
            var useW = core.GetMenu.GetMenu.Item("JW");
            var useE = core.GetMenu.GetMenu.Item("JE");
            var miniumMana = 0;//core.GetMenu.GetMenu.Item("JM").GetValue<Slider>().Value;
            if (core.Hero.ManaPercent < miniumMana) return;
            if (useQ)
            {
                var minionQ =
                    MinionManager.GetMinions(
                        core.Hero.Position,
                        core.GetSpells.GetQ.Range,
                        MinionTypes.All,
                        GameObjectTeam.Neutral);
                if (minionQ != null)
                {
                    var QfarmPos = core.GetSpells.GetQ.GetCircularFarmLocation(minionQ);
                    //      if (QfarmPos.Position.IsValid())
                    if (QfarmPos.MinionsHit >= 1)
                        core.GetSpells.GetQ.Cast(QfarmPos.Position);

                }
            }
            if (useW)
            {
                //EnsoulSharp.SDK.M
                var minionW = MinionManager.GetMinions(ObjectManager.Player.Position, core.GetSpells.GetW.Range, MinionTypes.All,GameObjectTeam.Chaos);
                var minionss = minionW.Any(x => x.Team == GameObjectTeam.Neutral);
                if (minionss)
                {
                    var WfarmPos = core.GetSpells.GetQ.GetCircularFarmLocation(minionW);
                    if (WfarmPos.Position.IsValid())
                    {
                        core.GetSpells.CastWToPos(WfarmPos.Position);

                    }
                    else
                    {
                        core.GetSpells.CastWToPos(Game.CursorPos.ToVector2());
                    }
                }
            }
            if (useE)
            {
                
                var minionE =
                    MinionManager.GetMinions(
                        core.Hero.Position,
                        core.GetSpells.GetQ.Range,
                        MinionTypes.All,
                        GameObjectTeam.Neutral);


                if (minionE != null)
                {
                    var EfarmPos = core.GetSpells.GetQ.GetCircularFarmLocation(minionE);
                    if (EfarmPos.Position.IsValid())
                    {
                        core.GetSpells.GetE.Cast(EfarmPos.Position);
                    }
                }
            }
            base.Jungleclear(core);
        }
    }
        
        }
    

