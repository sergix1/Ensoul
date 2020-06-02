
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.Utility;

namespace DarkMage
{
    public class SyndraCore
    {

        private string _tittle,_version;
     
        public AIHeroClient Hero => ObjectManager.Player;
        public Menu GetMenu { get; private set; }
        public GameEvents Events { get; }
        public Spells GetSpells { get; private set; }
        private Modes _modes;
        public List<Vector3> GetOrbs { get; private set; }

        private DrawDamage drawDamage;
        public List<Champion> championsWithDodgeSpells;
        
        public SyndraCore()
        {

         _tittle = "[Syndra]Dark Mage by sergix";
            _version = "2.0.0.0";
            GameEvent.OnGameLoad += OnLoad;
            EnsoulSharp.SDK.Gapcloser.OnGapcloser += OnGapCloser;
            EnsoulSharp.SDK.Interrupter.OnInterrupterSpell += OnInterrupt;
        }

        private void OnInterrupt(AIHeroClient sender, Interrupter.InterruptSpellArgs args)
        {
            bool onI = GetMenu.GetBoolOption("IE");
            if (onI)
            {
                if (sender.Distance(GameObjects.Player)<300 && GetSpells.GetE.IsInRange(sender))
                {
                    GetSpells.GetE.Cast(sender.Position);
                }
            }
        }

        private void OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserArgs args)
        {
            bool onGap = GetMenu.GetBoolOption("AE");
            if (onGap)
            {
                if (sender.Distance(GameObjects.Player)<300)
                {
                    GetSpells.GetE.Cast(sender);

                }
            }
        }

        /* private void OnInterrupt(AIHeroClient sender, Interrupter2.InterruptableTargetEventArgs args)
{
   bool onI = GetMenu.GetMenu.Item("IE");
   if (onI)
   {
       if (sender.IsValidTarget(300)&&GetSpells.GetE.IsInRange(sender))
       {
           GetSpells.GetE.Cast(sender.Position);
       }
   }
}

private void OnGapcloser(ActiveGapcloser gapcloser)
{
   bool onGap=GetMenu.GetMenu.Item("AE");
   if (onGap)
   {
       if (gapcloser.Sender.IsValidTarget(300))
       {
           GetSpells.GetE.Cast(gapcloser.Sender);
       }
   }
}*/

                private void OnLoad()
        {
            if (ObjectManager.Player.CharacterName != "Syndra") return;
            Game.Print("<b><font color =\"#FF33D6\">Dark Mage Loaded!</font></b>");
            var events = new GameEvents(this);
            GetMenu = new SyndraMenu("Dark.Mage by sergix", this);
            GetSpells = new Spells();
            drawDamage = new DrawDamage(this);
            _modes = new SyndraModes();
           
            //    AntiGapcloser.OnEnemyGapcloser += OnGapcloser;
        //    Interrupter2.OnInterruptableTarget += OnInterrupt;
            Game.OnUpdate += OnUpdate;
            Drawing.OnDraw+= Ondraw;
            

        }

        private void Ondraw(EventArgs args)
        {
            
                
            
            var drawQ = GetMenu.GetBoolOption("DQ");
            var drawW = GetMenu.GetBoolOption("DW");
            var drawE = GetMenu.GetBoolOption("DE");
            var drawQE = GetMenu.GetBoolOption("DQE");
            var drawR = GetMenu.GetBoolOption("DR");
            var drawOrb = GetMenu.GetBoolOption("DO");
            var drawOrbText = GetMenu.GetBoolOption("DST");
            var drawHarassTogle = GetMenu.HKey.Active;

            if (ObjectManager.Player.IsDead)
            {
                return;
            }
            if (drawHarassTogle)
            {
                var HKey = true;//GetMenu.HKey;
                if(HKey)
                Drawing.DrawText(0, 250, System.Drawing.Color.Yellow, "Harass Toggle : True");
                else
                    Drawing.DrawText(0, 250, System.Drawing.Color.Yellow, "Harass Toggle : False");
            }
                if (GetSpells.GetQ.IsReady()&&drawQ)
            Render.Circle.DrawCircle(ObjectManager.Player.Position, GetSpells.GetQ.Range, System.Drawing.Color.DarkCyan, 2);
            if (GetSpells.GetW.IsReady() && drawW)
                Render.Circle.DrawCircle(ObjectManager.Player.Position, GetSpells.GetW.Range, System.Drawing.Color.DarkCyan, 2);
            if (GetSpells.GetE.IsReady() && drawE)
                Render.Circle.DrawCircle(ObjectManager.Player.Position, GetSpells.GetE.Range, System.Drawing.Color.DarkCyan, 2);
            if (GetSpells.GetR.IsReady() && drawR)
                Render.Circle.DrawCircle(ObjectManager.Player.Position, GetSpells.GetR.Range, System.Drawing.Color.DarkCyan, 2);
            if(drawQE&&GetSpells.GetE.IsReady()&&GetSpells.GetQ.IsReady())
            Render.Circle.DrawCircle(ObjectManager.Player.Position, GetSpells.GetQ.Range+450, System.Drawing.Color.Red, 2);
            var orbs = GetOrbs;
            if (orbs != null)
            {
                if (drawOrb)
                    for (var i = 0; i < orbs.Count; i++)
                    {
                 
                        var b = orbs[i];
                        Render.Circle.DrawCircle(b, 50, System.Drawing.Color.DarkRed, 2);
                        var wts = Drawing.WorldToScreen(Hero.Position);
                        var wtssxt = Drawing.WorldToScreen(b);

                 
                        if(wtssxt.X >0 && wtssxt.Y>0)
                        Drawing.DrawLine(wts, wtssxt, 2, System.Drawing.Color.DarkRed);
                    }

                if (drawOrbText)
                {

                    var orbsTotal = "Active Orbs R : " + (orbs.Count);
                    Drawing.DrawText(0, 200, System.Drawing.Color.Yellow, orbsTotal);
                }
            }

        }

        private void OnUpdate(EventArgs args)
        {
            GetOrbs = GetSpells.GetOrbs.GetOrbs();
            _modes.Update(this);
        }
    }
}
