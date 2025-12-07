using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Misc;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class SpeedButton : Button
    {
        Blob _blob;
        Text CostDisplay;
        CoinSprite coinSprite;

        public SpeedButton(Blob blob)
        {
            SetSprite("BlobStatsButton", 78, 0f, new int[] { 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            _blob = blob;
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            AddCostDisplay();
            SpeedUpgrade();
            GetAnimatedSprite().SetFrameNumber(1);
        }

        public void SpeedUpgrade()
        {
            if (_blob.MaxSpeed == 5)
            {
                CostDisplay.SetMessage("" + 5);
                if (Clicked == true && _blob.Coins >= 5)
                {
                    _blob.MaxSpeed = 6;
                    _blob.Acceleration = _blob.MaxSpeed / 5;
                    _blob.Coins -= 5;
                    Clicked = false;
                }
            }
            if (_blob.MaxSpeed == 6)
            {
                CostDisplay.SetMessage("" + 20);
                if (Clicked == true && _blob.Coins >= 20)
                {
                    _blob.MaxSpeed = 8;
                    _blob.Acceleration = _blob.MaxSpeed / 5;
                    _blob.Coins -= 20;
                    Clicked = false;
                }
            }
            if (_blob.MaxSpeed == 8)
            {
                CostDisplay.SetMessage("" + 50);
                if (Clicked == true && _blob.Coins >= 50)
                {
                    _blob.MaxSpeed = 10;
                    _blob.Acceleration = _blob.MaxSpeed / 5;
                    _blob.Coins -= 50;
                    Clicked = false;
                    RemoveCostDisplay();
                }
            }
        }

        public void RemoveCostDisplay()
        {
            GetScreen().RemoveText(CostDisplay);
            GetScreen().RemoveObject(coinSprite);
        }

        private void AddCostDisplay()
        {
            if (CostDisplay == null && coinSprite == null)
            {
                if (_blob.MaxSpeed != 15)
                {
                    CostDisplay = new Text("", Color.White);
                    coinSprite = new CoinSprite();

                    GetScreen().AddText(CostDisplay, (int)GetX() + 64, (int)GetY() - 12);
                    GetScreen().AddObject(coinSprite, (int)GetX() + 48, (int)GetY());
                }
            }
        }
    }
}