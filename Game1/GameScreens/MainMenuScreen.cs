using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    public class MainMenuScreen : Screen
    {
        private MenuManager menuManager;

        public MainMenuScreen()
        {
            menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            menuManager.LoadContent("./Load/Menus/MainMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            menuManager.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
