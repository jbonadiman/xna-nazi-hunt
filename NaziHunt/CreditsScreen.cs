using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class CreditsScreen : GameObject
{
    private readonly Texture2D credits;//, cursor;
    //MenuItem iniciar_jogo, sair;

    //MouseState mouse;

    public CreditsScreen(Game g)
    {
        credits = g.Content.Load<Texture2D>("images/credits.png");
    }

    public void Draw(SpriteBatch screen, GraphicsDevice graphics)
    {
        screen.Draw(
            credits,
            new Rectangle(graphics.Viewport.Width / 2 - 238 / 2, graphics.Viewport.Height / 2 - 289 / 2, 238, 289),
            Color.White);
        //iniciar_jogo.DesenharNaTela(tela);
        //  continuar.DesenharNaTela(tela);
        // sair.DesenharNaTela(tela);
        //tela.Draw(cursor, new Rectangle(mouse.X - 25, mouse.Y - 25, 50, 50), Color.Black);
    }
}
