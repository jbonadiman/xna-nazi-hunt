using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class AnimationSprites
{
    private List<Texture2D> aImagens;

    private int elapsedTime;

    private int time;

    private int index;

    public AnimationSprites()
    {
        aImagens = new List<Texture2D>();
    }

    public void Add(Texture2D item)
    {
        aImagens.Add(item);
    }

    public void StartAnimation(int t)
    {
        time = t;
        elapsedTime = index = 0;
    }

    public Texture2D getImage(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            index++;
            if (index == aImagens.Count)
                index = 0;
        }

        return aImagens[index];
    }
}
