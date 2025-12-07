using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class Weapon : GameObject
    {
        public bool unlocked { get; set; } = false;

        public float ShootingTimer;
        public float FireRate;// how frequently bubbles can be shot/ created (lower number = faster)
        public float RecoilStrength;// how much the blobs velocity decreases by when they shoot
        public int Accuracy;
        public int BulletSpeed;

        public int MagazineSize;
        public int MagazineCount;

        // arraya that hold the different levels of statistics you can upgrade the weapons to
        public float[] FireRateLevel = new float[5];
        public int[] AccuracyLevel = new int[5];
        public int[] MagazineSizeLevel = new int[5];

        public Text MagCount;

        public bool IsHit;

        public Weapon()
        {

        }
        public override void Update(float deltaTime)
        {

        }
        public void HandleInput(float deltaTime, int BulletCount, Blob blob)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && ShootingTimer >= FireRate && IsHit == false && MagazineCount > 0)
            {
                for (int i = 0; i < BulletCount; i++)
                {
                    Bubble bubble = new Bubble(blob.GetPosition(), Accuracy, BulletSpeed);
                    GetScreen().AddObject(bubble, (int)blob.GetX(), (int)blob.GetY());
                }
                GetScreen().GetOneObjectOfType<Blob>().Recoil(RecoilStrength);
                MagazineCount--;
                ShootingTimer = 0;
            }
            if (ShootingTimer < FireRate)
            {
                ShootingTimer += deltaTime;
            }
        }

        public void SniperHandleInput(float deltaTime, int BulletCount, Blob blob)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && ShootingTimer >= FireRate && IsHit == false && MagazineCount > 0)
            {
                for (int i = 0; i < BulletCount; i++)
                {
                    SniperBubble sniperBubble = new SniperBubble(blob.GetPosition(), Accuracy, BulletSpeed);
                    GetScreen().AddObject(sniperBubble, (int)blob.GetX(), (int)blob.GetY());
                }
                GetScreen().GetOneObjectOfType<Blob>().Recoil(RecoilStrength);
                MagazineCount--;
                ShootingTimer = 0;
            }
            if (ShootingTimer < FireRate)
            {
                ShootingTimer += deltaTime;
            }
        }


        public void Reload()
        {
            MagazineCount = MagazineSize;
        }

        public void InitialStats(float fireRate, float recoilStrength, int accuracy, int maxMagazineSize, int bulletSpeed)// used only once when the object is first created, used to set the basic stats to a balanced state for each weapon
        {
            FireRate = fireRate;
            RecoilStrength = recoilStrength;
            Accuracy = accuracy;
            MagazineSize = maxMagazineSize;
            BulletSpeed = bulletSpeed;
        }

        public void ResetStats()// fully loads the gun, makes it inactive for blob to correctly spawn it in, sets shooting timer to the fire rate so you don't need to wait on the first bullet. Used everytime the game world starts
        {
            MagazineCount = MagazineSize;
            ShootingTimer = FireRate;

            if (MagCount != null)
            {
                MagCount = null;
            }
        }

        public void FireRateLevels(float lvl1, float lvl2, float lvl3, float lvl4, float lvl5)
        {
            FireRateLevel[0] = lvl1;
            FireRateLevel[1] = lvl2;
            FireRateLevel[2] = lvl3;
            FireRateLevel[3] = lvl4;
            FireRateLevel[4] = lvl5;
        }

        public void AccuracyLevels(int lvl1, int lvl2, int lvl3, int lvl4, int lvl5)
        {
            AccuracyLevel[0] = lvl1;
            AccuracyLevel[1] = lvl2;
            AccuracyLevel[2] = lvl3;
            AccuracyLevel[3] = lvl4;
            AccuracyLevel[4] = lvl5;
        }

        public void MaxMagazineSizeLevels(int lvl1, int lvl2, int lvl3, int lvl4, int lvl5)
        {
            MagazineSizeLevel[0] = lvl1;
            MagazineSizeLevel[1] = lvl2;
            MagazineSizeLevel[2] = lvl3;
            MagazineSizeLevel[3] = lvl4;
            MagazineSizeLevel[4] = lvl5;
        }

        public void DisplayMagCount()
        {
            if (MagCount == null)
            {
                MagCount = new Text(" " + MagazineCount, Color.White);
                GetScreen().AddText(MagCount, (int)GetX() + 20, (int)GetY() - 12);
            }
            else if (MagCount != null)
            {
                MagCount.SetMessage(" " + MagazineCount);
            }
        }
    }
}
