using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    public class SplashScreen : Screen
    {
        public Image Image;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            Image.Update(gametime);

            //Press Enter to change screen (current to back to it self)
            if(InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
            {
                ScreenManager.Instance.ChangeScreens(Type.Name);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
