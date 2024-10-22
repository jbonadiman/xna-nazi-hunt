using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class Ground : GameObject
{
    private readonly Texture2D sprite;

    public Ground(Game g, int l, int t, int w, int h)
    {
        rect = new Rectangle(l, t, w, h);
        sprite = g.Content.Load<Texture2D>("images/ground.png");
    }

    public void Draw(SpriteBatch screen)
    {
        screen.Draw(sprite, rect, Color.White);
    }
}
