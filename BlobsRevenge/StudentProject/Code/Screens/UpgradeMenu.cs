using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.MenuObjects;
using StudentProject.Code.GameObjects.Misc;
using System;
using System.Diagnostics;

namespace StudentProject.Code.Screens
{
    class UpgradeMenu : Screen
    {
        MyGame _core;
        Blob _blob;
        Crosshair crosshair = new Crosshair();

        bool FirstFrame = false;

        Button[] WeaponSelector = { new Button(), new Button(), new Button(), new Button(), new Button(), new Button() };

        BlobDisplay blobDisplay;

        public bool[] WeaponSelected { get; set; } = { false, false, false, false, false, false };
        public override void Start(Core core)
        {
            base.Start(core);
            _core = (MyGame)core;
            _blob = _core.blob;
            AddObject(crosshair, 0, 0);
            GameButtons();
            BlobUpgrades();

            SetBackground("UpgradeMenuBG");
            AudioManager.Instance.PlayBGM("MenuTheme", true, true);

            Transition.Instance.EndTransition();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            SingleShotUpgrades();
            TwoShotUpgrades();
            ShotgunUpgrades();
            MinigunUpgrades();
            SniperUpgrades();
            LaserUpgrades();
            WeaponSelectButtons();
        }

        public override void End()
        {
            base.End();
            _blob.ResetStats();
            _core.blob = _blob;
        }

        public void GameButtons()
        {
            AddObject(new Title(), (int)Settings.GameResolution.X / 6, 180 * 1);

            AddObject(new StartButton(_blob), (int)Settings.GameResolution.X / 6, 180 * 2);
            AddObject(new ControlsButton(), (int)Settings.GameResolution.X / 6, 180 * 3);
            AddObject(new ExitButton(), (int)Settings.GameResolution.X / 6, 180 * 4);
        }

        public void BlobUpgrades()
        {
            blobDisplay = new BlobDisplay(_blob);
            Text Coins = new Text("", Color.White);
            Text Lives = new Text("", Color.White);
            
            AddObject(blobDisplay, (int)Settings.GameResolution.X / 2 - _blob.GetSprite().GetWidth() / 2, (int)Settings.GameResolution.Y / 3 - _blob.GetSprite().GetHeight() / 2);
            AddText(Coins, (int)blobDisplay.GetX() + 12, (int)blobDisplay.GetY() - 12);
            AddText(Lives, (int)blobDisplay.GetX() + 12, (int)blobDisplay.GetY() - 12);

            AddObject(new HealthButton(_blob), (int)Settings.GameResolution.X / 2 - 200, 180 * 3);
            AddObject(new SpeedButton(_blob), (int)Settings.GameResolution.X / 2 - 200, 180 * 3 + 120);
        }

        private void WeaponSelectButtons()
        {
            if (FirstFrame == false)
            {
                int i = 0;

                for (i = 0; i < WeaponSelector.Length; i++)
                {
                    WeaponSelector[i].SetSprite("BlobWeapons", 54, 51, 0f, new int[] { 1, 1, 1, 1, 1, 1 });
                    WeaponSelector[i].GetAnimatedSprite().StartAnimation(i);
                    AddObject(WeaponSelector[i], (int)Settings.GameResolution.X / 6 * 4 + (i * 96), 180);
                }
                FirstFrame = true;
            }

            foreach (Button button in WeaponSelector)
            {
                if (button.Clicked == true)
                {
                    WeaponSelected[Array.IndexOf(WeaponSelector, button)] = true;
                }
            }
        }

        private void SingleShotUpgrades()
        {
            if (WeaponSelected[0] == true)
            {
                RemoveUpgradeButtons();

                AddObject(new FireRateButton(_blob, _blob.singleShot, 1, 3, 5, 7), (int)Settings.GameResolution.X / 6 * 5, 180 * 2);
                AddObject(new AccuracyButton(_blob, _blob.singleShot, 1, 2, 3, 5), (int)Settings.GameResolution.X / 6 * 5, 180 * 3);
                AddObject(new MagazineSizeButton(_blob, _blob.singleShot, 3, 6, 9, 12), (int)Settings.GameResolution.X / 6 * 5, 180 * 4);

                _blob.CurrentWeapon = _blob.singleShot;
                blobDisplay.GetAnimatedSprite().StartAnimation(0);
                _blob.SpriteNumber = 0;
                WeaponSelected[0] = false;
            }
        }

        private void TwoShotUpgrades()// NOTE - fix the colours not staying white when clicked
        {

            if (WeaponSelected[1] == true)
            {
                RemoveUpgradeButtons();

                if (_blob.twoShot.unlocked == false)
                {
                    UnlockButton unlockButton = new UnlockButton(this, _blob, _blob.twoShot, 60, 1);

                    if (unlockButton != null)
                    {
                        AddObject(unlockButton, (int)Settings.GameResolution.X / 6 * 5, (int)Settings.GameResolution.Y / 2);
                    }
                }

                if (_blob.twoShot.unlocked == true)
                {
                    FireRateButton fireRateButton = new FireRateButton(_blob, _blob.twoShot, 10, 20, 30, 40);
                    AccuracyButton accuracyButton = new AccuracyButton(_blob, _blob.twoShot, 22, 34, 36, 45);
                    MagazineSizeButton magazineSizeButton = new MagazineSizeButton(_blob, _blob.twoShot, 25, 35, 50, 65);

                    if (fireRateButton != null && accuracyButton != null && magazineSizeButton != null)
                    {
                        AddObject(fireRateButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 2);
                        AddObject(accuracyButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 3);
                        AddObject(magazineSizeButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 4);
                    }

                    _blob.CurrentWeapon = _blob.twoShot;
                    blobDisplay.GetAnimatedSprite().StartAnimation(1);
                    _blob.SpriteNumber = 1;
                    WeaponSelected[1] = false;
                }
            }
        }

        private void ShotgunUpgrades()
        {
            if (WeaponSelected[2] == true)
            {
                RemoveUpgradeButtons();

                if ( _blob.shotgun.unlocked == false)
                {
                    RemoveUpgradeButtons();

                    UnlockButton unlockButton = new UnlockButton(this, _blob, _blob.shotgun, 300, 2);

                    if (unlockButton != null)
                    {
                        AddObject(unlockButton, (int)Settings.GameResolution.X / 6 * 5, (int)Settings.GameResolution.Y / 2);
                    }
                }

                if (_blob.shotgun.unlocked == true)
                {
                    RemoveUpgradeButtons();

                    FireRateButton fireRateButton = new FireRateButton(_blob, _blob.shotgun, 30, 45, 60, 75);
                    AccuracyButton accuracyButton = new AccuracyButton(_blob, _blob.shotgun, 10, 20, 30, 40);
                    MagazineSizeButton magazineSizeButton = new MagazineSizeButton(_blob, _blob.shotgun, 35, 55, 75, 90);

                    if (fireRateButton != null && accuracyButton != null && magazineSizeButton != null)
                    {
                        AddObject(fireRateButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 2);
                        AddObject(accuracyButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 3);
                        AddObject(magazineSizeButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 4);
                    }

                    _blob.CurrentWeapon = _blob.shotgun;
                    blobDisplay.GetAnimatedSprite().StartAnimation(3);
                    _blob.SpriteNumber = 3;
                }
            }
        }

        private void MinigunUpgrades()
        {
            if (WeaponSelected[3] == true)
            {
                if (_blob.minigun.unlocked == false)
                {
                    RemoveUpgradeButtons();

                    UnlockButton unlockButton = new UnlockButton(this, _blob, _blob.minigun, 700, 3);

                    if (unlockButton != null)
                    {
                        AddObject(unlockButton, (int)Settings.GameResolution.X / 6 * 5, (int)Settings.GameResolution.Y / 2);
                    }
                }

                if (_blob.minigun.unlocked == true)
                {
                    RemoveUpgradeButtons();

                    FireRateButton fireRateButton = new FireRateButton(_blob, _blob.minigun, 60, 70, 85, 100);
                    AccuracyButton accuracyButton = new AccuracyButton(_blob, _blob.minigun, 50, 55, 60, 65);
                    MagazineSizeButton magazineSizeButton = new MagazineSizeButton(_blob, _blob.minigun, 75, 90, 115, 130);

                    if (fireRateButton != null)
                    {
                        AddObject(fireRateButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 2);
                        AddObject(accuracyButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 3);
                        AddObject(magazineSizeButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 4);
                    }

                    _blob.CurrentWeapon = _blob.minigun;
                    blobDisplay.GetAnimatedSprite().StartAnimation(2);
                    _blob.SpriteNumber = 2;
                }
            }
        }

        private void SniperUpgrades()
        {
            if (WeaponSelected[4] == true)
            {
                if (_blob.sniper.unlocked == false)
                {
                    RemoveUpgradeButtons();

                    UnlockButton unlockButton = new UnlockButton(this, _blob, _blob.sniper, 2000, 4);

                    if (unlockButton != null)
                    {
                        AddObject(unlockButton, (int)Settings.GameResolution.X / 6 * 5, (int)Settings.GameResolution.Y / 2);
                    }
                }

                if (_blob.sniper.unlocked == true)
                {
                    RemoveUpgradeButtons();

                    FireRateButton fireRateButton = new FireRateButton(_blob, _blob.sniper, 80, 90, 110, 120);
                    AccuracyButton accuracyButton = new AccuracyButton(_blob, _blob.sniper, 60, 70, 80, 90);
                    MagazineSizeButton magazineSizeButton = new MagazineSizeButton(_blob, _blob.sniper, 100, 120, 150, 180);

                    if (fireRateButton != null)
                    {
                        AddObject(fireRateButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 2);
                        AddObject(accuracyButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 3);
                        AddObject(magazineSizeButton, (int)Settings.GameResolution.X / 6 * 5, 180 * 4);
                    }

                    _blob.CurrentWeapon = _blob.sniper;
                    blobDisplay.GetAnimatedSprite().StartAnimation(4);
                    _blob.SpriteNumber = 4;
                }
            }
        }

        private void LaserUpgrades()
        {
            if (WeaponSelected[5] == true)
            {
                if (_blob.laser.unlocked == false)
                {
                    RemoveUpgradeButtons();

                    UnlockButton unlockButton = new UnlockButton(this, _blob, _blob.laser, 9999, 5);

                    if (unlockButton != null)
                    {
                        AddObject(unlockButton, (int)Settings.GameResolution.X / 6 * 5, (int)Settings.GameResolution.Y / 2);
                    }
                }

                if (_blob.laser.unlocked == true)
                {
                    RemoveUpgradeButtons();

                    _blob.CurrentWeapon = _blob.laser;
                    blobDisplay.GetAnimatedSprite().StartAnimation(5);
                    _blob.SpriteNumber = 5;
                }
            }
        }

        private void RemoveUpgradeButtons()
        {
            foreach (FireRateButton button in GetAllObjectsOfType<FireRateButton>())
            {
                button.RemoveCostDisplay();
                RemoveObject(button);
            }

            foreach (AccuracyButton button in GetAllObjectsOfType<AccuracyButton>())
            {
                button.RemoveCostDisplay();
                RemoveObject(button);
            }

            foreach (MagazineSizeButton button in GetAllObjectsOfType<MagazineSizeButton>())
            {
                button.RemoveCostDisplay();
                RemoveObject(button);
            }

            foreach (UnlockButton button in GetAllObjectsOfType<UnlockButton>())
            {
                button.RemoveCostDisplay();
                RemoveObject(button);
            }

            for (int i = 0; i < WeaponSelected.Length; i++)
            {
                WeaponSelector[i].GetSprite().SetColour(Color.Gray);
                WeaponSelected[i] = false;
            }
        }
    }
}