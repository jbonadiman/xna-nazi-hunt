using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace NaziHunt
{
    class Stage1Screen
    {

        Player personagem;

        List<GameObject> aElemento;


        // Texture2D fundo;

        List<Bullet> aTiro;

        List<EnemyBullet> aTiroInimigo;

        Song musica;

        KeyboardState oldStateTeclado;

        SoundEffect somTiro;

        Bullet tiro;

        Enemy inimigo;

        public int largura_tela, altura_tela;

        Game game;

        Background fundo, fundo2;



        //  KeyboardState oldStateTeclado;

        public Stage1Screen(Game g)
        {
            //Score = 0;
            game = g;
            altura_tela = g.GraphicsDevice.Viewport.Height;
            largura_tela = g.GraphicsDevice.Viewport.Width;
            // cont_inimigo = 0;

            //fonte = g.Content.Load<SpriteFont>("SpriteFont1");

            personagem = new Player(game, 300, altura_tela - 10 - 110, 90, 110);

            aElemento = new List<GameObject>();
            aTiro = new List<Bullet>();
            aTiroInimigo = new List<EnemyBullet>();


            musica = g.Content.Load<Song>("sounds/music.ogg");
            somTiro = g.Content.Load<SoundEffect>("sounds/bullet.wav");

            MediaPlayer.Play(musica);
            MediaPlayer.IsRepeating = true;

            int posx = 0;
            for (int x = 1; x <= 100; x++)
            {

                aElemento.Add(new Ground(game, posx, altura_tela - 10, 131, 123));
                //aSuporte.Add(new Suporte(game, 900, altura_tela - 221, 121, 157));
                // aElemento.Add(new Suporte(game, posx, altura_tela - 221, 131, 157));
                //aSuporte.Add(new Suporte(game, 0, altura_tela - 221, 121, 157));

                //aElemento.Add(new FundoTela(game, posx, altura_tela - 100, 170, 49));
                //aElemento.Add(new FundoTela(game, posx, altura_tela - 600, 500, 675));


                posx += 131;
            }

            fundo = new Background(game, 0, 0, 800, 480);
            fundo2 = new Background(game, 800, 0, 800, 480);
            aElemento.Add(fundo);
            aElemento.Add(fundo2);
            aElemento.Add(new Crate(game, 900, 370, 100, 100));
            aElemento.Add(new Enemy(game, 1300, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Enemy(game, 2000, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Enemy(game, 2500, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Tent(game, 3000, altura_tela - 10 - 110, 90, 110));


            //inimigo = new Inimigo(game, 600, 50, 90, 110);
            //for (int x = 1; x <= 20; x++)
            //{
            //    aElemento.Add(new FundoTela(game, posx, altura_tela + 100, 170, 49));
            //    posx += 131;
            //    //posx += 131;
            //}


        }

        public void Update(GameTime gameTime, KeyboardState teclado)
        { //1

            for (int x = 0; x < aElemento.Count; x++)
            { //2
                if (aElemento[x] is Tent)

                    if (personagem.obj.Intersects(aElemento[x].obj))
                        //Vai para a tela de créditos
                        GameScreens.status = GameScreens.Screen.CREDITS;

            } //2


            for (int x = 0; x < aTiro.Count; x++)
            {
                //Tiro do personagem
                aTiro[x].Processar(gameTime, 30, aElemento);
                if (!aTiro[x].tiro_disparado)
                {
                    aTiro.RemoveAt(x);
                    x--;
                }
            }

            for (int x = 0; x < aTiroInimigo.Count; x++)
            {
                //Tiro do personagem
                aTiroInimigo[x].Processar(gameTime, 30, personagem);

                if (!aTiroInimigo[x].tiro_disparado)
                {
                    aTiroInimigo.RemoveAt(x);
                    x--;
                }
            }

            for (int x = 0; x < aElemento.Count; x++)
            {
                if (aElemento[x] is Enemy)

                    (aElemento[x] as Enemy).Processar(gameTime, 30, aTiroInimigo);

            }

            personagem.Processar(gameTime, 30, aElemento);


            if (teclado.IsKeyDown(Keys.Right))
            {
                personagem.MoverParaDireita();
                DeslocarCenario(-7);

                //Checa a colisao da caixa
                if (checarColisaoCaixa())
                {
                    DeslocarCenario(7);
                }

                if (fundo.obj.X < -800)
                    fundo.obj.X = fundo2.obj.X + 800;
                if (fundo2.obj.X < -800)
                    fundo2.obj.X = fundo.obj.X + 800;


            }
            else if (teclado.IsKeyDown(Keys.Left))
            {
                personagem.MoverParaEsquerda();
                DeslocarCenario(7);

                //Checa a colisao da caixa
                if (checarColisaoCaixa())
                {
                    DeslocarCenario(-7);
                }


                if (fundo.obj.X > 800)
                    fundo.obj.X = fundo2.obj.X - 800;
                if (fundo2.obj.X > 800)
                    fundo2.obj.X = fundo.obj.X - 800;
            }

            else if (teclado.IsKeyDown(Keys.Up))
            {
                personagem.Pular();
            }
            else
            {
                personagem.Parar();
            }

            if ((teclado.IsKeyDown(Keys.Space)) && (!oldStateTeclado.IsKeyDown(Keys.Space)))
            {

                somTiro.Play();
                Bullet tiro = new Bullet(game);
                aTiro.Add(tiro);
                tiro.Disparar(personagem);
                personagem.Atirar();



            }

            oldStateTeclado = teclado;
        }

        //1




        public void Draw(GameTime gameTime, SpriteBatch tela)
        {

            //Adiciona vários blocos de cerca


            //Desenha os suportes, se houver
            for (int x = 0; x < aElemento.Count; x++)
            {
                if (aElemento[x] is Ground)
                    (aElemento[x] as Ground).DesenharNaTela(tela);
                else if (aElemento[x] is Crate)
                    //Desenha o chão
                    (aElemento[x] as Crate).DesenharNaTela(tela);
                else if (aElemento[x] is Background)
                    (aElemento[x] as Background).DesenharNaTela(tela);
                else if (aElemento[x] is Tent)
                    (aElemento[x] as Tent).DesenharNaTela(tela);
                else if (aElemento[x] is Enemy)
                    (aElemento[x] as Enemy).DesenharNaTela(gameTime, tela);


            }

            //  inimigo.DesenharNaTela(gameTime, tela);

            for (int x = 0; x < aTiro.Count; x++)
            {
                aTiro[x].DesenharNaTela(tela);
            }

            for (int x = 0; x < aTiroInimigo.Count; x++)
            {
                aTiroInimigo[x].DesenharNaTela(tela);
            }

            personagem.DesenharNaTela(gameTime, tela);
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


        public void DeslocarCenario(int l)
        {
            for (int x = 0; x < aElemento.Count; x++)
            {
                aElemento[x].DeslocarObjetoX(l);
            }
            //Desloca suportes, se houver
            /*for (int x = 0; x < aSuporte.Count; x++)
            {
                ((Suporte)aSuporte[x]).DeslocarObjeto(l);
            }*/
        }

        public bool checarColisaoCaixa()
        {
            bool colidiu = false;
            for (int x = 0; x < aElemento.Count; x++)
            {
                if (aElemento[x] is Crate)
                    if (personagem.obj.Intersects(aElemento[x].obj))
                    {
                        colidiu = true;
                        break;
                    }
            }

            return colidiu;
        }
    }
}
