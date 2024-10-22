using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class Bullet
{
    private Texture2D tiro_esquerda, tiro_direita;

    public enum Sentido { PARADO, DIREITA, ESQUERDA };

    public Sentido sentido;

    private Rectangle obj;

    public bool tiro_disparado;

    private int elapsedTime;

    public Bullet(Game g)
    {
        tiro_direita = g.Content.Load<Texture2D>("images/bullet.png");
        tiro_esquerda = Flip.FlipImage(g.Content.Load<Texture2D>("images/bullet.png"), false, true);
        sentido = Sentido.PARADO;
        tiro_disparado = false;
        obj = new Rectangle(0, 0, 10, 10);
        elapsedTime = 0;
    }

    public void Disparar(Player p)
    {
        if ((p.status == Player.Status.PARADO_DIREITA) || (p.status == Player.Status.ATIRANDO_DIREITA) || (p.status == Player.Status.CORRENDO_DIREITA))
        {
            if (!tiro_disparado)
            {
                //Faz o posicionamento do tiro na tela
                obj.X = p.obj.X + p.obj.Width;
                obj.Y = p.obj.Y + 37;
                sentido = Sentido.DIREITA;
                tiro_disparado = true;
            }
        }
        else if ((p.status == Player.Status.PARADO_ESQUERDA) || (p.status == Player.Status.ATIRANDO_ESQUERDA) || (p.status == Player.Status.CORRENDO_ESQUERDA))

        {
            if (!tiro_disparado)
            {
                //Faz o posicionamento do tiro na tela
                obj.X = p.obj.X - obj.Width;
                obj.Y = p.obj.Y + 37;
                sentido = Sentido.ESQUERDA;
                tiro_disparado = true;
            }
        }

    }

    public void Processar(GameTime gameTime, int time, List<GameObject> elemento)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            if (sentido == Sentido.DIREITA)
            {
                obj.X += 15;

                for (int x = 0; x < elemento.Count; x++)
                {
                    if (elemento[x] is Enemy)
                    {
                        if (obj.Intersects(elemento[x].obj))
                        {
                            tiro_disparado = false;
                            sentido = Sentido.PARADO;
                            elemento.RemoveAt(x);
                            x--;
                        }
                    }
                }

                if (obj.X > 800) //800 é a largura da tela em pixels
                {
                    tiro_disparado = false;
                    sentido = Sentido.PARADO;
                }
            }
            else if (sentido == Sentido.ESQUERDA)
            {
                obj.X -= 15;


                for (int x = 0; x < elemento.Count; x++)
                {
                    if (elemento[x] is Enemy)
                    {
                        if (obj.Intersects(elemento[x].obj))
                        {
                            tiro_disparado = false;
                            sentido = Sentido.PARADO;
                            elemento.RemoveAt(x);
                            x--;
                        }
                    }
                }

                if (obj.X < -obj.Width) //Sair da tela pela esquerda ?
                {
                    tiro_disparado = false;
                    sentido = Sentido.PARADO;
                }
            }
        }
    }

    public void DesenharNaTela(SpriteBatch tela)
    {
        if (sentido == Sentido.DIREITA)
            tela.Draw(tiro_direita, obj, Color.White);
        if (sentido == Sentido.ESQUERDA)
            tela.Draw(tiro_esquerda, obj, Color.White);

    }
}
