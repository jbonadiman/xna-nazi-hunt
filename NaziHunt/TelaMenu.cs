using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt
{
    class TelaMenu
    {
        Texture2D fundo, cursor;
        MenuItem iniciar_jogo, sair;

        MouseState mouse;

        public TelaMenu(Game1 g)
        {

            fundo = g.Content.Load<Texture2D>("images/fundo.png");
            iniciar_jogo = new MenuItem(g, "images/menu/START_GAME_OFF", "images/menu/START_GAME_ON", g.GraphicsDevice.Viewport.Width / 2 - 119 / 2, g.GraphicsDevice.Viewport.Height / 2 + 50/ 2, 119, 15);
            sair = new MenuItem(g, "images/menu/QUIT_GAME_OFF", "images/menu/QUIT_GAME_ON", g.GraphicsDevice.Viewport.Width / 2 - 102 / 2, g.GraphicsDevice.Viewport.Height / 2 + 150 / 2, 102, 16);
          //  continuar = new MenuItem(g, "imagens/continuar", 280, 250, 240, 80);
          //  sair = new MenuItem(g, "imagens/sair", 280, 350, 240, 80);
            cursor = g.Content.Load<Texture2D>("images/cursor.png");

        }

        public void Update(GameTime gameTime, MouseState m)
        {

            iniciar_jogo.CheckMouseOver(m);
            sair.CheckMouseOver(m);
           if (iniciar_jogo.Clicou(m))
               TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_FASE1;
            //else if (continuar.Clicou(m))
            //    TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_CONTINUAR;
         else if (sair.Clicou(m))
                TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_SAIR;

            mouse = m;

        }

        public void Draw(GameTime gameTime, SpriteBatch tela)
        {
         tela.Draw(fundo, new Rectangle(0, 0, 800, 480), Color.White);
         iniciar_jogo.DesenharNaTela(tela);
          //  continuar.DesenharNaTela(tela);
          sair.DesenharNaTela(tela);
          tela.Draw(cursor, new Rectangle(mouse.X, mouse.Y, 32, 32), Color.White);

        }
    }
}
