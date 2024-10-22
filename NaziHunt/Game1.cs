using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        List<ElementoJogo> aElemento;

        KeyboardState teclado;

        MouseState mouse;

        TelaMenu tela_menu;

        TelaFase1 tela_fase1;
        TelaCreditos tela_credito;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "assets";
        }






        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_MENU;
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

            tela_menu = new TelaMenu(this);
            tela_fase1 = new TelaFase1(this);
            tela_credito = new TelaCreditos(this);


            spriteBatch = new SpriteBatch(GraphicsDevice);

            aElemento = new List<ElementoJogo>();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
                this.Exit();

            teclado = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_MENU)
                tela_menu.Update(gameTime, mouse);
            else if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_FASE1)
                tela_fase1.Update(gameTime, teclado);
            else if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_SAIR)
                this.Exit();





            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_MENU)
                tela_menu.Draw(gameTime, spriteBatch);
            else if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_FASE1)
                tela_fase1.Draw(gameTime, spriteBatch);
            else if (TelasDoJogo.status == TelasDoJogo.TelaJogo.TELA_CREDITOS)
                tela_credito.Draw(gameTime, spriteBatch, GraphicsDevice);



            spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }
