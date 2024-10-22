using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt
{
    class FundoTela : ElementoJogo
    {
         private Texture2D imagem;

        public FundoTela(Game1 g, int l, int t, int w, int h)
        {
            obj = new Rectangle(l, t, w, h);
            imagem = g.Content.Load<Texture2D>("images/fase1nova.png");
        }

        public void DesenharNaTela(SpriteBatch tela)
        {
            tela.Draw(imagem, obj, Color.White);
        }
    }
}
