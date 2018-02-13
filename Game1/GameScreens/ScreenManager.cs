using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    public class ScreenManager
    {
        //Singleton of this class
        private static ScreenManager instance;

        //This handles all the content of this project
        public ContentManager Content { private set; get; }

        //Screen widht and height
        private Screen currentScreen;
        public Vector2 Dimensions { private set; get; }
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        private XmlManager<Screen> xmlGameScreenManager;



        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        private ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XmlManager<Screen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("./Load/SplashScreen.xml");
        }

        public void LoadContent(ContentManager content)
        {
            //Use TopDownShooters ContentManager
            Content = new ContentManager(content.ServiceProvider, "Content");

            //Loads current screens content
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            //Unloads current screens content
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}
