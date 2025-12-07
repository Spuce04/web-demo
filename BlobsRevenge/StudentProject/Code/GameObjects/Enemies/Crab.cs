using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;
using System;

namespace StudentProject.Code.GameObjects.Enemies
{
    class Crab : GameObject
    {
        private Level _level;
        private Blob _blob;
        private CrabClaw crabClaw;

        private int MaxStepCount = 100;
        private int LeftStepCount = 0;
        private int RightStepCount = 0;

        private bool MovingLeft;
        private bool MovingRight;

        public int health { get; set; }

        private int clawSpeed;

        public Crab(Level level, Blob blob, int ClawSpeed, int Health)
        {
            SetSprite("Crab", 96, 87, 0.25f, new int[] { 4 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            _blob = blob;
            clawSpeed = ClawSpeed;
            health = Health;
            _level = level;

            _level.EnemyCount++;
        }
        public override void Update(float deltaTime)
        {
            MoveUp();
            AddClaw();
            StepLeft();
            StepRight();
            RandomMovement();
            AtWorldEdge();
            CheckCollisions();
            RemoveCrab();
        }

        // moves the crab up until it reaches a certain Y position, this method is here because the crab objects are created under the world size
        public void MoveUp()
        {
            if (GetY() > (int)Settings.GameResolution.Y - 200)
            {
                SetPosition(GetX(), GetY() - 2);
            }
        }

        public void AddClaw()
        {
            if (crabClaw == null)
            {
                crabClaw = new CrabClaw(this, _blob, clawSpeed);
                GetScreen().AddObject(crabClaw, (int)GetX(), (int)GetY());
            }
        }

        // if step left is chosen from the random movement method, then the crab will move left until it reaches the maximum step count and as long as its at least 50 pixels to the right of the screen
        public void StepLeft()
        {
            if (LeftStepCount >= 0 && LeftStepCount < MaxStepCount && MovingLeft == true)
            {
                if (GetX() > 50)
                {
                    SetPosition(GetX() - 4, GetY());
                }
                LeftStepCount++;
            }
            if (LeftStepCount == MaxStepCount)
            {
                LeftStepCount = 0;
                MovingLeft = false;
            }
        }

        // if step right is chosen from the random movement method, then the crab will move right until it reaches the maximum step count and as long as its at least 50 pixels to the left of the screen
        public void StepRight()
        {
            if (RightStepCount >= 0 && RightStepCount < MaxStepCount && MovingRight == true)
            {
                if (GetX() < (int)Settings.GameResolution.X - 50)
                {
                    SetPosition(GetX() + 4, GetY());
                }
                RightStepCount++;
            }
            if (RightStepCount == MaxStepCount)
            {
                RightStepCount = 0;
                MovingRight = false;
            }
        }

        // creates the chance of when the crab will move and in what direction
        public void RandomMovement()
        {
            int Direction = new Random().Next(0, 50);

            if (Direction == 1 && MovingRight == false)
            {
                MovingLeft = true;
            }

            if (Direction == 2 && MovingLeft == false)
            {
                MovingRight = true;
            }
        }

        private void CheckCollisions()
        {
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

        // if the crab somehow reaches the edge of the world, then its move method opposite of the edge it's at will be activated
        public void AtWorldEdge()
        {
            if (GetX() < 0)
            {
                MovingRight = true;
            }
            if (GetX() > (int)Settings.GameResolution.X)
            {
                MovingLeft = true;
            }
        }

        // once the crab reaches 0 health, then it will remove itself and its designated claw projectile, and has a chance to drop a heart and a random amount of coins
        public void RemoveCrab()
        {
            if (health <= 0)
            {
                GetScreen().RemoveObject(crabClaw);
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
                _level.CrabKillCount++;
                _level.EnemyCount--;
            }
        }
    }
}
