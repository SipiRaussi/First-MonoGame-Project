using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Shooter;
using System;
using System.Collections.Generic;

namespace TopDownShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TopDownShooter : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //private Player player;

        //private GamePadState currentGamePadState;
        //private GamePadState previousGamePadState;
        //private KeyboardState currentKeyboardState;
        //private KeyboardState previousKeyboardState;
        //private MouseState currentMouseState;
        //private MouseState previosMouseState;

        //private float playerMoveSpeed;

        //// Image used to display the static background
        ////private Texture2D mainBackground;
        ////private Rectangle rectBackground;
        ////private float scale = 1f;
        ////private ParallaxingBackground bgLayer1, bgLayer2;

        ////Enemies
        //private Texture2D enemyTexture;
        //private List<Enemy> enemies;
        //// The rate at which the enemies appear
        //private TimeSpan enemySpawnTime;
        //private TimeSpan previousSpawnTime;
        //// A random number generator
        //private Random random;
        
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
            //// Initialize player
            //player = new Player();

            //// Background
            ////bgLayer1 = new ParallaxingBackground();
            ////bgLayer2 = new ParallaxingBackground();

            //// Initialize the enemies list
            //enemies = new List<Enemy>();
            //// Set the time keepers to zero
            //previousSpawnTime = TimeSpan.Zero;
            //// Used to determine how fast enemy respawns
            //enemySpawnTime = TimeSpan.FromSeconds(1.0f);
            //// Initialize our random number generator
            //random = new Random();


            //playerMoveSpeed = 8.0f;
            //TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
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

            //// Load the player resources
            //Animation playerAnimation = new Animation();
            //Texture2D playerTexture = Content.Load<Texture2D>("Graphics/SonicFrames");
            //playerAnimation.Initialize(playerTexture, Vector2.Zero, 32, 40, 8, 75, Color.White, 1f, true);

            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, 
            //                                     GraphicsDevice.Viewport.TitleSafeArea.Y
            //                                     + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            //player.Initialize(playerAnimation, playerPosition);
            //// Load the parallaxing background
            ////bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            ////bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            ////mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");

            //enemyTexture = Content.Load<Texture2D>("Graphics/Shadow");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Instance.Update(gameTime);

            //// Save the previous state of the keyboard and game pad so we can determine single key/button presses
            //previousGamePadState = currentGamePadState;
            //previousKeyboardState = currentKeyboardState;
            //previosMouseState = currentMouseState;
            //// Read the current state of the keboard and gamepad and store it
            //currentKeyboardState = Keyboard.GetState();
            //currentGamePadState = GamePad.GetState(PlayerIndex.One);
            //currentMouseState = Mouse.GetState();

            //// Update the player
            //UpdatePlayer(gameTime);
            //// Update the parallaxing background
            ////bgLayer1.Update(gameTime);
            ////bgLayer2.Update(gameTime);
            //// Update the enemies
            //UpdateEnemy(gameTime);
            //// Update the collision
            //UpdateCollision();

            base.Update(gameTime);
        }

        //private void UpdatePlayer(GameTime gameTime)
        //{
        //    player.Update(gameTime);

        //    // Get thumbstick controls
        //    player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
        //    player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

        //    // Use the Keyboard / Dpad
        //    if (currentKeyboardState.IsKeyDown(Keys.Left))
        //    {
        //        player.Position.X -= playerMoveSpeed;
        //    }

        //    if (currentKeyboardState.IsKeyDown(Keys.Right))
        //    {
        //        player.Position.X += playerMoveSpeed;
        //    }

        //    if (currentKeyboardState.IsKeyDown(Keys.Up))
        //    {
        //        player.Position.Y -= playerMoveSpeed;
        //    }

        //    if (currentKeyboardState.IsKeyDown(Keys.Down))
        //    {
        //        player.Position.Y += playerMoveSpeed;
        //    }

        //    // Make sure that the player does not go out of bounds
        //    player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
        //    player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

        //    // Windows Touch Gesture for MonoGame
        //    while (TouchPanel.IsGestureAvailable)
        //    {
        //        GestureSample gesture = TouchPanel.ReadGesture();

        //        if(gesture.GestureType == GestureType.FreeDrag)
        //        {
        //            player.Position += gesture.Delta;
        //        }
        //    }

        //    // Get Mouse State then Capture the button type and Respond Button Press
        //    Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

        //    if(currentMouseState.LeftButton == ButtonState.Pressed)
        //    {
        //        Vector2 posDelta = mousePosition - player.Position;
        //        posDelta.Normalize();
        //        posDelta *= playerMoveSpeed;
        //        player.Position += posDelta;
        //    }
        //}

        //private void UpdateEnemy(GameTime gameTime)
        //{
        //    // Spawn a new enemy evry 1.5 seconds
        //    if(gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
        //    {
        //        previousSpawnTime = gameTime.TotalGameTime;
        //        // Add enemy
        //        AddEnemy();
        //    }

        //    // Update the enemies
        //    for(int enemyIndex = enemies.Count - 1; enemyIndex >= 0; enemyIndex--)
        //    {
        //        enemies[enemyIndex].Update(gameTime);
        //        if(enemies[enemyIndex].Active == false)
        //        {
        //            enemies.RemoveAt(enemyIndex);
        //        }
        //    }
        //}

        //private void UpdateCollision()
        //{
        //    // Use the Rectangle's built-in intersect function to determine if two objects are overlapping
        //    Rectangle rectangle1;
        //    Rectangle rectangle2;

        //    // Only create the rectangle once for player
        //    rectangle1 = new Rectangle((int)player.Position.X,
        //                               (int)player.Position.Y,
        //                               player.Width,
        //                               player.Height);
        //    // Do the collision between the player and the enemies
        //    for (int enemyIndex = 0; enemyIndex < enemies.Count; enemyIndex++)
        //    {
        //        rectangle2 = new Rectangle((int)enemies[enemyIndex].Position.X,
        //                                   (int)enemies[enemyIndex].Position.Y,
        //                                   enemies[enemyIndex].Width,
        //                                   enemies[enemyIndex].Height);
        //        // Determine if the two objects collided with each other
        //        if (rectangle1.Intersects(rectangle2))
        //        {
        //            // Substract the healt from the player based on the enemy damage
        //            player.Health -= enemies[enemyIndex].Damage;
        //            // Since the enemy collided with the player destroy it
        //            enemies[enemyIndex].Health = 0;
        //            // If the player health is less than zero we died
        //            if(player.Health <= 0)
        //            {
        //                player.Active = false;
        //            }
        //        }
        //    }
        //}

        //private void AddEnemy()
        //{
        //    // Create the animation object
        //    Animation enemyAnimation = new Animation();
        //    // Initialize the animation with the correct animation information
        //    enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 64, 64, 7, 75, Color.White, 1f, true);
        //    // Randomly generate the position of the enemy
        //    Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
        //                                   random.Next(100, GraphicsDevice.Viewport.Height - 100));
        //    // Create enemy
        //    Enemy enemy = new Enemy();
        //    // Initialize enemy
        //    enemy.Initialize(enemyAnimation, position);
        //    // Add the enemy to the active enemy list
        //    enemies.Add(enemy);
        //}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            //spriteBatch.Begin();

            //// Draw the Main Background Texture
            ////spriteBatch.Draw(mainBackground, rectBackground, Color.White);
            //// Draw the moving background
            ////bgLayer1.Draw(spriteBatch);
            ////bgLayer2.Draw(spriteBatch);
            //// Draw the enemies
            //for (int enemy = 0; enemy < enemies.Count; enemy++ )
            //{
            //    enemies[enemy].Draw(spriteBatch);
            //}

            //player.Draw(spriteBatch);

            //spriteBatch.End();

            //base.Draw(gameTime);
        }
    }
}
