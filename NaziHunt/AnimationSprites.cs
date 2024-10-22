using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class AnimationSprites
{
    private readonly IList<Texture2D> frames;
    private int elapsedTime;
    private int time;
    private int index;

    public AnimationSprites()
    {
        frames = [];
    }

    public void Add(Texture2D item)
    {
        frames.Add(item);
    }

    public void StartAnimation(int t)
    {
        time = t;
        elapsedTime = index = 0;
    }

    public Texture2D GetImage(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            index++;
            if (index == frames.Count)
            {
                index = 0;
            }
        }

        return frames[index];
    }
}
