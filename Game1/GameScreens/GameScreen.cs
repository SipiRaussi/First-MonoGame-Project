using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    public class GameScreen : Screen
    {
        //Player declaration
        private Player player;

        //The rate at which the enemies appear
        private TimeSpan enemySpawnTime;
        private TimeSpan previousSpawnTime;

        //A random number generator
        private Random random;

        public GameScreen()
        {
            //Initialize player
            player = new Player();

            //Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            //Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            //Initalize EnemyManager
            EnemyManager.Instance.Initialize(ScreenManager.Instance.GraphicsDevice);

            //Initialize our random number generator
            random = new Random();

            LoadContent();
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            List<int> projectilesToDestroy = new List<int>();
            List<Enemy> enemies = EnemyManager.Instance.Enemies;

            //Update projectiles
            if (Projectile.Projectiles != null)
            {
                //Check for collisions with enemies
                for (int i = 0; i < Projectile.Projectiles.Count; i++)
                {
                    Projectile.Projectiles[i].Update(gameTime);
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        Rectangle rectangle1;
                        Rectangle rectangle2;

                        //Projectile rectangle
                        rectangle1 = new Rectangle((int)Projectile.Projectiles[i].Position.X,
                            (int)Projectile.Projectiles[i].Position.Y,
                            Projectile.Projectiles[i].Animation.FrameWidth,
                            Projectile.Projectiles[i].Animation.FrameHeight);

                        //Enemy rectangle
                        rectangle2 = new Rectangle((int)enemies[j].Position.X,
                                           (int)enemies[j].Position.Y,
                                           enemies[j].Width,
                                           enemies[j].Height);

                        if (rectangle1.Intersects(rectangle2))
                        {
                            //Subratct health and mark projectile for deactivation
                            enemies[j].Health -= Projectile.Projectiles[i].Damage;
                            projectilesToDestroy.Add(i);
                        }
                    }
                }

                //Range check
                for (int i = 0; i < Projectile.Projectiles.Count; i++)
                {
                    if (Projectile.Projectiles[i].RangeCheck())
                    {
                        projectilesToDestroy.Add(i);
                    }
                }

                //Remove destroyed projectiles
                if (Projectile.Projectiles.Count > 0)
                {
                    for (int i = 0; i < projectilesToDestroy.Count; i++)
                    {
                        //Test if the projectile still exists
                        if (i < Projectile.Projectiles.Count)
                        {
                            Projectile.Projectiles[i].Active = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private void UpdateCollision()
        {
            //Use the Rectangle's built-in intersect function to determine if two objects are overlapping
            Rectangle rectangle1;
            Rectangle rectangle2;

            //For readabilitys sake
            List<Enemy> enemies = EnemyManager.Instance.Enemies;

            //Only create the rectangle once for player
            rectangle1 = new Rectangle((int)player.Position.X,
                                       (int)player.Position.Y,
                                       player.Width,
                                       player.Height);
            //Do the collision between the player and the enemies
            for (int enemyIndex = 0; enemyIndex < EnemyManager.Instance.Enemies.Count; enemyIndex++)
            {
                rectangle2 = new Rectangle((int)enemies[enemyIndex].Position.X,
                                           (int)enemies[enemyIndex].Position.Y,
                                           enemies[enemyIndex].Width,
                                           enemies[enemyIndex].Height);
                //Determine if the two objects collided with each other
                if (rectangle1.Intersects(rectangle2))
                {
                    //Substract the healt from the player based on the enemy damage
                    player.Health -= enemies[enemyIndex].Damage;

                    //Since the enemy collided with the player destroy it
                    EnemyManager.Instance.Enemies[enemyIndex].Health = 0;
                }
            }
        }


        public override void LoadContent()
        {
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = ScreenManager.Instance.Content.Load<Texture2D>("Graphics/SonicFrames");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 32, 40, 8, 75, Color.White, 1f, true);

            //Place player within the bounds of the screen
            Vector2 playerPosition = new Vector2(ScreenManager.Instance.GraphicsDevice.Viewport.TitleSafeArea.X + 100,
                                                 ScreenManager.Instance.GraphicsDevice.Viewport.TitleSafeArea.Y
                                                 + ScreenManager.Instance.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);

            //Load enemy texture
            EnemyManager.enemyTexture = ScreenManager.Instance.Content.Load<Texture2D>("Graphics/Shadow");

            Projectile.Texture = ScreenManager.Instance.Content.Load<Texture2D>("Graphics/SonicOneFrame");

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                //Exit();
            }

            //Update input
            InputManager.Instance.Update();

            //Update the player
            player.Update(gameTime);

            //Update the enemies
            EnemyManager.Instance.Update(gameTime, player);

            //Update the collision
            UpdateCollision();

            //Update projectiles
            UpdateProjectiles(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            List<Enemy> enemies = EnemyManager.Instance.Enemies;

            //Background color
            ScreenManager.Instance.GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin();

            //Draw enemies
            for (int enemy = 0; enemy < enemies.Count; enemy++)
            {
                enemies[enemy].Draw(spriteBatch);
            }

            //Draw player
            player.Draw(spriteBatch);

            //Draw projectiles
            if (Projectile.Projectiles != null)
            {
                for (int i = 0; i < Projectile.Projectiles.Count; i++)
                {
                    Projectile.Projectiles[i].Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
