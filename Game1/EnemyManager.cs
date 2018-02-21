using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class EnemyManager
    {
        //Enemy list
        public List<Enemy> Enemies;

        //Time definitions
        private TimeSpan spawnInterval;
        private TimeSpan previousSpawnTime;

        //Texture
        public static Texture2D enemyTexture;

        //New random for enemy placement
        private Random random;

        //Graphics device for screen dimensions
        private GraphicsDeviceManager graphics;

        //Singleton
        private static EnemyManager instance;
        public static EnemyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyManager();
                }
                return instance;
            }
        }

        private void SpawnEnemy()
        {
            //Create the animation object
            Animation enemyAnimation = new Animation();

            //Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 64, 64, 7, 75, Color.White, 1f, true);

            //Randomly generate the position of the enemy
            Vector2 position = new Vector2(graphics.GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
                                           random.Next(100, graphics.GraphicsDevice.Viewport.Height - 100));

            //Create and initialize enemy
            Enemy enemy = new Enemy();
            enemy.Initialize(enemyAnimation, position);

            //Add the enemy to the active enemy list
            Enemies.Add(enemy);
        }
        
        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            random = new Random();
            previousSpawnTime = TimeSpan.Zero;
            spawnInterval = TimeSpan.FromSeconds(1.0f);
            Enemies = new List<Enemy>();
        }

        /// <summary>
        /// Updates the enemies and spawns new ones.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, Player player)
        {
            //Spawn a new enemy every 1.5 seconds
            if (gameTime.TotalGameTime - previousSpawnTime > spawnInterval)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                //Add enemy
                SpawnEnemy();
            }

            //Update the enemies
            for (int enemyIndex = Enemies.Count - 1; enemyIndex >= 0; enemyIndex--)
            {
                Enemies[enemyIndex].Update(gameTime, player.Position);
                if (Enemies[enemyIndex].Active == false)
                {
                    Enemies.RemoveAt(enemyIndex);
                }
            }
        }

        
    }
}
