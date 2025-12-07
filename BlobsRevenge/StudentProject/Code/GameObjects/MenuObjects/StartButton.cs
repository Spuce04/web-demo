using MonoGameEngine;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.Screens;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    class StartButton : Button
    {
        Blob _blob;
        public StartButton(Blob blob)
        {
            SetSprite("StartButton", 270, 90, 0f, new int[] { 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            _blob = blob;
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            ChangeSprite();
            Start();
        }

        private void Start()
        {
            if (Clicked == true)
            {
                Transition.Instance.ToScreen<Level>();
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
