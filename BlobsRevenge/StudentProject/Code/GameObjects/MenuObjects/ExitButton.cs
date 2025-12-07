using System;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class ExitButton : Button
    {
        public ExitButton()
        {
            SetSprite("ExitButton", 225, 90, 0f, new int[] { 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            Exit();
            ChangeSprite();
        }

        public void Exit()
        {
            if (Clicked == true)
            {
                Environment.Exit(0);
            }
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
    }
}
