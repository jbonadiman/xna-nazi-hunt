using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace NaziHunt
{
    class TelaFase1
    {

        Personagem personagem;

        List<ElementoJogo> aElemento;


       // Texture2D fundo;

        List<Tiro> aTiro;

        List<TiroInimigo> aTiroInimigo;

        Song musica;

        KeyboardState oldStateTeclado;

        SoundEffect somTiro;

        Tiro tiro;

        Inimigo inimigo;

        public int largura_tela, altura_tela;

        Game1 game;

        FundoTela fundo, fundo2;



      //  KeyboardState oldStateTeclado;

        public TelaFase1(Game1 g)
        {
            //Score = 0;
            game = g;
            altura_tela = g.GraphicsDevice.Viewport.Height;
            largura_tela = g.GraphicsDevice.Viewport.Width;
           // cont_inimigo = 0;

            //fonte = g.Content.Load<SpriteFont>("SpriteFont1");

            personagem = new Personagem(game, 300, altura_tela - 10 - 110, 90, 110);

            aElemento = new List<ElementoJogo>();
            aTiro = new List<Tiro>();
            aTiroInimigo = new List<TiroInimigo>();


            musica = g.Content.Load<Song>("sounds/459121_At_Arms.mp3");
            somTiro = g.Content.Load<SoundEffect>("sounds/37236__shades__gun-pistol-one-shot.wav");

            MediaPlayer.Play(musica);
            MediaPlayer.IsRepeating = true;

            int posx = 0;
            for (int x = 1; x <= 100; x++)
            {

              aElemento.Add(new Chao(game, posx, altura_tela - 10, 131, 123));
              //aSuporte.Add(new Suporte(game, 900, altura_tela - 221, 121, 157));
             // aElemento.Add(new Suporte(game, posx, altura_tela - 221, 131, 157));
              //aSuporte.Add(new Suporte(game, 0, altura_tela - 221, 121, 157));

                //aElemento.Add(new FundoTela(game, posx, altura_tela - 100, 170, 49));
                //aElemento.Add(new FundoTela(game, posx, altura_tela - 600, 500, 675));


                posx += 131;
            }

            fundo = new FundoTela(game, 0, 0, 800, 480);
            fundo2 = new FundoTela(game, 800, 0, 800, 480);
            aElemento.Add(fundo);
            aElemento.Add(fundo2);
            aElemento.Add(new Caixa(game, 900, 370, 100, 100));
            aElemento.Add(new Inimigo(game, 1300, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Inimigo(game, 2000, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Inimigo(game, 2500, altura_tela - 10 - 110, 90, 110));
            aElemento.Add(new Barraco(game, 3000, altura_tela - 10 - 110, 90, 110));


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
                if (aElemento[x] is Barraco)

                    if (personagem.obj.Intersects(aElemento[x].obj))
                        //Vai para a tela de créditos
                        TelasDoJogo.status = TelasDoJogo.TelaJogo.TELA_CREDITOS;

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
                if (aElemento[x] is Inimigo)

                    (aElemento[x] as Inimigo).Processar(gameTime, 30, aTiroInimigo);

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
                Tiro tiro = new Tiro(game);
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
                if (aElemento[x] is Chao)
                    (aElemento[x] as Chao).DesenharNaTela(tela);
                else  if (aElemento[x] is Caixa)
                    //Desenha o chão
                    (aElemento[x] as Caixa).DesenharNaTela(tela);
                else if (aElemento[x] is FundoTela)
                  (aElemento[x] as FundoTela).DesenharNaTela(tela);
                     else if (aElemento[x] is Barraco)
                  (aElemento[x] as Barraco).DesenharNaTela(tela);
               else if (aElemento[x] is Inimigo)
                    (aElemento[x] as Inimigo).DesenharNaTela(gameTime, tela);


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
                if(aElemento[x] is Caixa)
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
