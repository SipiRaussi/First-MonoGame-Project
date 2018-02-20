using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

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
        [XmlIgnore]
        public ContentManager Content { private set; get; }

        //Screen widht and height
        private Screen currentScreen, newScreen;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        private XmlManager<Screen> xmlGameScreenManager;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("./Load/ScreenManager.xml");
                }
                return instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            //Create instance of new screen
            newScreen = (Screen)Activator.CreateInstance(Type.GetType("TopDownShooter." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        //Make transition between current and new screen
        private void Transition(GameTime gameTime)
        {
            if(IsTransitioning)
            {
                Image.Update(gameTime);
                if(Image.Alpha == 1.0f)
                {
                    //Unload content before setting new screen
                    currentScreen.UnloadContent();
                    //Current screen is now new one
                    currentScreen = newScreen;
                    //Load screen informations from its Xml file
                    xmlGameScreenManager.Type = currentScreen.Type;
                    if (File.Exists(currentScreen.XmlPath))
                    {
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    }
                    //Load new screen content
                    currentScreen.LoadContent();
                }
                else if(Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
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
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            //Unloads current screens content
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if(IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }
        }
    }
}
