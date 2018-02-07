using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    public class Image
    {
        //For if we want text in the image
        public string Text, FontName, Path;
        private SpriteFont spriteFont;

        //Texture, alpha and rectangle of the image
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public float Alpha;

        //Images position on screen and scale
        public Vector2 Position, Scale;
        private Vector2 origin;

        //Use ContentManager for loading image
        private ContentManager content;

        private RenderTarget2D renderTarget;

        //Default Ccnstructor
        public Image()
        {
            Path = Text = String.Empty;
            FontName = "Arial";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRectangle = Rectangle.Empty;
        }

        public void LoadContent()
        {
            //Use ScreenManager ContentManager for loading content
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            //Load image from a path
            if(Path != String.Empty)
            {
                Texture = content.Load<Texture2D>(Path);
            }

            //Get font
            spriteFont = content.Load<SpriteFont>(FontName);


            //Get textures and font widht and height
            Vector2 dimensions = Vector2.Zero;

            //Get texture and font widht
            if (Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.X += spriteFont.MeasureString(Text).X;
            }

            //Get texture and font Height
            if (Texture != null)
            {
                dimensions.Y += Math.Max(Texture.Height, spriteFont.MeasureString(Text).Y);
            }
            else
            {
                dimensions.Y = spriteFont.MeasureString(Text).Y;
            }




            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
