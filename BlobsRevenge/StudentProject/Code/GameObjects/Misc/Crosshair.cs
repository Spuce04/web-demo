using Microsoft.Xna.Framework.Input;
using MonoGameEngine.StandardCore;

namespace StudentProject.Code.GameObjects.Misc
{
    public class Crosshair : GameObject
    {
        MouseState mouseState;

        public Crosshair()
        {
            // sets the sprites image, size, animation, anchorpoint and layer
            SetSprite("Bubble");
            GetSprite().SetScale(0.5f, 0.5f);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);
        }
        // sets the position to always be wherever the mouse is
        public override void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();
            SetPosition(mouseState.X, mouseState.Y);
        }
    }
}
