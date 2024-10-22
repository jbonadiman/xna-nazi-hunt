using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt
{
    class Player
    {

        public enum Status
        {
            PARADO_ESQUERDA, PARADO_DIREITA, CORRENDO_ESQUERDA, CORRENDO_DIREITA, PULANDO_ESQUERDA, PULANDO_DIREITA, ATIRANDO_DIREITA, ATIRANDO_ESQUERDA
        }

        public enum StatusSolo { NO_CHAO, CAINDO, SUBINDO };
        public StatusSolo status_solo;

        private AnimationSprites aCorrendoEsquerda, aCorrendoDireita, aPulandoEsquerda, aPulandoDireita;

        public Status status;

        private Texture2D _personagem, _personagem_parado_direita, _personagem_parado_esquerda, _atirando_direita, _atirando_esquerda;

        public Rectangle obj;

        private int count_pulo;

        private int elapsedTime;

        public Player(Game g, int l, int t, int w, int h)
        {

            obj = new Rectangle(l, t, w, h);


            elapsedTime = 0;

            _personagem_parado_direita = g.Content.Load<Texture2D>("images/player_jump_1.png");
            _personagem_parado_esquerda = Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_1.png"), false, true);

            aCorrendoDireita = new AnimationSprites();

            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_1.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_2.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_3.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_4.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_5.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_6.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_7.png"));
            aCorrendoDireita.Add(g.Content.Load<Texture2D>("images/player_walk_8.png"));




            aCorrendoEsquerda = new AnimationSprites();

            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_1.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_2.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_3.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_4.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_5.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_6.png"), false, true));
            aCorrendoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_8.png"), false, true));


            aPulandoDireita = new AnimationSprites();

            aPulandoDireita.Add(g.Content.Load<Texture2D>("images/player_jump_2.png"));
            //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 3.png"));
            //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 2.png"));
            //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 1.png"));

            aPulandoEsquerda = new AnimationSprites();

            aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_2.png"), false, true));
            //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 3.png"), false, true));
            //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 2.png"), false, true));
            //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 1.png"), false, true));

            _atirando_direita = g.Content.Load<Texture2D>("images/player_jump_1.png");
            _atirando_esquerda = Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_1.png"), false, true);


            status_solo = StatusSolo.NO_CHAO;


            status = Status.PARADO_DIREITA;
            _personagem = _personagem_parado_direita;

            elapsedTime = 0;

        }

        public void MoverParaDireita()
        {
            if ((status != Status.CORRENDO_DIREITA) && (status != Status.PULANDO_DIREITA) && (status != Status.PULANDO_ESQUERDA))
            {

                status = Status.CORRENDO_DIREITA;
                aCorrendoDireita.StartAnimation(300);
            }
            // obj.X += 10;
        }

        public void MoverParaEsquerda()
        {
            if ((status != Status.CORRENDO_ESQUERDA) && (status != Status.PULANDO_DIREITA) && (status != Status.PULANDO_ESQUERDA))
            {

                status = Status.CORRENDO_ESQUERDA;
                aCorrendoEsquerda.StartAnimation(300);

            }
            //  obj.X -= 10;
        }
        public void Pular()
        {
            if ((status != Status.PULANDO_DIREITA) && (status != Status.PULANDO_ESQUERDA))
            {
                count_pulo = 0;
                status_solo = StatusSolo.SUBINDO;

                if (status == Status.PARADO_DIREITA)
                {
                    status = Status.PULANDO_DIREITA;
                    aPulandoDireita.StartAnimation(100);
                }
                else
                {
                    status = Status.PULANDO_ESQUERDA;
                    aPulandoEsquerda.StartAnimation(100);
                }

            }


        }

        public void Atirar()
        {
            if ((status == Status.PARADO_DIREITA))
            {
                status = Status.ATIRANDO_DIREITA;
            }
            else if ((status == Status.PARADO_ESQUERDA))
            {
                status = Status.ATIRANDO_ESQUERDA;
            }
        }

        public void Parar()
        {
            if (status == Status.CORRENDO_DIREITA)
            {
                status = Status.PARADO_DIREITA;
                _personagem = _personagem_parado_direita;
                //  aCorrendoDireita.StopAnimation();
            }
            else if (status == Status.CORRENDO_ESQUERDA)
            {
                status = Status.PARADO_ESQUERDA;
                _personagem = _personagem_parado_esquerda;
                //    aCorrendoEsquerda.StopAnimation();
            }



        }

        public void DesenharNaTela(GameTime gameTime, SpriteBatch tela)
        {

            if ((status == Status.PARADO_DIREITA) || (status == Status.PARADO_ESQUERDA))
            {
                tela.Draw(_personagem, obj, Color.White);

            }
            else if (status == Status.CORRENDO_DIREITA)
            {
                tela.Draw(aCorrendoDireita.getImage(gameTime), obj, Color.White);
            }
            else if (status == Status.CORRENDO_ESQUERDA)
            {
                tela.Draw(aCorrendoEsquerda.getImage(gameTime), obj, Color.White);
            }
            if (status == Status.PULANDO_ESQUERDA)
            {
                tela.Draw(aPulandoEsquerda.getImage(gameTime), obj, Color.White);
            }
            else if (status == Status.PULANDO_DIREITA)
            {
                tela.Draw(aPulandoDireita.getImage(gameTime), obj, Color.White);
            }
            else if (status == Status.ATIRANDO_DIREITA)
            {
                tela.Draw(_atirando_direita, obj, Color.White);
            }
            else if (status == Status.ATIRANDO_ESQUERDA)
            {
                tela.Draw(_atirando_esquerda, obj, Color.White);
            }

        }

        public void Processar(GameTime gameTime, int time, List<GameObject> elemento)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedTime >= time)
            {
                elapsedTime = 0;
                if (((status == Status.PULANDO_ESQUERDA) || (status == Status.PULANDO_DIREITA)) && (status_solo == StatusSolo.SUBINDO))
                {
                    count_pulo++;
                    //aqui o deslocamento y sera do personagem
                    if (count_pulo <= 8)
                        obj.Y -= 25;
                    else if (count_pulo == 9)
                    {
                        status_solo = StatusSolo.CAINDO;
                    }
                }

                else if (((status == Status.PULANDO_ESQUERDA) || (status == Status.PULANDO_DIREITA)) && (status_solo == StatusSolo.CAINDO))
                {
                    obj.Y += 25;

                    for (int x = 0; x < elemento.Count; x++)
                    {
                        if ((obj.Intersects(elemento[x].obj)) &&
                          ((obj.Y + obj.Height) <= elemento[x].obj.Y + 30))
                        {
                            obj.Y = elemento[x].obj.Y - obj.Height;

                            if (status == Status.PULANDO_ESQUERDA)
                                status = Status.PARADO_ESQUERDA;
                            else if (status == Status.PULANDO_DIREITA)
                                status = Status.PARADO_DIREITA;

                            status_solo = StatusSolo.NO_CHAO;
                        }
                    }

                }

                else if (((status == Status.CORRENDO_DIREITA) || (status == Status.CORRENDO_ESQUERDA)) && (status_solo == StatusSolo.NO_CHAO))
                {
                    //Verifico se ele pode estar fora de algum sólido

                    bool pode_cair = true;
                    for (int x = 0; x < elemento.Count; x++)
                    {
                        if ((elemento[x].obj.Y == (obj.Y + obj.Height)) && (((obj.X + obj.Width) >= elemento[x].obj.X) && (obj.X <= (elemento[x].obj.X + elemento[x].obj.Width))))
                        {
                            pode_cair = false;
                            break;
                        }
                    }
                    if (pode_cair)
                    {
                        if (status == Status.CORRENDO_DIREITA)
                            status = Status.PULANDO_DIREITA;
                        else if (status == Status.CORRENDO_ESQUERDA)
                            status = Status.PULANDO_ESQUERDA;

                        status_solo = StatusSolo.CAINDO;

                    }

                }


            }

        }
    }
}
