using EnsoulSharp.SDK;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsoulSharp.SDK.MenuUI.Values;
using Color = SharpDX.Color;

namespace DarkMage
{
    public class Menu
    {
        public EnsoulSharp.SDK.MenuUI.Menu GetMenu { get; private set; }
        public MenuKeyBind QEkey, AUTOQE, HKey, Rkey;
        private string _menuName;
        //  EnsoulSharp.
        //    public Orbwalking.Orbwalker Orb { get; private set; }
        EnsoulSharp.SDK.MenuUI.Menu _orbWalkerMenu, _targetSelectorMenu;
        public List<MenuBool> menuBools = new List<MenuBool>();

        public MenuBool GetBoolOption(String name)
        {
            return menuBools.FirstOrDefault(x => x.Name == name);
        }
        public Menu(string menuName, SyndraCore core)
        {
            this._menuName = menuName;
            LoadMenu(core);
            CloseMenu();
        }
        public virtual void LoadMenu(SyndraCore azir)
        {
            GetMenu = new EnsoulSharp.SDK.MenuUI.Menu(_menuName, _menuName, true); ;
            _orbWalkerMenu = new EnsoulSharp.SDK.MenuUI.Menu("Orbwalker", "Orbwalker");
          //  Orb = new Orbwalking.Orbwalker(_orbWalkerMenu);
            _targetSelectorMenu = new EnsoulSharp.SDK.MenuUI.Menu("TargetSelector", "TargetSelector");
            LoadLaneClearMenu();
            LoadHarashMenu();
            LoadComboMenu();
            LoadJungleClearMenu();
            LoadDrawings();
            LoadkeyMenu();
          //  LoadMiscInterrupt(azir);
            LoadMiscMenu();

        }

      public virtual void LoadMiscMenu()
        {
         
        }

        private void LoadMiscInterrupt(SyndraCore azir)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadkeyMenu()
        {
 
        }

       public virtual void LoadComboMenu()
        {
         
        }

      public virtual void LoadDrawings()
        {
        }

        private void LoadJungleClearMenu()
        {
        }

        public virtual void LoadHarashMenu()
        {
  
        }

        public virtual void LoadLaneClearMenu()
        {
        }

        public virtual void CloseMenu()
        {
           // TargetSelector.AddToMenu(_targetSelectorMenu);
        //    GetMenu.AddSubMenu(_orbWalkerMenu);        //ORBWALKER
        //    GetMenu.AddSubMenu(_targetSelectorMenu);
        //    GetMenu.AddToMainMenu();
            GetMenu.Attach();

        }
    }
}
