using StudentProject.Code.GameObjects.BlobStuff;

namespace StudentProject.Code.GameObjects.Weapons
{
    public class Sniper : Weapon
    {
        Blob _blob;
        public Sniper(Blob blob)
        {
            _blob = blob;
            SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
            GetAnimatedSprite().StartAnimation(4);
            GetSprite().SetOrigin(0.5f, 0.5f);
            GetSprite().SetInWorldSpace(false);

            FireRateLevels(1f, 0.9f, 0.7f, 0.5f, 0.4f);
            AccuracyLevels(100, 75, 50, 25, 1);
            MaxMagazineSizeLevels(6, 9, 14, 18, 25);

            InitialStats(FireRateLevel[0], 7f, AccuracyLevel[0], MagazineSizeLevel[0], 1000);
        }

        public override void Update(float deltaTime)
        {
            IsHit = _blob.IsHit;
            SniperHandleInput(deltaTime, 1, _blob);
            DisplayMagCount();
        }
    }
}
