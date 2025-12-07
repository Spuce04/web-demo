using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class TwoShot : Weapon
    {
        Blob _blob;
        public TwoShot(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(1);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(1f, 0.8f, 0.6f, 0.5f, 0.4f);
            AccuracyLevels(300, 250, 220, 180, 140);
            MaxMagazineSizeLevels(4, 6, 8, 10, 15);

            InitialStats(FireRateLevel[0], 10f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            HandleInput(deltaTime, 2, _blob);
            DisplayMagCount();
        }
    }
}
