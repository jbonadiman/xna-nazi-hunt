using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace NaziHunt
{
    class TelaCreditos : ElementoJogo

    {
        Texture2D fundo;//, cursor;
        //MenuItem iniciar_jogo, sair;

        //MouseState mouse;

        public TelaCreditos(Game1 g)
        {

            fundo = g.Content.Load<Texture2D>("images/creditos.png");
        }


        public void Draw(GameTime gameTime, SpriteBatch tela, GraphicsDevice graphics)
        {
         tela.Draw(fundo, new Rectangle(graphics.Viewport.Width / 2 - 238 / 2, graphics.Viewport.Height / 2 - 289 / 2, 238, 289), Color.White);
         //iniciar_jogo.DesenharNaTela(tela);
          //  continuar.DesenharNaTela(tela);
         // sair.DesenharNaTela(tela);
          //tela.Draw(cursor, new Rectangle(mouse.X - 25, mouse.Y - 25, 50, 50), Color.Black);

        }
    }
}
