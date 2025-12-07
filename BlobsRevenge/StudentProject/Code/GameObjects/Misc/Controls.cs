using Microsoft.Xna.Framework.Input;
using MonoGameEngine.StandardCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentProject.Code.GameObjects.Misc
{
    internal class Controls : GameObject
    {
        MouseState mouseState;
        public Controls()
        {
            SetSprite("Controls");
            GetSprite().SetOrigin(0.5f, 0.5f);
        }

        public override void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();
            SetPosition(mouseState.X, mouseState.Y);
        }
    }
}
