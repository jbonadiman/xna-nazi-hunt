using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class CreditsScreen : GameObject
{
    private readonly Texture2D credits;//, cursor;
    //MenuItem iniciar_jogo, sair;

    //MouseState mouse;
    private readonly Game game;

    public CreditsScreen(Game g)
    {
        game = g;
        credits = g.Content.Load<Texture2D>("images/credits.png");
    }

    public void Draw(SpriteBatch screen)
    {
        screen.Draw(
            credits,
            new Rectangle(game.ViewportWidth / 2 - 238 / 2, game.ViewportHeight / 2 - 289 / 2, 238, 289),
            Color.White);
        //iniciar_jogo.DesenharNaTela(tela);
        //  continuar.DesenharNaTela(tela);
        // sair.DesenharNaTela(tela);
        //tela.Draw(cursor, new Rectangle(mouse.X - 25, mouse.Y - 25, 50, 50), Color.Black);
    }
}
