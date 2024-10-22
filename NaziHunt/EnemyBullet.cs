using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class EnemyBullet
{
    private readonly Texture2D leftBullet;
    private readonly Texture2D rightBullet;

    public enum Orientation { STOP, RIGHT, LEFT };
    public Orientation currentOrientation;
    private Rectangle rect;
    public bool isShotFired;
    private int elapsedTime;
    readonly Game game;

    public EnemyBullet(Game g)
    {
        rightBullet = g.Content.Load<Texture2D>("images/bullet.png");
        leftBullet = Flip.FlipImage(g.Content.Load<Texture2D>("images/bullet.png"), false, true);
        currentOrientation = Orientation.STOP;
        isShotFired = false;
        rect = new Rectangle(0, 0, 10, 10);
        elapsedTime = 0;

        game = g;
    }

    public void Shoot(Enemy p)
    {
        if ((p.state == Enemy.EnemyState.RIGHT_WALK))
        {
            if (!isShotFired)
            {
                //Faz o posicionamento do tiro na tela
                rect.X = p.rect.X + p.rect.Width;
                rect.Y = p.rect.Y + 37;
                currentOrientation = Orientation.RIGHT;
                isShotFired = true;
            }
        }
        else if ((p.state == Enemy.EnemyState.LEFT_WALK))
        {
            if (!isShotFired)
            {
                //Faz o posicionamento do tiro na tela
                rect.X = p.rect.X - rect.Width;
                rect.Y = p.rect.Y + 37;
                currentOrientation = Orientation.LEFT;
                isShotFired = true;
            }
        }
    }

    public void Update(GameTime gameTime, int time, Player player)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            if (currentOrientation == Orientation.RIGHT)
            {
                rect.X += 15;
                if (rect.Intersects(player.obj))
                {
                    game.Exit();
                }

                if (rect.X > 800) //800 é a largura da tela em pixels
                {
                    isShotFired = false;
                    currentOrientation = Orientation.STOP;
                }
            }
            else if (currentOrientation == Orientation.LEFT)
            {
                rect.X -= 15;

                if (rect.Intersects(player.obj))
                {
                    game.Exit();
                }

                if (rect.X < -rect.Width) //Sair da tela pela esquerda ?
                {
                    isShotFired = false;
                    currentOrientation = Orientation.STOP;
                }
            }
        }
    }

    public void Draw(SpriteBatch screen)
    {
        if (currentOrientation == Orientation.RIGHT)
        {
            screen.Draw(rightBullet, rect, Color.White);
        }
        if (currentOrientation == Orientation.LEFT)
        {
            screen.Draw(leftBullet, rect, Color.White);
        }
    }
}
