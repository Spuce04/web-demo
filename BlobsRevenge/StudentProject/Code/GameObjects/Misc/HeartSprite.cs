using MonoGameEngine.StandardCore;

namespace StudentProject.Code.GameObjects.BlobStuff
{
    class HeartSprite : GameObject
    {
        public HeartSprite()
        {
            SetSprite("Heart");
            GetSprite().SetOrigin(0.5f, 0.5f);
        }
        public override void Update(float deltaTime)
        {

        }
    }
}
