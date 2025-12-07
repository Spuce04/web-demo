using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentProject.Code.GameObjects.Misc
{
    internal class Clam : GameObject
    {
        bool Open = false;
        public Clam() 
        {
            SetSprite("Clam", 60, 51, 0.5f, new int[] { 2, 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetLayerDepth(2);
        }

        public override void Update(float deltaTime)
        {
            MoveLeft();
            CheckColisions();
            AtWorldEdge();
        }

        // gives the illusion of moving right to the player
        public void MoveLeft()
        {
            SetPosition(GetX() - 2, GetY());
        }

        private void CheckColisions()
        {
            GameObject bubble = GetOneIntersectingObject<Bubble>();
            GameObject sniperBubble = GetOneIntersectingObject<SniperBubble>();
            GameObject laserBeam = GetOneIntersectingObject<LaserBeam>();

            if (bubble != null || sniperBubble != null || laserBeam != null)
            {
                if (Open == false)
                {
                    GetAnimatedSprite().StartAnimation(1);
                    GetScreen().AddObject(new Pearl(this), (int)GetX(), (int)GetY());
                    Open = true;
                }
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
