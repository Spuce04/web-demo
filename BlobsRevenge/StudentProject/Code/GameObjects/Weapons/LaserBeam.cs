using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using System;

namespace StudentProject.Code.GameObjects.Weapons
{
    internal class LaserBeam : GameObject
    {
        public Vector2 Direction { get; set; }
        float Angle;
        int offset;

        Blob _blob;


        // sets the sprites image, anchor point, and scale. Also calcualtes the direction between its original position and the mouse position
        public LaserBeam(Blob blob, int Offset)
        {
            _blob = blob;
            offset = Offset;

            SetSprite("Laser");
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetLayerDepth(6);
            SetPosition(_blob.GetPosition());
        }
        public override void Update(float deltaTime)
        {
            Rotation();
            MoveTowardsMouse();
        }

        private void MoveTowardsMouse()
        {
            SetPosition(_blob.GetPosition() + Direction * offset);// sets the position in the direction towards wherever the mouse was when the bubble object was created
        }

        public void Rotation()
        {
            Direction = GameInput.GetMousePosition() - _blob.GetPosition();

            Angle = (float)Math.Atan2(Direction.Y, Direction.X);// coverts the direction vector between the mouse and the bubbles position to an angle

            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));// converts the angle back to a direction to move in

            Angle = MathHelper.ToDegrees(Angle);
            GetSprite().SetRotation(Angle);
        }
    }
}
