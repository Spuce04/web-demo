using MonoGameEngine.StandardCore;

namespace StudentProject.Code.GameObjects.Consumables
{
    class ReloadBubble : GameObject
    {
        public ReloadBubble()
        {
            SetSprite("Bubble");
            GetSprite().SetScale(1.5f, 1.5f);
        }
        public override void Update(float deltaTime)
        {
            Move();
            AtWorldEdge();
        }

        // gives the illusion of moving right to the player, and also makes the bubble object rise to the surface
        public void Move()
        {
            SetPosition(GetX() - 2, GetY() - 1);
        }

        private void AtWorldEdge()
        {
            if (GetPosition().X <= 0f - GetSprite().GetWidth())
            {
                GetScreen().RemoveObject(this);
            }
        }
    }
}
