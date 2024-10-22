﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace NaziHunt
{
    class Inimigo : ElementoJogo
    {

        AnimationSprites AndandoDireita, AndandoEsquerda;

        public enum StatusInimigo { ANDANDO_DIREITA, ANDANDO_ESQUERDA , ATIRANDO_ESQUERDA, ATIRANDO_DIREITA }
        public StatusInimigo status;

        Texture2D atirando_esquerda, atirando_direita;

        int elapsedTimeTiro;

        int elapsedTime;

        TiroInimigo tiroInimigo;

        Game1 game;

        public void Atirar(GameTime gameTime)
        {
            //Atira a cada 3 segundos
            elapsedTimeTiro += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTimeTiro >= 3000)
            {
                if (status == StatusInimigo.ANDANDO_DIREITA)
                {
                    status = StatusInimigo.ATIRANDO_DIREITA;



                }
                else if (status == StatusInimigo.ANDANDO_ESQUERDA)
                {
                    status = StatusInimigo.ATIRANDO_ESQUERDA;

                }

                elapsedTimeTiro = 0;
            }
        }

        public Inimigo(Game1 g, int l, int t, int w, int h)
        {
            game = g;

            atirando_direita =Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_atirando.png"), false, true);
            atirando_esquerda = g.Content.Load<Texture2D>("images/inimigo_atirando.png");

            AndandoDireita = new AnimationSprites();
     //       AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_1.png"), false, true));
            AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_2.png"), false, true));
            AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_3.png"), false, true));
            AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_4.png"), false, true));
            AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_5.png"), false, true));
            AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_6.png"), false, true));
            AndandoDireita.StartAnimation(100);

            AndandoEsquerda = new AnimationSprites();
       //     AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_1.png"));
            AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_2.png"));
            AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_3.png"));
            AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_4.png"));
            AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_5.png"));
            AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/inimigo_andando_6.png"));
            AndandoEsquerda.StartAnimation(100);

            obj = new Rectangle(l, t, w, h);

            status = StatusInimigo.ANDANDO_ESQUERDA;

            elapsedTime = 0;
            elapsedTimeTiro = 0;

        }

        public void Processar(GameTime gameTime, int time, List<TiroInimigo> elemento)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            Atirar(gameTime);

            if (elapsedTime >= time)
            {
                elapsedTime = 0;
                if ((status == StatusInimigo.ATIRANDO_DIREITA) || (status == StatusInimigo.ATIRANDO_ESQUERDA))
                {

                    TiroInimigo tiroInimigo = new TiroInimigo(game);
                    elemento.Add(tiroInimigo);
                    tiroInimigo.Disparar(this);

                    if (status == StatusInimigo.ATIRANDO_DIREITA)
                        status = StatusInimigo.ANDANDO_DIREITA;
                    else if (status == StatusInimigo.ATIRANDO_ESQUERDA)
                        status = StatusInimigo.ANDANDO_ESQUERDA;
                }
                else if (status == StatusInimigo.ANDANDO_DIREITA)
                    obj.X += 5;
                else if (status == StatusInimigo.ANDANDO_ESQUERDA)
                    obj.X -= 5;

            }
        }

        public void DesenharNaTela(GameTime gameTime, SpriteBatch tela)
        {
            if (status == StatusInimigo.ANDANDO_DIREITA)
                tela.Draw(AndandoDireita.getImage(gameTime), obj, Color.White);
            else if (status == StatusInimigo.ANDANDO_ESQUERDA)
                tela.Draw(AndandoEsquerda.getImage(gameTime), obj, Color.White);
            else if (status == StatusInimigo.ATIRANDO_DIREITA)
                tela.Draw(atirando_direita, obj, Color.White);
            else if (status == StatusInimigo.ATIRANDO_ESQUERDA)
                tela.Draw(atirando_esquerda, obj, Color.White);
        }

    }
}
