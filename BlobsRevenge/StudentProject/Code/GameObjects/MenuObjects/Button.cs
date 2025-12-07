using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.Misc;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class Button : GameObject
    {
        public bool Clicked = false;

        MouseState prevmouseState;
        MouseState mouseState;

        public GameObject Active { get; set; }

        public Button()
        {
            Clicked = false;
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
        }

        public void CheckIfClicked()
        {
            Active = GetOneIntersectingObject<Crosshair>();

            if (Active != null)
            {
                GetSprite().SetColour(Color.White);

                prevmouseState = mouseState;
                mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
                {
                    Clicked = true;
                }
                else if (Clicked == true)
                {
                    Clicked = false;
                }
            }
            else
            {
                GetSprite().SetColour(Color.Gray);
            }
        }
    }
}
