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
        [XmlIgnore]
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
            FontName = "Fonts/Ubuntu-L";
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
                dimensions.Y = Math.Max(Texture.Height, spriteFont.MeasureString(Text).Y);
            }
            else
            {
                dimensions.Y = spriteFont.MeasureString(Text).Y;
            }

            //Create rectangle based on images/fonts width & height
            if (SourceRectangle == Rectangle.Empty)
            {
                SourceRectangle = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            //Set RenderTarget
            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);

            //Clears screen and start SpriteBatching
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            if(Texture != null)
            {
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            }
            ScreenManager.Instance.SpriteBatch.DrawString(spriteFont, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.SpriteBatch.End();

            //This makes text and image one texture
            Texture = renderTarget;
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRectangle.Width / 2,
                                 SourceRectangle.Height / 2);
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
