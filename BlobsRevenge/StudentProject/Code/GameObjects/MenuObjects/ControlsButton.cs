using StudentProject.Code.GameObjects.Misc;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class ControlsButton : Button
    {
        private Controls controls;
        public ControlsButton()
        {
            SetSprite("ControlsButton", 414, 90, 0f, new int[] { 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            ChangeSprite();
            DisplayControls();
        }

        private void ChangeSprite()
        {
            if (Active != null)
            {
                GetAnimatedSprite().SetFrameNumber(1);
            }
            else
            {
                GetAnimatedSprite().SetFrameNumber(0);
            }
        }

        private void DisplayControls()
        {
            if (Clicked == true && controls == null)
            {
                controls = new Controls();
                GetScreen().AddObject(controls, (int)GetX(), (int)GetY());
                Clicked = false;
            }
            if (Clicked == true && controls != null)
            {
                GetScreen().RemoveObject(controls);
                controls = null;
                Clicked = false;
            }
            if (Active == null && controls != null)
            {
                GetScreen().RemoveObject(controls);
                controls = null;
            }
        }
    }
}
