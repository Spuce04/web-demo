using Microsoft.Xna.Framework;
using MonoGameEngine.StandardCore;
using StudentProject.Code.Screens;

namespace StudentProject.Code.GameObjects.Misc
{
    internal class ProgressDisplay : GameObject
    {
        Level _level;
        public Text Count { get; set; }
        public ProgressDisplay(Level level)
        {
            _level = level;
            SetSprite("Jellyfish", 96, 0, new int[] { 5, 1 });
        }

        public override void Update(float deltaTime)
        {
            if (Count == null)
            {
                Count = new Text("", Color.White);
                GetScreen().AddText(Count, (int)GetX(), (int)GetY() + 40);
            }
            if (_level.JellyfishKillCount < _level.JellyfishAmount)
            {
                SetSprite("Jellyfish", 96, 0, new int[] { 5, 1 });
                GetSprite().SetOrigin(0.5f, 0.5f);
                GetAnimatedSprite().StartAnimation(1);
                Count.SetMessage("" + (_level.JellyfishAmount - _level.JellyfishKillCount));
            }
            else if (_level.JellyfishKillCount >= _level.JellyfishAmount && _level.SharkKillCount < _level.SharkAmount)
            {
                SetSprite("Shark", 192, 90, 0, new int[] { 4, 1 });
                GetSprite().SetOrigin(0.5f, 0.5f);
                GetAnimatedSprite().StartAnimation(1);
                Count.SetMessage("" + (_level.SharkAmount - _level.SharkKillCount));
            }
            else if (_level.SharkKillCount >= _level.SharkAmount && _level.CrabKillCount < _level.CrabAmount)
            {
                SetSprite("Crab", 87, 96, 0, new int[] { 4, 1 });
                GetSprite().SetOrigin(0.5f, 0.5f);
                GetAnimatedSprite().StartAnimation(1);
                Count.SetMessage("" + (_level.CrabAmount - _level.CrabKillCount));
            }
        }
    }
}