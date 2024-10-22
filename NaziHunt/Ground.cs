using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class Ground : GameObject
{
    private Texture2D imagem;

    public Ground(Game g, int l, int t, int w, int h)
    {
        obj = new Rectangle(l, t, w, h);
        imagem = g.Content.Load<Texture2D>("images/ground.png");
    }

    public void DesenharNaTela(SpriteBatch tela)
    {
        tela.Draw(imagem, obj, Color.White);
    }
}
