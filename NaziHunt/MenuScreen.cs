using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt;

public class MenuScreen
{
    private readonly Texture2D background;
    private readonly Texture2D cursor;
    private readonly MenuItem startGameButton;
    private readonly MenuItem exitButton;
    MouseState mouse;

    public MenuScreen(Game g)
    {
        background = g.Content.Load<Texture2D>("images/menu_screen.png");
        startGameButton = new MenuItem(g,
            "images/menu/START_GAME_OFF", "images/menu/START_GAME_ON",
            g.GraphicsDevice.Viewport.Width / 2 - 119 / 2,
            g.GraphicsDevice.Viewport.Height / 2 + 50 / 2,
            119,
            15);

        exitButton = new MenuItem(g,
            "images/menu/QUIT_GAME_OFF", "images/menu/QUIT_GAME_ON",
            g.GraphicsDevice.Viewport.Width / 2 - 102 / 2,
            g.GraphicsDevice.Viewport.Height / 2 + 150 / 2,
            102,
            16);

        cursor = g.Content.Load<Texture2D>("images/cursor.png");
    }

    public void Update(MouseState m)
    {
        startGameButton.CheckMouseOver(m);
        exitButton.CheckMouseOver(m);
        if (startGameButton.WasClicked(m))
        {
            GameScreens.CurrentScreen = GameScreens.Screen.STAGE1;
        }
        //else if (continuar.Clicou(m))
        //    TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_CONTINUAR;
        else if (exitButton.WasClicked(m))
        {
            GameScreens.CurrentScreen = GameScreens.Screen.EXIT;
        }

        mouse = m;
    }

    public void Draw(SpriteBatch screen)
    {
        screen.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
        startGameButton.Draw(screen);
        //  continuar.DesenharNaTela(tela);
        exitButton.Draw(screen);
        screen.Draw(cursor, new Rectangle(mouse.X, mouse.Y, 32, 32), Color.White);
    }
}
