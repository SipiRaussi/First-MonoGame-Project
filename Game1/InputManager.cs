using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    public class InputManager
    {
        private KeyboardState currentKeyboardState, previousKeyboardState;

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

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            if(!ScreenManager.Instance.IsTransitioning)
            {
                currentKeyboardState = Keyboard.GetState();
            }
        }

    }
}
