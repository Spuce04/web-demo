using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using System;

namespace StudentProject.Code.GameObjects.Consumables
{
    internal class Coin : GameObject
    {
        int value;
        Vector2 Velocity = new Vector2(new Random().Next(-5, 5), new Random().Next(-5, 5));// picks a random velocity for the coin to head in to create a burst-out like effect

        public Coin(int Value)
        {
            SetSprite("Coin", 24, 0.25f, new int[] { 4 });
            value = Value;
        }

        public override void Update(float deltaTime)
        {
            CheckCollisions();
            MoveLeft();
            BurstOut();
            AtWorldEdge();
        }

        // adds the amount of coins to the blob based on the coins given value
        public void CheckCollisions()
        {
            GameObject blob = GetOneIntersectingObject<Blob>();
            if (blob != null)
            {
                GetScreen().GetOneObjectOfType<Blob>().AddCoins(value);
                GetScreen().RemoveObject(this);
            }
        }

        // gives the illusion of moving right to the player
        public void MoveLeft()
        {
            SetPosition(GetX() - 2, GetY());
        }

        // the coin will move in a random direction and gradually slow down to 0 Velocity
        public void BurstOut()
        {
            Velocity = new Vector2((float)Math.Round(Velocity.X * 4) / 4.0f, (float)Math.Round(Velocity.Y * 4) / 4.0f);

            if (Velocity.X > 0f)
            {
                Velocity.X -= 0.25f;
            }
            else if (Velocity.X < 0f)
            {
                Velocity.X += 0.25f;
            }

            if (Velocity.Y > 0f)
            {
                Velocity.Y -= 0.25f;
            }
            else if (Velocity.Y < 0f)
            {
                Velocity.Y += 0.25f;
            }

            SetPosition(GetPosition() + Velocity);
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
