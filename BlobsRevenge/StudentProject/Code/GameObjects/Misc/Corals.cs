using MonoGameEngine.StandardCore;
using System;

namespace StudentProject.Code.GameObjects.Misc
{
    class Corals : GameObject
    {
        public Corals()
        {
            int CoralType = new Random().Next(4);

            SetSprite("Corals", 96, 0, new int[] { 1 });
            GetAnimatedSprite().SetFrameNumber(CoralType);
            GetSprite().SetOrigin(0.5f, 0.5f);
        }
        public override void Update(float deltaTime)
        {
            SetPosition(GetX() - 2, GetY());
        }
    }
}
