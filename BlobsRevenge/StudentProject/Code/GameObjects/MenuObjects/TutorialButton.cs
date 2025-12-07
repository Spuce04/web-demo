namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class TutorialButton : Button
    {
        public TutorialButton()
        {
            SetSprite("TutorialButton");
            GetSprite().SetOrigin(0.5f, 0.5f);
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
        }
    }
}
