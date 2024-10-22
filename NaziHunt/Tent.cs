using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace NaziHunt
{
    class Tent : GameObject
    {
        Texture2D imagem;

        public Tent(Game g, int l, int t, int w, int h)
        {
            obj = new Rectangle(l, t - 10, 615 / 2, 289 / 2);
            imagem = g.Content.Load<Texture2D>("images/tent.png");
        }

        public void DesenharNaTela(SpriteBatch tela)
        {
            tela.Draw(imagem, obj, Color.White);
        }
    }

}
