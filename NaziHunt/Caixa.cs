using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt
{
    class Caixa : ElementoJogo
    {
        Texture2D imagem;

        public Caixa(Game1 g, int l, int t, int w, int h)
        {
            obj = new Rectangle(l, t, w, h);
            imagem = g.Content.Load<Texture2D>("images/Crate.png");
        }

        public void DesenharNaTela(SpriteBatch tela)
        {
            tela.Draw(imagem, obj, Color.White);
        }
    }
}
