using MonoGameEngine.StandardCore;

namespace StudentProject.Code.GameObjects.Misc
{
    internal class Title : GameObject
    {
        public Title()
        {
            SetSprite("Title");
            GetSprite().SetOrigin(0.5f, 0.5f);
        }

        public override void Update(float deltaTime)
        {

        }
    }
}