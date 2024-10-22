using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class Tent : GameObject
{
    Texture2D sprite;

    public Tent(Game g, int l, int t, int w, int h)
    {
        rect = new Rectangle(l, t - 10, 615 / 2, 289 / 2);
        sprite = g.Content.Load<Texture2D>("images/tent.png");
    }

    public void Draw(SpriteBatch screen)
    {
        screen.Draw(sprite, rect, Color.White);
    }
}
