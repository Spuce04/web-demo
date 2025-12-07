using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;
using System;

namespace StudentProject.Code.GameObjects.Enemies
{
    internal class Shark : GameObject
    {
        Level _level;
        Blob _blob;

        private float Velocity;
        private float maxVelocity;
        private float Acceleration = -2f;// the rate of speed that the shark slows down when their velocity increases

        Vector2 Direction;
        Vector2 BurstDirection;
        private float Angle;
        private float BurstFrequency = 2f;// how often the shark bursts in seconds (lower = faster)
        private float BurstLast = 3f;// how long each burst lasts in seconds
        private float BurstTimer;

        public int health { get; set; }// how many times the shark will get hit before dying

        public Shark(Level level, Blob blob, float MaxVelocity, int Health)
        {
            SetSprite("Shark", 192, 96, 0.25f, new int[] { 4, 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().FlipHorizontally(true);

            _level = level;
            _blob = blob;

            maxVelocity = MaxVelocity;
            health = Health;

            _level.EnemyCount++;
        }

        public override void Update(float deltaTime)
        {
            Burst(deltaTime);
            CheckCollisions(deltaTime);
            RemoveShark();
        }

        private void Burst(float deltaTime)
        {
            if (BurstTimer < BurstFrequency)// everything that happens in between bursts
            {
                Rotation();
                BurstDirection = Direction;
                Velocity = maxVelocity;
                SetPosition(GetX() - 2, GetY());
            }
            if (BurstTimer > BurstFrequency && BurstTimer < BurstLast + BurstFrequency)// everything that happens during bursts
            {
                SetPosition(GetPosition() - BurstDirection * deltaTime * Velocity);
                if (Velocity > 0)
                {
                    Velocity += Acceleration;
                }
            }
            BurstTimer += deltaTime;
            if (BurstTimer > BurstLast + BurstFrequency)
            {
                BurstTimer = 0;
            }
        }
        private void Rotation()
        {
            Direction = GetPosition() - _blob.GetPosition();
            Direction.Normalize();
            Angle = (float)Math.Atan2(Direction.Y, Direction.X);
            Angle = MathHelper.ToDegrees(Angle);
            GetSprite().SetRotation(Angle);
        }

        private void CheckCollisions(float deltaTime)
        {
            GameObject bubble = GetOneIntersectingObject<Bubble>();
            if (bubble != null)
            {
                GetScreen().RemoveObject(bubble);
                health--;
            }

            GameObject sniperBubble = GetOneIntersectingObject<SniperBubble>();
            if (sniperBubble != null)
            {
                health = 0;
            }

            GameObject laserBeam = GetOneIntersectingObject<LaserBeam>();
            if (laserBeam != null)
            {
                health = 0;
            }
        }

        public void RemoveShark()
        {
            if (health <= 0)
            {
                GetScreen().RemoveObject(this);

                int i = new Random().Next(1, 5);
                if (i == 1)
                {
                    Heart heart = new Heart(_blob, GetPosition());
                    GetScreen().AddObject(heart, (int)GetX(), (int)GetY());
                }
                for (int j = 0; j < i; j++)
                {
                    Coin coin = new Coin(1);
                    GetScreen().AddObject(coin, (int)GetX(), (int)GetY());
                }
                _level.SharkKillCount++;
                _level.EnemyCount--;
            }
        }
    }
}
