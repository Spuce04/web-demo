using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.BlobStuff;
using StudentProject.Code.GameObjects.Consumables;
using StudentProject.Code.GameObjects.Enemies;
using StudentProject.Code.GameObjects.Misc;
using System;

namespace StudentProject.Code.Screens
{
    public class Level : Screen
    {
        public Blob blob { get; set; }
        private MyGame _core;
        private Crosshair crosshair = new Crosshair();

        private LivesDisplay livesDisplay = new LivesDisplay();
        private CoinSprite coinDisplay = new CoinSprite();

        private Text Coins = new Text("", Color.White);

        private int JellyfishSpawnChance;
        private int SharkSpawnChance;
        private int CrabSpawnChance;
        private int CoralSpawnChance = 50;
        private int ClamSpawnChance = 750;
        private int BubbleSpawnChance = 200;

        public int JellyfishAmount { get; set; }
        public int SharkAmount { get; set; }
        public int CrabAmount { get; set; }
        public int EnemyCount { get; set; }
        private int EnemyLimit;

        public int JellyfishKillCount { get; set; }
        public int SharkKillCount { get; set; }
        public int CrabKillCount { get; set; }

        public int DifficultyScaler { get; set; }// blob progress squared for exponential growth on some variables that affect the difficulty

        float EndRoundTimer = 0;

        public override void Start(Core core)
        {
            base.Start(core);
            _core = (MyGame)core;
            SpawnBlob();

            SetBackground("test_bg", BackgroundType.HorizontalScroll);
            AudioManager.Instance.PlayBGM("OceanCommotion", true, true);
            SpawnUI();
            SpawnInitialCoral();

            DifficultyScaler = blob.ProgressCount * blob.ProgressCount;

            JellyfishAmount = DifficultyScaler * 2;
            SharkAmount = DifficultyScaler;
            CrabAmount = blob.ProgressCount;

            EnemyLimit = DifficultyScaler;

            JellyfishKillCount = 0;
            SharkKillCount = 0;
            CrabKillCount = 0;
            EnemyCount = 0;

            AddObject(new ProgressDisplay(this), (int)Settings.GameResolution.X / 2, 50);

            SetSpawnChance(500 / DifficultyScaler, 625 / DifficultyScaler, 750 / DifficultyScaler);

            Transition.Instance.EndTransition();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (blob == null)
            {
                SpawnBlob();
            }
            GetBackground().Move(new Vector2(2f, 0));
            SpawnEnemies(deltaTime);
            SpawnCoral();
            SpawnClam();
            SpawnBubble();
            livesDisplay.SetLives(blob.Lives);
            Coins.SetMessage("x" + blob.Coins);
        }

        public override void End()
        {

        }

        public void SpawnBlob()
        {
            blob = _core.blob;
            blob.ResetStats();
            AddObject(blob, 720, 360);
            SpawnWeapon();
        }

        public void SpawnWeapon()
        {
            if (blob.CurrentWeapon == null)
            {
                blob.CurrentWeapon = blob.singleShot;
                AddObject(blob.singleShot, 30, 100);
            }
            else
            {
                AddObject(blob.CurrentWeapon, 30, 100);
            }
            blob.CurrentWeapon.ResetStats();
        }

        public void SpawnUI()
        {
            AddObject(crosshair, 720, 360);
            AddObject(livesDisplay, 8, 20);
            AddObject(coinDisplay, 20, 60);
            AddText(Coins, (int)coinDisplay.GetX() + 12, (int)coinDisplay.GetY() - 12);
        }

        public void SetSpawnChance(int jellyfish, int shark, int crab)
        {
            JellyfishSpawnChance = jellyfish;
            SharkSpawnChance = shark;
            CrabSpawnChance = crab;

            if(JellyfishSpawnChance < 2)
            {
                JellyfishSpawnChance = 2;
            }
            if (SharkSpawnChance < 2)
            {
                SharkSpawnChance = 2;
            }
            if (CrabSpawnChance < 2)
            {
                CrabSpawnChance = 2;
            }
        }

        public void SpawnEnemies(float deltaTime)
        {
            if (EnemyCount < EnemyLimit)
            {
                SpawnJellyfish(500, blob.ProgressCount * 2);
            }
            if (JellyfishKillCount >= JellyfishAmount && EnemyCount < EnemyLimit)
            {
                SpawnSharks(500 * blob.ProgressCount, 1);
            }
            if (SharkKillCount >= SharkAmount && EnemyCount < EnemyLimit)
            {
                for (int i = GetAllObjectsOfType<Crab>().Length; i < CrabAmount; i++)
                {
                    SpawnCrabs(600, 2);
                }
            }
            if (CrabKillCount >= CrabAmount)
            {
                if (blob.Lives > 0 || blob.Lives != 0)
                {
                    EndRound(deltaTime, 10f);
                }
            }
        }

        public void SpawnJellyfish(float MaxVelocity, int Health)
        {
            int Y = new Random().Next(0, 720);

            int spawn = new Random().Next(JellyfishSpawnChance);

            if (spawn == 1)
            {
                AddObject(new Jellyfish(this, blob, MaxVelocity, Health), (int)Settings.GameResolution.X + 50, Y);
            }
        }

        public void SpawnSharks(float MaxVelocity, int Health)
        {
            int Y = new Random().Next(0, 720);

            int spawn = new Random().Next(SharkSpawnChance);

            if (spawn == 1)
            {
                AddObject(new Shark(this, blob, MaxVelocity, Health), (int)Settings.GameResolution.X + 50, Y);
            }
        }

        public void SpawnCrabs(int ClawSpeed, int Health)
        {
            int X = new Random().Next(0, (int)Settings.GameResolution.X);

            int Crab = new Random().Next(CrabSpawnChance);

            if (Crab == 1)
            {
                Crab crab = new Crab(this, blob, ClawSpeed, Health);
                AddObject(crab, X, (int)Settings.GameResolution.Y + 50);
            }
        }

        public void SpawnInitialCoral()
        {
            for (int i = 0; i < 10; i++)
            {
                int Y = new Random().Next((int)Settings.GameResolution.Y - 200, (int)Settings.GameResolution.Y);
                int X = new Random().Next(0, (int)Settings.GameResolution.X);

                AddObject(new Corals(), X, Y);
            }
        }

        public void SpawnCoral()
        {
            int Y = new Random().Next((int)Settings.GameResolution.Y - 200, (int)Settings.GameResolution.Y);

            int Coral = new Random().Next(CoralSpawnChance);

            if (Coral == 1)
            {
                AddObject(new Corals(), (int)Settings.GameResolution.X + 50, Y);
            }
        }

        public void SpawnClam()
        {
            int Y = new Random().Next((int)Settings.GameResolution.Y - 200, (int)Settings.GameResolution.Y - 100);

            int Clam = new Random().Next(ClamSpawnChance);

            if (Clam == 1)
            {
                AddObject(new Clam(), (int)Settings.GameResolution.X + 50, Y);
            }
        }

        public void SpawnBubble()
        {
            int X = new Random().Next(0, (int)Settings.GameResolution.X);

            Random rand = new Random();
            int Bubble = rand.Next(BubbleSpawnChance);

            if (Bubble == 1)
            {
                AddObject(new ReloadBubble(), X, (int)Settings.GameResolution.Y + 50);
            }
        }

        private void EndRound(float deltaTime, float MaxTime)
        {
            foreach (Jellyfish jellyfish in GetAllObjectsOfType<Jellyfish>())
            {
                jellyfish.health = 0;
            }
            foreach (Shark shark in GetAllObjectsOfType<Shark>())
            {
                shark.health = 0;
            }
            foreach (Crab crab in GetAllObjectsOfType<Crab>())
            {
                crab.health = 0;
            }
            foreach (ProgressDisplay progressDisplay in GetAllObjectsOfType<ProgressDisplay>())
            {
                RemoveText(progressDisplay.Count);
                RemoveObject(progressDisplay);
            }

            JellyfishSpawnChance = 0;
            SharkSpawnChance = 0;
            CrabSpawnChance = 0;

            EndRoundTimer += deltaTime;

            if (EndRoundTimer >= MaxTime)
            {
                SharkKillCount = 0;
                JellyfishKillCount = 0;
                CrabKillCount = 0;

                blob.ProgressCount++;
                Transition.Instance.ToScreen<UpgradeMenu>();
                blob.Coins += blob.ProgressCount * 50;
                EndRoundTimer = 0;
            }
        }
    }
}