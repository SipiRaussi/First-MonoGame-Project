using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace TopDownShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TopDownShooter : Game
    {
        //Graphics components
        private GraphicsDeviceManager graphics;
        private SpriteBatch           spriteBatch;

        ////Player declaration
        //private Player                player;

        ////Input device states
        //private KeyboardState         currentKeyboardState;
        //private KeyboardState         previousKeyboardState;
        //private MouseState            currentMouseState;
        //private MouseState            previosMouseState;

        ////Enemies
        //private Texture2D             enemyTexture;
        //private List<Enemy>           enemies;

        //Projectiles
        //private Texture2D             blasterTexture;

        //The rate at which the enemies appear
        private TimeSpan              enemySpawnTime;
        private TimeSpan              previousSpawnTime;

        //A random number generator
        private Random                random;
        
        public TopDownShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ////Initialize player
            //player = new Player();

            ////Initialize the enemies list
            //enemies = new List<Enemy>();

            //Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            //Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            //Initialize our random number generator
            random = new Random();

            LoadContent();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Pass GraphicsDevice information to screen manager
            ScreenManager.Instance.GraphicsDevice = GraphicsDevice;

            //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.SpriteBatch = spriteBatch;

            //Pass content to screen manager
            ScreenManager.Instance.LoadContent(Content);

            //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics/SonicFrames");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 32, 40, 8, 75, Color.White, 1f, true);

            //Place player within the bounds of the screen
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 100, 
                                                 GraphicsDevice.Viewport.TitleSafeArea.Y
                                                 + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            //player.Initialize(playerAnimation, playerPosition);

            ////Load enemy texture
            //enemyTexture = Content.Load<Texture2D>("Graphics/Shadow");

            //Projectile.blasterTexture = Content.Load<Texture2D>("Graphics/SonicOneFrame");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            ScreenManager.Instance.Update(gameTime);

            ////Save the previous state of the keyboard and game pad so we can determine single key/button presses
            //previousKeyboardState = currentKeyboardState;
            //previosMouseState = currentMouseState;

            ////Read the current state of the keyboard and gamepad and store it
            //currentKeyboardState = Keyboard.GetState();
            //currentMouseState = Mouse.GetState();

            ////Update the player
            //player.Update(gameTime, currentKeyboardState, previousKeyboardState, currentMouseState);

            ////Update the enemies
            //UpdateEnemy(gameTime);

            ////Update the collision
            //UpdateCollision();

            ////Update projectiles
            //UpdateProjectiles(gameTime);
        }

        //private void UpdateEnemy(GameTime gameTime)
        //{
        //    //Spawn a new enemy evry 1.5 seconds
        //    if(gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
        //    {
        //        previousSpawnTime = gameTime.TotalGameTime;
        //        //Add enemy
        //        AddEnemy();
        //    }

        //    //Update the enemies
        //    for(int enemyIndex = enemies.Count - 1; enemyIndex >= 0; enemyIndex--)
        //    {
        //        enemies[enemyIndex].Update(gameTime);
        //        if(enemies[enemyIndex].Active == false)
        //        {
        //            enemies.RemoveAt(enemyIndex);
        //        }
        //    }
        //}

        //private void UpdateProjectiles(GameTime gameTime)
        //{
        //    List<int> projectilesToDestroy = new List<int>();

        //    //Update projectiles
        //    if (Projectile.Projectiles != null)
        //    {
        //        for (int i = 0; i < Projectile.Projectiles.Count; i++)
        //        {
        //            Projectile.Projectiles[i].Update(gameTime);
        //            for (int j = 0; j < enemies.Count; j++)
        //            {
        //                Rectangle rectangle1;
        //                Rectangle rectangle2;

        //                //Projectile rectangle
        //                rectangle1 = new Rectangle((int)Projectile.Projectiles[i].Position.X,
        //                    (int)Projectile.Projectiles[i].Position.Y,
        //                    Projectile.Projectiles[i].Animation.FrameWidth,
        //                    Projectile.Projectiles[i].Animation.FrameHeight);

        //                //Enemy rectangle
        //                rectangle2 = new Rectangle((int)enemies[j].Position.X,
        //                                   (int)enemies[j].Position.Y,
        //                                   enemies[j].Width,
        //                                   enemies[j].Height);

        //                if (rectangle1.Intersects(rectangle2))
        //                {
        //                    enemies[j].Health -= Projectile.Projectiles[i].Damage;
        //                    projectilesToDestroy.Add(i);
        //                }
        //            }
        //        }

        //        //Remove destroyed projectiles
        //        for (int i = 0; i < projectilesToDestroy.Count; i++)
        //        {
        //            Projectile.Projectiles.RemoveAt(projectilesToDestroy[i]);
        //        }
        //    }
        //}

        //private void UpdateCollision()
        //{
        //    //Use the Rectangle's built-in intersect function to determine if two objects are overlapping
        //    Rectangle rectangle1;
        //    Rectangle rectangle2;

        //    //Only create the rectangle once for player
        //    rectangle1 = new Rectangle((int)player.Position.X,
        //                               (int)player.Position.Y,
        //                               player.Width,
        //                               player.Height);
        //    //Do the collision between the player and the enemies
        //    for (int enemyIndex = 0; enemyIndex < enemies.Count; enemyIndex++)
        //    {
        //        rectangle2 = new Rectangle((int)enemies[enemyIndex].Position.X,
        //                                   (int)enemies[enemyIndex].Position.Y,
        //                                   enemies[enemyIndex].Width,
        //                                   enemies[enemyIndex].Height);
        //        //Determine if the two objects collided with each other
        //        if (rectangle1.Intersects(rectangle2))
        //        {
        //            //Substract the healt from the player based on the enemy damage
        //            player.Health -= enemies[enemyIndex].Damage;

        //            //Since the enemy collided with the player destroy it
        //            enemies[enemyIndex].Health = 0;

        //            //If the player health is less than zero we died
        //            if(player.Health <= 0)
        //            {
        //                player.Active = false;
        //            }
        //        }
        //    }
        //}

        //private void AddEnemy()
        //{
        //    //Create the animation object
        //    Animation enemyAnimation = new Animation();

        //    //Initialize the animation with the correct animation information
        //    enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 64, 64, 7, 75, Color.White, 1f, true);

        //    //Randomly generate the position of the enemy
        //    Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
        //                                   random.Next(100, GraphicsDevice.Viewport.Height - 100));

        //    //Create and initialize enemy
        //    Enemy enemy = new Enemy();
        //    enemy.Initialize(enemyAnimation, position);

        //    //Add the enemy to the active enemy list
        //    enemies.Add(enemy);
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Background color
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            ScreenManager.Instance.Draw(spriteBatch);

            ////Draw enemies
            //for (int enemy = 0; enemy < enemies.Count; enemy++ )
            //{
            //    enemies[enemy].Draw(spriteBatch);
            //}

            ////Draw player
            //player.Draw(spriteBatch);

            ////Draw projectiles
            //if (Projectile.Projectiles != null)
            //{
            //    for (int i = 0; i < Projectile.Projectiles.Count; i++)
            //    {
            //        Projectile.Projectiles[i].Draw(spriteBatch);
            //    } 
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
