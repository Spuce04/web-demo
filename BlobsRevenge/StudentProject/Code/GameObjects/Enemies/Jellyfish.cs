using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;
using System;

namespace StudentProject.Code.GameObjects.Enemies
{
    class Jellyfish : GameObject
    {
        Level _level;
        Blob _blob;

        private float Velocity;
        private float maxVelocity;
        private float Acceleration = -4f;// the rate of speed that the jellyfish slows down when their velocity increases

        Vector2 Direction;
        Vector2 BurstDirection;
        private float Angle;
        private float BurstFrequency = 4f;// how often the jellyfish bursts in seconds (lower = faster)
        private float BurstLast = 1.5f;// how long each burst lasts in seconds
        private float BurstTimer;

        public int health { get; set; }// how many times the jellyfish will get hit before dying
        bool IsHit;
        float InvincibilityTimer;
        float InvincibilityTime = 0.25f;

        public Jellyfish(Level level, Blob blob, float MaxVelocity, int Health)
        {
            SetSprite("Jellyfish", 96, 0.25f, new int[] { 5, 1 });
            GetAnimatedSprite().StartAnimation(0);
            GetSprite().SetOrigin(0.5f, 0.5f);

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
            RemoveJellyfish();
        }
        private void Burst(float deltaTime)
        {
            if (BurstTimer < BurstFrequency)// everything that happens in between bursts
            {
                BurstDirection = Direction;
                Velocity = maxVelocity;
                SetPosition(GetPosition() - Direction * deltaTime * 150);
                Rotation();
            }
            if (BurstTimer > BurstFrequency && BurstTimer < BurstLast + BurstFrequency)// everything that happens during bursts
            {
                SetPosition(GetPosition() - BurstDirection * deltaTime * Velocity);
                if (Velocity > 0)
                {
                    Velocity += Acceleration;
                }
                if (Velocity == maxVelocity)
                {
                    SetSprite("Jellyfish", 96, 0.05f, new int[] { 5 });
                    GetSprite().SetOrigin(0.5f, 0.5f);
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
            GetSprite().SetRotation(Angle - 90);
        }

        private void CheckCollisions(float deltaTime)
        {
            GameObject bubble = GetOneIntersectingObject<Bubble>();
            if (bubble != null)
            {
                GetScreen().RemoveObject(bubble);
                IsHit = true;
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

            if (InvincibilityTimer < InvincibilityTime && IsHit == true)
            {
                InvincibilityTimer += deltaTime;
                GetAnimatedSprite().StartAnimation(1);
            }
            if (InvincibilityTimer >= InvincibilityTime && IsHit == true)
            {
                IsHit = false;
                InvincibilityTimer = 0;
                GetAnimatedSprite().StartAnimation(0);
            }
        }

        public void RemoveJellyfish()
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
                _level.JellyfishKillCount++;
                _level.EnemyCount--;
            }
        }
    }
}
