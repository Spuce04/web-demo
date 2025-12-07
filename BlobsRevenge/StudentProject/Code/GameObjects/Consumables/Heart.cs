using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Consumables
{
    internal class Heart : GameObject
    {
        Blob _blob;
        public Heart(Blob blob, Vector2 SpawnPoint)
        {
            _blob = blob;
            SetSprite("Heart");
            GetSprite().SetLayerDepth(4);
            SetPosition(SpawnPoint);
        }
        public override void Update(float deltaTime)
        {
            CheckCollisions();
            MoveLeft();
            AtWorldEdge();
        }

        // adds one life to the blob when the heart and blob objects collide and the blob doesnt have their cap lives, and deletes itself
        public void CheckCollisions()
        {
            Blob blob = (Blob)GetOneIntersectingObject<Blob>();
            if (blob != null && blob.Lives > 0)
            {
                if (_blob.Lives < _blob.MaxLives)
                {
                    _blob.Lives++;
                }
                GetScreen().RemoveObject(this);
            }
        }

        // gives the illusion of moving right to the player
        public void MoveLeft()
        {
            SetPosition(GetX() - 2, GetY());
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
