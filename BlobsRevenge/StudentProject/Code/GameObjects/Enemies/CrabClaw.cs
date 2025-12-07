using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Weapons;
using System;

namespace StudentProject.Code.GameObjects.Enemies
{
    class CrabClaw : GameObject
    {
        private Crab _crab;
        private Blob _blob;

        private Vector2 Direction;
        private Vector2 BurstDirection;
        private Vector2 CrabDirection;

        private float speed;
        private float Angle;

        private float BurstFrequency = 5f;// how often the claw bursts in seconds (lower = faster)
        private float BurstLast = 2.5f;// how long each burst lasts in seconds
        public float BurstTimer { get; set; }

        private bool Deflected = false;

        public CrabClaw(Crab crab, Blob blob, float Speed)
        {
            _crab = crab;
            _blob = blob;
            speed = Speed;

            SetSprite("CrabClaw");
            GetSprite().SetOrigin(0.5f, 0.5f);
        }

        public override void Update(float deltaTime)
        {
            CrabDirection = GetPosition() - _crab.GetPosition(); // used to go towards the crab after moving towards the blob
            CrabDirection.Normalize();
            Burst(deltaTime);
            RemoveClaw();
            GetSprite().SetRotation(GetSprite().GetRotation() + 10);
        }

        private void Burst(float deltaTime)
        {
            if (BurstTimer < BurstFrequency)// everything that happens in between bursts
            {
                Rotation();
                BurstDirection = Direction; // constantly updates the burst direction until the last frame so the objects goes towards the blob objects most recent position but doesn't follow it
                int BoxSize = 50; // used as a makeshift hitbox since the claws speed is to fast to touch the crabs exact position when going back towards the crab

                if (GetX() < _crab.GetX() + BoxSize && GetX() > _crab.GetX() - BoxSize && GetY() < _crab.GetY() + BoxSize && GetY() > _crab.GetY() - BoxSize)
                {
                    SetPosition(_crab.GetPosition());

                    if (Deflected == true) // if the player succesfully hits the claw object while its going towards the blob, it will be considered deflected and take health off the crab
                    {
                        _crab.health--;
                        Deflected = false;
                    }
                }
                else
                {
                    SetPosition(GetPosition() - CrabDirection * deltaTime * speed); // moves towards the crabs position while its not in the box size range
                }
            }
            if (BurstTimer > BurstFrequency && BurstTimer < BurstLast + BurstFrequency)// everything that happens during bursts
            {
                SetPosition(GetPosition() - BurstDirection * deltaTime * speed);// heads towards the blobs most recent position before the "burst" starts

                GameObject bubble = GetOneIntersectingObject<Bubble>();
                if (bubble != null)
                {
                    BurstTimer = 0f; // timer is set to 0 so the claw immediatly starts moving towards the crab
                    GetScreen().RemoveObject(bubble);
                    Deflected = true; //if the player succesfully hits the claw object while its going towards the blob, it will be considered deflected
                }
                GameObject sniperBubble = GetOneIntersectingObject<SniperBubble>();
                if (sniperBubble != null)
                {
                    BurstTimer = 0f; // timer is set to 0 so the claw immediatly starts moving towards the crab
                    Deflected = true; // if the player succesfully hits the claw object while its going towards the blob, it will be considered deflected
                }
                GameObject laserBeam = GetOneIntersectingObject<LaserBeam>();
                if (laserBeam != null)
                {
                    BurstTimer = 0f;
                    Deflected = true;
                }
            }
            BurstTimer += deltaTime;
            if (BurstTimer > BurstLast + BurstFrequency)
            {
                BurstTimer = 0;
            }
        }

        // uses the direction to calculate what angle is between the Claw and the mouse, and sets the blobs sprite to always face the mouse
        private void Rotation()
        {
            Direction = GetPosition() - _blob.GetPosition();
            Direction.Normalize();
            Angle = (float)Math.Atan2(Direction.Y, Direction.X);
            Angle = MathHelper.ToDegrees(Angle);
            GetSprite().SetRotation(Angle - 90);
        }

        // removes itself if the crab enemy is at 0 health
        public void RemoveClaw()
        {
            if (_crab.health <= 0)
            {
                GetScreen().RemoveObject(this);
            }
        }
    }
}