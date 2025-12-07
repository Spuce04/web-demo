using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentProject.Code.GameObjects.Consumables
{
    internal class Pearl : GameObject
    {
        Clam _clam;
        public Pearl(Clam clam)
        {
            _clam = clam;
            SetSprite("Pearl");
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetLayerDepth(1);
        }

        public override void Update(float deltaTime)
        {
            SetPosition(_clam.GetX() -3, _clam.GetY() - 20);
            CheckCollisions();
            AtWorldEdge();
        }

        public void CheckCollisions()
        {
            Blob blob = (Blob)GetOneIntersectingObject<Blob>();
            if (blob != null)
            {
                GetScreen().GetOneObjectOfType<Blob>().AddCoins(blob.ProgressCount * 5);
                GetScreen().RemoveObject(this);
            }
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
