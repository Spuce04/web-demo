using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.Misc;
using System;

namespace StudentProject.Code.GameObjects.BlobStuff
{
    class BlobDisplay : GameObject
    {
        private Blob _blob;

        private Text Lives;
        private Text Coins;

        Vector2 Direction;
        float Angle;

        public BlobDisplay(Blob blob)
        {
            SetSprite("Blob", 96, 0.25f, new int[] { 4, 4, 4, 4, 4, 4, 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetScale(2, 2);
            _blob = blob;
        }

        public override void Update(float deltaTime)
        {
            DisplayStats();
            Rotation();
        }

        public void DisplayStats()// displays the values of the blob which can be affected by the player in the upgrade menu
        {
            if (Lives == null && Coins == null)
            {
                Lives = new Text("", Color.White);
                Coins = new Text("", Color.White);

                GetScreen().AddText(Lives, (int)GetX() - 72, (int)GetY() + 90);
                GetScreen().AddText(Coins, (int)GetX() - 72, (int)GetY() + 120);

                GetScreen().AddObject(new HeartSprite(), (int)GetX() - 90, (int)GetY() + 100);
                GetScreen().AddObject(new CoinSprite(), (int)GetX() - 90, (int)GetY() + 130);
            }
            else
            {
                Coins.SetMessage("x" + _blob.Coins);
                Lives.SetMessage("x" + _blob.MaxLives);
            }
        }

        // uses the direction to calculate what angle is between the blob and the mouse, and sets the blobs sprite to always face the mouse
        private void Rotation()
        {
            Direction = GameInput.GetMousePosition() - GetPosition();// works out the direction between the mouse and the blob
            Direction.Normalize();

            if (GameInput.GetMousePosition() != GetPosition())
            {
                Angle = (float)Math.Atan2(Direction.Y, Direction.X);
                Angle = MathHelper.ToDegrees(Angle);
                GetSprite().SetRotation(Angle);
            }
        }
    }
}
