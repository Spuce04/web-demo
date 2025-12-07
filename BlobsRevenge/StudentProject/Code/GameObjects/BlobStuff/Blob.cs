using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Enemies;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;
using System;

namespace StudentProject.Code.GameObjects.BlobStuff
{
    public class Blob : GameObject
    {
        public SingleShot singleShot { get; set; }
        public TwoShot twoShot { get; set; }
        public Shotgun shotgun { get; set; }
        public Minigun minigun { get; set; }
        public Sniper sniper { get; set; }
        public Laser laser { get; set; }

        public int ProgressCount { get; set; } = 1;

        private Vector2 Direction;

        private Vector2 Velocity;
        public float Acceleration { get; set; } = 1f;// how fast the blobs velocity changes when they move with WASD
        public float MaxSpeed { get; set; } = 5f;// how fast the player can go before their velocity starts decreasing

        private float Angle;

        private bool HasShot = true;

        public int MaxLives { get; set; } = 1;
        public int Lives { get; set; }
        public int Coins { get; set; } = 0;


        public bool Dead { get; set; } = false;
        public bool IsHit { get; set; } = false;
        private float InvincibilityTimer;
        private float MaxInvincibilityTime = 0.5f;

        public bool Reloading { get; set; }
        public Weapon CurrentWeapon { get; set; }

        public int SpriteNumber { get; set; } = 0;


        public Blob()
        {
            // sets the sprites image, size, animation, and anchorpoint
            SetSprite("Blob", 96, 0.25f, new int[] { 4, 4, 4, 4, 4, 4, 1 });
            GetAnimatedSprite().StartAnimation(SpriteNumber);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetLayerDepth(1);
        }

        public override void Update(float deltaTime)
        {
            if (Lives > 0)
            {
                HandleInput();
                SetPosition(GetPosition() + Velocity);// sets the position corrosponding to the velocity calculated from the handle input method
                AtWorldEdge();
                CheckCollisions();
                CheckIfHit(deltaTime);
                Rotation();
            }
            else if (Lives <= 0)
            {
                Death();
            }
        }

        private void HandleInput()
        {
            // rounds the velocity so there's no problems after doing any calclations
            Velocity = new Vector2((float)Math.Round(Velocity.X * 4) / 4.0f, (float)Math.Round(Velocity.Y * 4) / 4.0f);

            // control keys, gradually increases velocity by a set accelration until it reaches the speed cap
            if (GameInput.IsKeyHeld("W"))
            {
                if (Velocity.Y > -MaxSpeed)
                {
                    Velocity.Y -= Acceleration;
                }
            }
            if (GameInput.IsKeyHeld("A"))
            {
                if (Velocity.X > -MaxSpeed)
                {
                    Velocity.X -= Acceleration;
                }
            }
            if (GameInput.IsKeyHeld("S"))
            {
                if (Velocity.Y < MaxSpeed)
                {
                    Velocity.Y += Acceleration;
                }
            }
            if (GameInput.IsKeyHeld("D"))
            {
                if (Velocity.X < MaxSpeed)
                {
                    Velocity.X += Acceleration;
                }
            }

            // slows the player down in the opposite direction until they reach a stop
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
        }

        // creates a kickback/ recoil affect on the object in the opposite direction of the bubbles direction
        public void Recoil(float Recoilstrength)
        {
            HasShot = false;

            if (GameInput.GetMousePosition() != GetPosition()) // doesnt use recoil if the mouse is on the blob because that = 0,0 and removes the blob object
            {
                Direction = GameInput.GetMousePosition() - GetPosition();
                Direction.Normalize();

                Velocity -= Direction * Recoilstrength;
            }
        }

        // keeps the player within the game borders and makes their velocity 0 to make sure they don't slip out or exploit the game
        private void AtWorldEdge()
        {
            if (GetX() < 0)
            {
                Velocity.X = 0;
                SetPosition(0, GetY());
            }
            if (GetX() > (int)Settings.GameResolution.X)
            {
                Velocity.X = 0;
                SetPosition((int)Settings.GameResolution.X, GetY());
            }
            if (GetY() < 0)
            {
                Velocity.Y = 0;
                SetPosition(GetX(), 0);
            }
            if (GetY() > (int)Settings.GameResolution.Y)
            {
                Velocity.Y = 0;
                SetPosition(GetX(), (int)Settings.GameResolution.Y);
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

        private void CheckCollisions()
        {
            //if the blob touches an enemy object that isn't a boss, then the blob will lose a life, the enemies health will be 0 and it will count as killing it, and the blobs Invincibility-frames will start
            Jellyfish jellyfish = (Jellyfish)GetOneIntersectingObject<Jellyfish>();
            if (jellyfish != null && IsHit == false)
            {
                IsHit = true;
                jellyfish.health = 0;
                GetAnimatedSprite().StartAnimation(6);
                Lives--;
                Camera2D.Instance.Shake(10.0f, 0.25f);
            }

            Shark shark = (Shark)GetOneIntersectingObject<Shark>();
            if (shark != null && IsHit == false)
            {
                IsHit = true;
                shark.health = 0;
                GetAnimatedSprite().StartAnimation(6);
                Lives--;
                Camera2D.Instance.Shake(4.0f, 0.25f);
            }

            Crab crab = (Crab)GetOneIntersectingObject<Crab>();
            if (crab != null && IsHit == false)
            {
                IsHit = true;
                GetAnimatedSprite().StartAnimation(6);
                Lives--;
                Camera2D.Instance.Shake(4.0f, 0.25f);
            }

            CrabClaw crabclaw = (CrabClaw)GetOneIntersectingObject<CrabClaw>();
            if (crabclaw != null && IsHit == false)
            {
                crabclaw.BurstTimer = 0; // sets the crab claws burst timer back to 0 so it immediatly moves back towards the crab
                IsHit = true;
                GetAnimatedSprite().StartAnimation(6);
                Lives--;
                Camera2D.Instance.Shake(4.0f, 0.25f);
            }

            // if the blob touches a reload bubble, then the bubble will dissapear and the weapons magazine will reload back to full
            GameObject reloadBubble = GetOneIntersectingObject<ReloadBubble>();
            if (reloadBubble != null)
            {
                CurrentWeapon.Reload();
                GetScreen().RemoveObject(reloadBubble);
            }

        }

        // if the blob is touched by an enemy, then a timer will go off for its invincibility frames, meaning it wont be affected by enemies while that timer is less than its set maximum time
        private void CheckIfHit(float deltaTime)
        {
            if (InvincibilityTimer < MaxInvincibilityTime && IsHit == true)
            {
                InvincibilityTimer += deltaTime;
            }
            if (InvincibilityTimer >= MaxInvincibilityTime && IsHit == true)
            {
                IsHit = false;
                InvincibilityTimer = 0;
                GetAnimatedSprite().StartAnimation(SpriteNumber);
            }
        }

        // when the blob has 0 lives, it will be considered dead and move towards the top of the screen to look like a dead fish floating up water
        public void Death()
        {
            SetPosition(GetX(), GetY() - 4);

            if (GetY() < -5 && Dead == false)
            {
                Dead = true;
                Transition.Instance.ToScreen<UpgradeMenu>();
            }
        }

        public void AddCoins(int Amount)
        {
            Coins += Amount;
        }

        // resets the blobs values which makes it functional for the next time the player plays
        public void ResetStats()
        {
            Lives = MaxLives;
            Velocity = new Vector2(0, 0);
            Dead = false;
        }
    }
}