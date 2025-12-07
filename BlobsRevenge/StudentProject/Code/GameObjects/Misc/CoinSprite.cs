using MonoGameEngine.StandardCore;

namespace StudentProject.Code.GameObjects.Misc
{
    public class CoinSprite : GameObject
    {
        public CoinSprite()
        {
            SetSprite("Coin", 24, 0.25f, new int[] { 4 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);
        }

        public override void Update(float deltaTime)
        {

        }
    }
}
