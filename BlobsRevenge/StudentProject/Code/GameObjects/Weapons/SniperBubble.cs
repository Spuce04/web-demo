using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using System;

namespace StudentProject.Code.GameObjects.Weapons
{
    class SniperBubble : GameObject
    {
        Vector2 Direction;
        int speed;// how fast the bubble travels (higher = faster)
        float accuracy;

        // sets the sprites image, anchor point, and scale. Also calcualtes the direction between its original position and the mouse position
        public SniperBubble(Vector2 Blobpos, int Accuracy, int Speed)
        {
            SetSprite("ProjectileBubble", 36, 27, 0.5f, new int[] { 4 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetLayerDepth(6);
            SetPosition(Blobpos);

            accuracy = new Random().Next(-Accuracy, Accuracy);// uses the accuracy variable from the weapon shooting the bubble

            speed = Speed;

            Rotation();
        }
        public override void Update(float deltaTime)
        {
            MoveTowardsMouse(deltaTime);
            AtWorldEdge();
        }

        private void MoveTowardsMouse(float deltaTime)
        {
            SetPosition(GetPosition() + Direction * deltaTime * speed);// sets the position in the direction towards wherever the mouse was when the bubble object was created
        }

        private void Rotation()
        {
            Direction = GameInput.GetMousePosition() - GetPosition();

            float Angle = (float)Math.Atan2(Direction.Y, Direction.X);// coverts the direction vector between the mouse and the bubbles position to an angle

            accuracy /= 1000;// used to convert a higher integr to a lower value float since randoms only work in integers

            Angle += accuracy;

            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));// converts the angle back to a direction to move in

            Angle = MathHelper.ToDegrees(Angle);
            GetSprite().SetRotation(Angle);
        }

        private void AtWorldEdge()
        {
            if (GetX() < 0)
            {
                GetScreen().RemoveObject(this);
            }
            if (GetX() > (int)Settings.GameResolution.X)
            {
                GetScreen().RemoveObject(this);
            }
            if (GetY() < 0)
            {
                GetScreen().RemoveObject(this);
            }
            if (GetY() > (int)Settings.GameResolution.Y)
            {
                GetScreen().RemoveObject(this);
            }
        }
    }
}
