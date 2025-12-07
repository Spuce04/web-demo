using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Misc;
using StudentProject.Code.GameObjects.Weapons;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class MagazineSizeButton : Button
    {
        Blob _blob;
        Weapon _weapon;
        int[] Cost = new int[5];

        Text CostDisplay;
        CoinSprite coinSprite;

        public MagazineSizeButton(Blob blob, Weapon weapon, int FirstCost, int SecondCost, int ThirdCost, int FourthCost)
        {
            SetSprite("MagSizeButton", 396, 72, 0f, new int[] { 1, 1, 1, 1, 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            _blob = blob;
            _weapon = weapon;

            Cost[0] = FirstCost;
            Cost[1] = SecondCost;
            Cost[2] = ThirdCost;
            Cost[3] = FourthCost;
        }

        public override void Update(float deltaTime)
        {
            AddCostDisplay();
            CheckIfClicked();
            Upgrade();
        }

        public void Upgrade()
        {
            if (_weapon.MagazineSizeLevel[0] == _weapon.MagazineSize)
            {
                CostDisplay.SetMessage("" + Cost[0]);
                GetAnimatedSprite().StartAnimation(0);
                if (_blob.Coins >= Cost[0] && Clicked == true)
                {
                    _weapon.MagazineSize = _weapon.MagazineSizeLevel[1];
                    _blob.Coins -= Cost[0];
                    Clicked = false;
                }
            }
            if (_weapon.MagazineSizeLevel[1] == _weapon.MagazineSize)
            {
                CostDisplay.SetMessage("" + Cost[1]);
                GetAnimatedSprite().StartAnimation(1);
                if (_blob.Coins >= Cost[1] && Clicked == true)
                {
                    _weapon.MagazineSize = _weapon.MagazineSizeLevel[2];
                    _blob.Coins -= Cost[1];
                    Clicked = false;
                }
            }
            if (_weapon.MagazineSizeLevel[2] == _weapon.MagazineSize)
            {
                CostDisplay.SetMessage("" + Cost[2]);
                GetAnimatedSprite().StartAnimation(2);
                if (_blob.Coins >= Cost[2] && Clicked == true)
                {
                    _weapon.MagazineSize = _weapon.MagazineSizeLevel[3];
                    _blob.Coins -= Cost[2];
                    Clicked = false;
                }
            }
            if (_weapon.MagazineSizeLevel[3] == _weapon.MagazineSize)
            {
                CostDisplay.SetMessage("" + Cost[3]);
                GetAnimatedSprite().StartAnimation(3);
                if (_blob.Coins >= Cost[3] && Clicked == true)
                {
                    _weapon.MagazineSize = _weapon.MagazineSizeLevel[4];
                    _blob.Coins -= Cost[3];
                    Clicked = false;

                    RemoveCostDisplay();
                }
            }
            if (_weapon.MagazineSizeLevel[4] == _weapon.MagazineSize)
            {
                GetAnimatedSprite().StartAnimation(4);
            }
        }
        public void RemoveCostDisplay()
        {
            GetScreen().RemoveText(CostDisplay);
            GetScreen().RemoveObject(coinSprite);
        }

        private void AddCostDisplay()
        {
            if (coinSprite == null && CostDisplay == null)
            {
                if (_weapon.MagazineSizeLevel[4] != _weapon.MagazineSize)
                {
                    CostDisplay = new Text("", Color.White);
                    coinSprite = new CoinSprite();

                    GetScreen().AddObject(coinSprite, (int)GetX() - 324, (int)GetY());
                    GetScreen().AddText(CostDisplay, (int)GetX() - 300, (int)GetY() - 12);
                }
            }
        }
    }
}