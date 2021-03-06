﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class Animation
    {
        //Texture containing all frames of the animation
        private Texture2D spriteStrip;

        //Scale of the sprite
        private float     scale;

        //Animation time variables
        private int       elapsedTime;
        private int       frameTime;
        private int       frameCount;
        private int       currentFrame;

        private Color     color;

        //Rectangle to select the correct frame from the animation strip
        private Rectangle sourceRect = new Rectangle();

        //Rectangle to tell where to draw the selected sprite 
        private Rectangle destinationRect = new Rectangle();

        //Animation variables
        public int        FrameWidth;
        public int        FrameHeight;
        public bool       Active;
        public bool       Looping;
        public Vector2    Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            //Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            //Set the Animation to active by default
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            //Do not update the game if we are not active
            if (Active == false)
                return;

            //Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            //If the elapsed time is larger than frame time
            //we need to switch frames
            if (elapsedTime > frameTime)
            {
                //Move to next frame
                currentFrame++;

                //If the currentFrame is equal to frameCount reset currentFrame to zero
                if(currentFrame == frameCount)
                {
                    currentFrame = 0;

                    //If we are not looping deactive the animation
                    if (Looping == false)
                        Active = false;
                }

                //Reset the elapsed time to zero
                elapsedTime = 0;
            }

            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            //Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
                                            (int)Position.Y - (int)(FrameHeight * scale) / 2,
                                            (int)(FrameWidth * scale),
                                            (int)(FrameHeight * scale));
        }

        public virtual void Draw(SpriteBatch spriteBatch, float rotation)
        {
            //Only draw the animation when we are active
            if(Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color, rotation, new Vector2(FrameWidth / 2, FrameHeight / 2 ), SpriteEffects.None, 1f);
            }
        }
    }
}
