using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Weapons;
using StudentProject.Code.Screens;

namespace StudentProject.Code.GameObjects.MenuObjects
{
    internal class UnlockButton : Button
    {
        UpgradeMenu _upgradeMenu;
        Blob _blob;
        Weapon _weapon;
        int _cost;
        int _weaponSelectedPosition;


        Text CostDisplay;
        public UnlockButton(UpgradeMenu upgradeMenu, Blob blob, Weapon weapon, int cost, int weaponSelectedPosition)
        {
            SetSprite("Padlock", 108, 0, new int[] { 2 });
            GetAnimatedSprite().SetFrameNumber(1);
            GetSprite().SetOrigin(0.5f, 0.5f);
            _upgradeMenu = upgradeMenu;
            _blob = blob;
            _weapon = weapon;
            _cost = cost;
            _weaponSelectedPosition = weaponSelectedPosition;
        }

        public override void Update(float deltaTime)
        {
            CheckIfClicked();
            AddCostDisplay();

            if (Clicked == true && _weapon.unlocked == false && _blob.Coins >= _cost)
            {
                _weapon.unlocked = true;
                _blob.Coins -= _cost;
                _upgradeMenu.WeaponSelected[_weaponSelectedPosition] = true;
                GetScreen().RemoveObject(this);
                RemoveCostDisplay();
            }

            if (Active != null)
            {
                GetAnimatedSprite().SetFrameNumber(0);
            }
            else if (Active == null)
            {
                GetAnimatedSprite().SetFrameNumber(1);
            }
        }

        public void RemoveCostDisplay()
        {
            GetScreen().RemoveText(CostDisplay);
        }

        private void AddCostDisplay()
        {
            if (CostDisplay == null)
            {
                CostDisplay = new Text("" + _cost, Color.White);
                GetScreen().AddText(CostDisplay, (int)GetX() - 144, (int)GetY() + 10);
            }
        }
    }
}
