using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt
{
    class Background : GameObject
    {
        private Texture2D imagem;

        public Background(Game g, int l, int t, int w, int h)
        {
            obj = new Rectangle(l, t, w, h);
            imagem = g.Content.Load<Texture2D>("images/background.png");
        }

        public void DesenharNaTela(SpriteBatch tela)
        {
            tela.Draw(imagem, obj, Color.White);
        }
    }
}
