using Microsoft.Xna.Framework.Input;
using StudentProject.Code.GameObjects.BlobStuff;
using System.Linq;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class Laser : Weapon
    {
        Blob _blob;

        LaserBeam[] lasers = new LaserBeam[25];
        public Laser(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(5);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(0.05f, 0.01f, 0.005f, 0.001f, 0.0005f);
            AccuracyLevels(300, 270, 250, 220, 200);
            MaxMagazineSizeLevels(30, 40, 50, 75, 100);

            InitialStats(FireRateLevel[0], 3f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            LaserControl();
        }

        public void LaserControl()
        {
            if (_blob.Lives > 0)
            {
                MouseState MouseState = Mouse.GetState();
                if (MouseState.LeftButton == ButtonState.Pressed && lasers.All(laser => laser == null))
                {
                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i] = new LaserBeam(_blob, i * 20);
                        GetScreen().AddObject(lasers[i], (int)_blob.GetPosition().X, (int)_blob.GetPosition().Y);
                    }
                }

                if (MouseState.LeftButton == ButtonState.Released && lasers.All(laser => laser != null) || _blob.Lives == 0)
                {
                    for (int i = 0; i < lasers.Length; i++)
                    {
                        GetScreen().RemoveObject(lasers[i]);
                        lasers[i] = null;
                    }
                }
            }
            else if (_blob.Lives <= 0)
            {
                for (int i = 0; i < lasers.Length; i++)
                {
                    GetScreen().RemoveObject(lasers[i]);
                    lasers[i] = null;
                }
            }
        }
    }
}
