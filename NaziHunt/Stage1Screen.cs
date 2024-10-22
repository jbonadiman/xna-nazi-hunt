using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace NaziHunt;

public class Stage1Screen
{
    readonly Player player;
    readonly List<GameObject> gameObjects;
    // Texture2D fundo;
    readonly List<Bullet> bullets;
    readonly List<EnemyBullet> enemyBullets;
    readonly Song song;
    KeyboardState keyboardOldState;
    readonly SoundEffect bulletSound;
    public int screenWidth, screenHeight;
    Game game;
    Background background, background2;

    //  KeyboardState oldStateTeclado;

    public Stage1Screen(Game g)
    {
        //Score = 0;
        game = g;
        screenHeight = g.GraphicsDevice.Viewport.Height;
        screenWidth = g.GraphicsDevice.Viewport.Width;
        // cont_inimigo = 0;
        //fonte = g.Content.Load<SpriteFont>("SpriteFont1");
        player = new Player(game, 300, screenHeight - 10 - 110, 90, 110);

        gameObjects = [];
        bullets = [];
        enemyBullets = [];

        song = g.Content.Load<Song>("sounds/music.ogg");
        bulletSound = g.Content.Load<SoundEffect>("sounds/bullet.wav");

        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = true;

        int posx = 0;
        for (int x = 1; x <= 100; x++)
        {
            gameObjects.Add(new Ground(game, posx, screenHeight - 10, 131, 123));
            //aSuporte.Add(new Suporte(game, 900, altura_tela - 221, 121, 157));
            // aElemento.Add(new Suporte(game, posx, altura_tela - 221, 131, 157));
            //aSuporte.Add(new Suporte(game, 0, altura_tela - 221, 121, 157));

            //aElemento.Add(new FundoTela(game, posx, altura_tela - 100, 170, 49));
            //aElemento.Add(new FundoTela(game, posx, altura_tela - 600, 500, 675));
            posx += 131;
        }

        background = new Background(game, 0, 0, 800, 480);
        background2 = new Background(game, 800, 0, 800, 480);
        gameObjects.Add(background);
        gameObjects.Add(background2);
        gameObjects.Add(new Crate(game, 900, 370, 100, 100));
        gameObjects.Add(new Enemy(game, 1300, screenHeight - 10 - 110, 90, 110));
        gameObjects.Add(new Enemy(game, 2000, screenHeight - 10 - 110, 90, 110));
        gameObjects.Add(new Enemy(game, 2500, screenHeight - 10 - 110, 90, 110));
        gameObjects.Add(new Tent(game, 3000, screenHeight - 10 - 110, 90, 110));

        //inimigo = new Inimigo(game, 600, 50, 90, 110);
        //for (int x = 1; x <= 20; x++)
        //{
        //    aElemento.Add(new FundoTela(game, posx, altura_tela + 100, 170, 49));
        //    posx += 131;
        //    //posx += 131;
        //}
    }

    public void Update(GameTime gameTime, KeyboardState keyboard)
    { //1
        for (int x = 0; x < gameObjects.Count; x++)
        { //2
            if (gameObjects[x] is Tent)
            {
                if (player.obj.Intersects(gameObjects[x].rect))
                //Vai para a tela de créditos
                {
                    GameScreens.CurrentScreen = GameScreens.Screen.CREDITS;
                }
            }
        } //2

        for (int x = 0; x < bullets.Count; x++)
        {
            //Tiro do personagem
            bullets[x].Update(gameTime, 30, gameObjects);
            if (!bullets[x].shotFired)
            {
                bullets.RemoveAt(x);
                x--;
            }
        }

        for (int x = 0; x < enemyBullets.Count; x++)
        {
            //Tiro do personagem
            enemyBullets[x].Update(gameTime, 30, player);

            if (!enemyBullets[x].isShotFired)
            {
                enemyBullets.RemoveAt(x);
                x--;
            }
        }

        for (int x = 0; x < gameObjects.Count; x++)
        {
            if (gameObjects[x] is Enemy)
            {
                (gameObjects[x] as Enemy).Update(gameTime, 30, enemyBullets);
            }
        }

        player.Update(gameTime, 30, gameObjects);
        if (keyboard.IsKeyDown(Keys.Right))
        {
            player.MoveToTheRight();
            ShiftStage(-7);

            //Checa a colisao da caixa
            if (checkCrateCollision())
            {
                ShiftStage(7);
            }

            if (background.rect.X < -800)
            {
                background.rect.X = background2.rect.X + 800;
            }
            if (background2.rect.X < -800)
            {
                background2.rect.X = background.rect.X + 800;
            }
        }
        else if (keyboard.IsKeyDown(Keys.Left))
        {
            player.MoveToTheLeft();
            ShiftStage(7);

            //Checa a colisao da caixa
            if (checkCrateCollision())
            {
                ShiftStage(-7);
            }


            if (background.rect.X > 800)
            {
                background.rect.X = background2.rect.X - 800;
            }
            if (background2.rect.X > 800)
            {
                background2.rect.X = background.rect.X - 800;
            }
        }

        else if (keyboard.IsKeyDown(Keys.Up))
        {
            player.Jump();
        }
        else
        {
            player.Stop();
        }

        if ((keyboard.IsKeyDown(Keys.Space)) && (!keyboardOldState.IsKeyDown(Keys.Space)))
        {
            bulletSound.Play();
            Bullet tiro = new Bullet(game);
            bullets.Add(tiro);
            tiro.Shoot(player);
            player.Shoot();
        }

        keyboardOldState = keyboard;
    }

    public void Draw(SpriteBatch screen)
    {
        //Adiciona vários blocos de cerca
        //Desenha os suportes, se houver
        for (int x = 0; x < gameObjects.Count; x++)
        {
            if (gameObjects[x] is Ground)
            {
                (gameObjects[x] as Ground).Draw(screen);
            }
            else if (gameObjects[x] is Crate)
            //Desenha o chão
            {
                (gameObjects[x] as Crate).Draw(screen);
            }
            else if (gameObjects[x] is Background)
            {
                (gameObjects[x] as Background).Draw(screen);
            }
            else if (gameObjects[x] is Tent)
            {
                (gameObjects[x] as Tent).Draw(screen);
            }
            else if (gameObjects[x] is Enemy)
            {
                (gameObjects[x] as Enemy).Draw(screen);
            }
        }

        //  inimigo.DesenharNaTela(gameTime, tela);
        for (int x = 0; x < bullets.Count; x++)
        {
            bullets[x].Draw(screen);
        }

        for (int x = 0; x < enemyBullets.Count; x++)
        {
            enemyBullets[x].Draw(screen);
        }

        player.Draw(screen);
    }

    //Desenha os suportes, se houver
    /* for (int x = 0; x < aSuporte.Count; x++)
     {
         //Desenha o chão
         ((Suporte)aSuporte[x]).DesenharNaTela(tela);
     }

     personagem.DesenharNaTela(spriteBatch);

     spriteBatch.End();

     base.Draw(gameTime);
 }*/

    public void ShiftStage(int l)
    {
        for (int x = 0; x < gameObjects.Count; x++)
        {
            gameObjects[x].MoveOnX(l);
        }
        //Desloca suportes, se houver
        /*for (int x = 0; x < aSuporte.Count; x++)
        {
            ((Suporte)aSuporte[x]).DeslocarObjeto(l);
        }*/
    }

    public bool checkCrateCollision()
    {
        bool collided = false;
        for (int x = 0; x < gameObjects.Count; x++)
        {
            if (gameObjects[x] is Crate)
                if (player.obj.Intersects(gameObjects[x].rect))
                {
                    collided = true;
                    break;
                }
        }

        return collided;
    }
}
