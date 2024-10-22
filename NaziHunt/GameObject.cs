using Microsoft.Xna.Framework;

namespace NaziHunt;

public class GameObject
{
    public Rectangle rect;

    public void MoveOnX(int l)
    {
        rect.X += l;
    }
}
