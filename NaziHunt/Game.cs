using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt;

/// <summary>
/// This is the main type for your game
/// </summary>
public class Game : Microsoft.Xna.Framework.Game
{
    SpriteBatch spriteBatch;
    KeyboardState keyboard;
    MouseState mouse;
    MenuScreen menuScreen;
    Stage1Screen stage1Screen;
    CreditsScreen creditsScreen;

    public Game()
    {
        Content.RootDirectory = "assets";
        _ = new GraphicsDeviceManager(this)
        {
            IsFullScreen = false
        };
    }

    protected override void Initialize()
    {
        GameScreens.CurrentScreen = GameScreens.Screen.MENU;
        base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        menuScreen = new MenuScreen(this);
        stage1Screen = new Stage1Screen(this);
        creditsScreen = new CreditsScreen(this);
        /*aElemento = new List<ElementoJogo>();

        //Adiciona vários blocos de chão
        int posx = 0;
        for (int x = 1; x <= 60; x++)
        {
            aElemento.Add(new Chao(this, posx, altura_tela - 25, 138, 25));
            posx += 138;
        }


        //Adiciona vários blocos de cerca
        posx = 0;

        for (int x = 1; x <= 60; x++)
        {
            aElemento.Add(new FundoTela(this, posx, altura_tela - 100, 170, 49));
            posx += 170;
        }*/
        base.LoadContent();
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        // Allows the game to exit
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        {
            Exit();
        }

        keyboard = Keyboard.GetState();
        mouse = Mouse.GetState();
        if (GameScreens.CurrentScreen == GameScreens.Screen.MENU)
        {
            menuScreen.Update(mouse);
        }
        else if (GameScreens.CurrentScreen == GameScreens.Screen.STAGE1)
        {
            stage1Screen.Update(gameTime, keyboard);
        }
        else if (GameScreens.CurrentScreen == GameScreens.Screen.EXIT)
        {
            Exit();
        }

        base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        if (GameScreens.CurrentScreen == GameScreens.Screen.MENU)
        {
            menuScreen.Draw(spriteBatch);
        }
        else if (GameScreens.CurrentScreen == GameScreens.Screen.STAGE1)
        {
            stage1Screen.Draw(gameTime, spriteBatch);
        }
        else if (GameScreens.CurrentScreen == GameScreens.Screen.CREDITS)
        {
            creditsScreen.Draw(spriteBatch, GraphicsDevice);
        }

        spriteBatch.End();
        base.Draw(gameTime);
    }
}
