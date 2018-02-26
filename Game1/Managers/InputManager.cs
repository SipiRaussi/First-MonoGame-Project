using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    public class InputManager
    {
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private MouseState currentMouseState, previousMouseState;

        //Singleton
        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Returns true if defined key is once pressed
        /// </summary>
        /// <param name="keys">Any keyboard key</param>
        /// <returns></returns>
        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if defined key is released.
        /// </summary>
        /// <param name="keys">Any keyboard key</param>
        /// <returns></returns>
        public bool KeyUp(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if(currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if defined key is hold down.
        /// </summary>
        /// <param name="keys">Any keyboard key</param>
        /// <returns></returns>
        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyboardState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if left mouse button is pressed once.
        /// </summary>
        /// <returns></returns>
        public bool MouseLeftPressed()
        {
            if ((currentMouseState.LeftButton == ButtonState.Pressed) &&
                (previousMouseState.LeftButton == ButtonState.Released))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if left mouse button is released.
        /// </summary>
        /// <returns></returns>
        public bool MouseLeftUp()
        {
            if ((currentMouseState.LeftButton == ButtonState.Released) &&
                (previousMouseState.LeftButton == ButtonState.Pressed))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if mouse left is hold down.
        /// </summary>
        /// <returns></returns>
        public bool MouseLeftDown()
        {
            if ((currentMouseState.LeftButton == ButtonState.Pressed))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Mouses position.
        /// </summary>
        /// <returns>Position of the mouse</returns>
        public Point MousePosition()
        {
            return currentMouseState.Position;
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            if(!ScreenManager.Instance.IsTransitioning)
            {
                currentKeyboardState = Keyboard.GetState();
                currentMouseState = Mouse.GetState();
            }
        }

    }
}
