using Microsoft.Xna.Framework;
using MonoGameEngine;
using MonoGameEngine.StandardCore;
using StudentProject.Code.GameObjects.MenuObjects;
using StudentProject.Code.GameObjects.Misc;

namespace StudentProject.Code.Screens
{
    public class StartMenu : Screen
    {
        MyGame _core;
        public override void Start(Core core)
        {
            base.Start(core);
            _core = (MyGame)core;
            SpawnObjects();

            SetBackground("test_bg", BackgroundType.HorizontalScroll);
            AudioManager.Instance.PlayBGM("MenuTheme", true, true);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            GetBackground().Move(new Vector2(2f, 0));
        }

        public void SpawnObjects()
        {
            AddObject(new Title(), (int)Settings.GameResolution.X / 2, (int)Settings.GameResolution.Y / 8 * 1);

            AddObject(new Crosshair(), 0, 0);

            AddObject(new StartButton(_core.blob), (int)Settings.GameResolution.X / 2, (int)Settings.GameResolution.Y / 8 * 3);
            AddObject(new ControlsButton(), (int)Settings.GameResolution.X / 2, (int)Settings.GameResolution.Y / 8 * 4);
            AddObject(new ExitButton(), (int)Settings.GameResolution.X / 2, (int)Settings.GameResolution.Y / 8 * 5);
        }
    }
}
