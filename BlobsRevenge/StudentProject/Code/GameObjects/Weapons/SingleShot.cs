using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class SingleShot : Weapon
    {
        Blob _blob;
        public SingleShot(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(0);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(1f, 0.9f, 0.8f, 0.7f, 0.6f);
            AccuracyLevels(300, 250, 200, 150, 100);
            MaxMagazineSizeLevels(2, 3, 4, 5, 6);

            InitialStats(FireRateLevel[0], 10f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            HandleInput(deltaTime, 1, _blob);
            DisplayMagCount();
        }
    }
}