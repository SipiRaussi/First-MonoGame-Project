﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;

        private int itemNumber;
        private string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        private void AlignMenuItem()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (MenuItem item in Items)
            {
                dimensions += new Vector2(item.Image.SourceRectangle.Width,
                                          item.Image.SourceRectangle.Height);
            }
            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                                     (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);
            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                {
                    item.Image.Position = new Vector2(dimensions.X,
                                                      (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRectangle.Height) / 2);
                }
                else if (Axis == "Y")
                {
                    item.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X - item.Image.SourceRectangle.Width) / 2,
                                                      dimensions.Y);
                }

                dimensions += new Vector2(item.Image.SourceRectangle.Width,
                                          item.Image.SourceRectangle.Height);
            }
        }

        public Menu()
        {
            id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (string effect in split)
                {
                    item.Image.ActivateEffect(effect);
                }
            }
            AlignMenuItem();
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items)
            {
                item.Image.UnloadContent();
            }
        }

        //asdasdasd test
        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if(InputManager.Instance.KeyPressed(Keys.Right))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                {
                    itemNumber--;
                }
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                {
                    itemNumber--;
                }
            }

            if(itemNumber < 0)
            {
                itemNumber = 0;
            }
            else if (itemNumber > Items.Count -1)
            {
                itemNumber = Items.Count - 1;
            }

            for(int i = 0; i < Items.Count; i ++)
            {
                if(i == itemNumber)
                {
                    Items[i].Image.IsActive = true;
                }
                else
                {
                    Items[i].Image.IsActive = false;
                }

                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(MenuItem item in Items)
            {
                item.Image.Draw(spriteBatch);
            }
        }

    }
}
