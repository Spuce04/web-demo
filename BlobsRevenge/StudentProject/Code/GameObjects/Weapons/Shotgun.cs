using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class Shotgun : Weapon
    {
        Blob _blob;
        public Shotgun(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(2);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(1f, 0.8f, 0.6f, 0.4f, 0.2f);
            AccuracyLevels(300, 250, 200, 150, 100);
            MaxMagazineSizeLevels(3, 5, 7, 9, 12);

            InitialStats(FireRateLevel[0], 10f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            HandleInput(deltaTime, 8, _blob);
            DisplayMagCount();
        }
    }
}