using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameEngine.StandardCore;
public class LivesDisplay : GameObject
{
    private int Lives;
    public LivesDisplay()
    {
        SetSprite("Heart");
        GetSprite().SetOrigin(0.5f, 0.5f);
        GetSprite().SetInWorldSpace(false);
    }

    public override void Update(float deltaTime)
    {

    }

    public override void Render(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Lives; i++)
        {
            spriteBatch.Draw(GetSprite().GetTexture(), new Vector2(GetX() + (i * GetSprite().GetWidth()), GetY()), Color.White);
        }
    }

    public void SetLives(int lives)
    {
        Lives = lives;
    }
}
