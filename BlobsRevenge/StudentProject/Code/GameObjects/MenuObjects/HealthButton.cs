using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Misc;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class HealthButton : Button
    {
        Blob _blob;
        Text CostDisplay;
        CoinSprite coinSprite;
        public HealthButton(Blob blob)
        {
            SetSprite("BlobStatsButton", 78, 0f, new int[] { 1 });
            GetSprite().SetOrigin(0.5f, 0.5f);
            _blob = blob;
        }
        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            AddCostDisplay();
            HealthUpgrade();
            GetAnimatedSprite().SetFrameNumber(0);
        }

        public void HealthUpgrade()
        {
            if (_blob.MaxLives == 1)
            {
                CostDisplay.SetMessage("" + 5);
                if (Clicked == true && _blob.Coins >= 5)
                {
                    _blob.MaxLives++;
                    _blob.Coins -= 5;
                    Clicked = false;
                }
            }
            if (_blob.MaxLives == 2)
            {
                CostDisplay.SetMessage("" + 20);
                if (Clicked == true && _blob.Coins >= 20)
                {
                    _blob.MaxLives++;
                    _blob.Coins -= 20;
                    Clicked = false;
                }
            }
            if (_blob.MaxLives == 3)
            {
                CostDisplay.SetMessage("" + 50);
                if (Clicked == true && _blob.Coins >= 50)
                {
                    _blob.MaxLives++;
                    _blob.Coins -= 50;
                    Clicked = false;
                }
            }
            if (_blob.MaxLives == 4)
            {
                CostDisplay.SetMessage("" + 150);
                if (Clicked == true && _blob.Coins >= 150)
                {
                    _blob.MaxLives++;
                    _blob.Coins -= 150;
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
                if(_blob.MaxLives != 5)
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
