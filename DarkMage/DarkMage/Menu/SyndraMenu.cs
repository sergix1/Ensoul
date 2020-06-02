
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.MenuUI;
using EnsoulSharp.SDK.MenuUI.Values;

namespace DarkMage
{
   public class SyndraMenu : Menu
    {
        private EnsoulSharp.SDK.MenuUI.Menu _comboMenu,_drawingMenu, _harassMenu, _keyMenu,_targetsRMe, _dontRIfSpellReady,_farmMenu,_laneClearMenu,_JungleClearMenu,_miscMenu;
 
        public SyndraMenu(string menuName, SyndraCore core) : base(menuName, core)
        {
        }
        public override void LoadComboMenu()
        {
            var qCombo = new MenuBool("CQ", "Use Q").SetValue(true);
            var wCombo = new MenuBool("CW", "Use W").SetValue(true);
            var eCombo = new MenuBool("CE", "Use E").SetValue(true);
            var RCombo = new MenuBool("CR", "Use R").SetValue(true);
            menuBools.Add(qCombo);
            menuBools.Add(wCombo);
            menuBools.Add(eCombo);
            menuBools.Add(RCombo);

            _comboMenu = new EnsoulSharp.SDK.MenuUI.Menu("Combo", "Combo Menu");
            {
                _comboMenu.Add(qCombo);
                _comboMenu.Add(wCombo);
                _comboMenu.Add(eCombo);
                _comboMenu.Add(RCombo);
            }
        }
        public override void LoadHarashMenu()
        {
            var qHarass = new MenuBool("HQ", "Use Q").SetValue(true);
            var wHarass = new MenuBool("HW", "Use W").SetValue(false);
            var eHarass = new MenuBool("HE", "Use E").SetValue(false);
            menuBools.Add(qHarass);
            menuBools.Add(wHarass);
            menuBools.Add(eHarass);
            _harassMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Harass", "Harass Menu");
            {
                _harassMenu.Add(qHarass);
                _harassMenu.Add(wHarass);
                _harassMenu.Add(eHarass);
            }
        }
        public override void LoadDrawings()
        {
            var DQ = new MenuBool("DQ", "Draw Q Range").SetValue(true);
            var DW = new MenuBool("DW", "Draw W Range").SetValue(true);
            var DE = new MenuBool("DE", "Draw E Range").SetValue(true);
            var DQE = new MenuBool("DQE", "Draw QE Range").SetValue(true);
            var DR = new MenuBool("DR", "Draw R Range").SetValue(true);
            var DTD = new MenuBool("DTD", "Draw Total Damage").SetValue(true);
            var DO = new MenuBool("DO", "Draw Orbs").SetValue(true);
            var DST = new MenuBool("DST", "Draw Sphere Text").SetValue(true);
            var DHT = new MenuBool("DHT", "Draw Harass Togle").SetValue(true);
            menuBools.Add(DQ);
            menuBools.Add(DW);
            menuBools.Add(DE);
            menuBools.Add(DQE);
            menuBools.Add(DR);
            menuBools.Add(DTD);
            menuBools.Add(DO);
            menuBools.Add(DST);
            menuBools.Add(DHT);
            _drawingMenu = new EnsoulSharp.SDK.MenuUI.Menu("Drawings", "Draw Menu");
            {
                _drawingMenu.Add(DQ);
                _drawingMenu.Add(DW);
                _drawingMenu.Add(DE);
                _drawingMenu.Add(DQE);
                _drawingMenu.Add(DR);
                _drawingMenu.Add(DTD);//.SetTooltip("Q=Blue W=Orange E=Green Red=R"));
                _drawingMenu.Add(DO);
                _drawingMenu.Add(DST);
                _drawingMenu.Add(DHT);
            }
            _targetsRMe = new EnsoulSharp.SDK.MenuUI.Menu ("Targets R", "Targets R");
            {
                foreach (AIHeroClient hero in GameObjects.EnemyHeroes)
                {
                    if(hero.CharacterName!= "PracticeTool_TargetDummy")
                    _targetsRMe.Add(new MenuBool(hero.CharacterName, hero.CharacterName).SetValue(true));
                }
            }
            var DONTRZHONYA = new MenuBool("DONTRZHONYA", "Dont R if enemy has zhonia active").SetValue(false);
            menuBools.Add(DONTRZHONYA);
            _dontRIfSpellReady = new EnsoulSharp.SDK.MenuUI.Menu ("R Spells", "Dont R if");
            {
               _dontRIfSpellReady.Add(DONTRZHONYA);
                foreach (AIHeroClient hero in GameObjects.EnemyHeroes)
                {
                    foreach (String s in Lists.DontRSpellList)
                    {
                        var result = s.Split('-');
                        if (result[0].ToLower() == hero.CharacterName.ToLower())
                        {
                            _dontRIfSpellReady.Add(new MenuBool(hero.CharacterName+ "-"+result[1], hero.CharacterName+ " " + result[1]).SetValue(true));
                        }
                
                        //   "Fizz-E","Vladimir-W","Ekkko-R","Zed-R","Yi-Q","Zilean-R","Shaco-R","Kalista-R","Lissandra-R","Kindred-R","Kayle-R","Taric-R"
                    }
                    if (hero.CharacterName == "MasterYi")
                    {
                        _dontRIfSpellReady.Add(new MenuBool(hero.CharacterName + "-" +"Q", hero.CharacterName + " " +"Q").SetValue(true));
                    }
                }
            }
        }

        public override void LoadLaneClearMenu()
        {
            var ql= new MenuBool("LQ", "Use Q").SetValue(true);
            var wl = new MenuBool("LW", "Use W").SetValue(true);
            menuBools.Add(ql);
            menuBools.Add(wl);
            _laneClearMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Laneclear", "Laneclear");
            {
                _laneClearMenu.Add(ql);
                _laneClearMenu.Add(wl);
                //_laneClearMenu.Add(new MenuSlider("LM", "Minium Mana %", 0, 50, 100));
            }
            _JungleClearMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Jungleclear", "Jungleclear");
            {
                _JungleClearMenu.Add(new MenuBool("JQ", "Use Q").SetValue(true));
                _JungleClearMenu.Add(new MenuBool("JW", "Use W").SetValue(true));
                _JungleClearMenu.Add(new MenuBool("JE", "Use E").SetValue(true));
               // _JungleClearMenu.Add(new MenuSlider("JM", "Minium Mana %", 0, 50, 100));
            }
            _farmMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Farm  Menu", "Farm Menu");
            {
                _farmMenu.Add(_laneClearMenu);
                _farmMenu.Add(_JungleClearMenu);
            }

            base.LoadLaneClearMenu();
        }


     
        public override void LoadkeyMenu()
        {
            QEkey = new MenuKeyBind("QEkey", "Q+E To Mouse Key", Keys.T, KeyBindType.Press);
            AUTOQE = new MenuKeyBind("AUTOQE", "AUTO Q+E Stun target", Keys.X, KeyBindType.Press);
            HKey = new MenuKeyBind("HKey", "Harass Toggle", Keys.Z, KeyBindType.Toggle);
            Rkey = new MenuKeyBind("RKey", "R to best target", Keys.R, KeyBindType.Press);

            _keyMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Keys", "Keys Menu");
            {
                _keyMenu.Add(QEkey);
                _keyMenu.Add(AUTOQE);
                _keyMenu.Add(HKey);
                _keyMenu.Add(Rkey);
            }
        }
        public override void CloseMenu()
        {
            GetMenu.Add(_comboMenu);
            GetMenu.Add(_harassMenu);
            GetMenu.Add(_farmMenu);
            GetMenu.Add(_targetsRMe);
            GetMenu.Add(_dontRIfSpellReady);
            GetMenu.Add(_keyMenu);
            GetMenu.Add(_miscMenu);
            GetMenu.Add(_drawingMenu);
            base.CloseMenu();
        }

        public override void LoadMiscMenu()
        {
            var AE = new MenuBool("AE", "Use E Antigapclose").SetValue(true);
            var IE = new MenuBool("IE", "Use E Interrupt").SetValue(true);
            menuBools.Add(AE);
            menuBools.Add(IE);
                _miscMenu = new EnsoulSharp.SDK.MenuUI.Menu ("Misc", "Misc Menu");
            {
                _miscMenu.Add(AE);
                _miscMenu.Add(IE);
       
            }
            base.LoadMiscMenu();
        }
    }
}
