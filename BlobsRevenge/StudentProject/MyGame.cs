using Microsoft.Xna.Framework;
using MonoGameEngine;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;

namespace StudentProject
{
    public class MyGame : Core
    {
        public Blob blob;
        protected override void Initialize()
        {
            Window.Title = "Blobs Revenge";
            Settings.ScreenDimensions = new Vector2(1920, 1080);
            // TODO: Add your game's initialization logic below here

            StartScreen<StartMenu>();
            base.Initialize();
            BlobStart();
        }

        public void BlobStart()
        {
            blob = new Blob();
            blob.singleShot = new SingleShot(blob);
            blob.twoShot = new TwoShot(blob);
            blob.shotgun = new Shotgun(blob);
            blob.minigun = new Minigun(blob);
            blob.sniper = new Sniper(blob);
            blob.laser = new Laser(blob);
        }
    }
}