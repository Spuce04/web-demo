using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class Minigun : Weapon
    {
        Blob _blob;
        public Minigun(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(3);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(0.05f, 0.01f, 0.005f, 0.001f, 0.0005f);
            AccuracyLevels(300, 270, 250, 220, 200);
            MaxMagazineSizeLevels(100, 180, 250, 300, 400);

            InitialStats(FireRateLevel[0], 0.5f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            HandleInput(deltaTime, 1, _blob);
            DisplayMagCount();
        }
    }
}