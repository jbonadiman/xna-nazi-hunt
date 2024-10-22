using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NaziHunt;

public class Bullet
{
    public enum Orientation
    {
        STOPPED, RIGHT, LEFT
    };
    public Orientation currentOrientation;
    public bool shotFired;
    private readonly Texture2D leftBullet;
    private readonly Texture2D rightBullet;
    private Rectangle rect;
    private int elapsedTime;

    public Bullet(Game g)
    {
        rightBullet = g.Content.Load<Texture2D>("images/bullet.png");
        leftBullet = Flip.FlipImage(g.Content.Load<Texture2D>("images/bullet.png"), false, true);
        currentOrientation = Orientation.STOPPED;
        shotFired = false;
        rect = new Rectangle(0, 0, 10, 10);
        elapsedTime = 0;
    }

    public void Shoot(Player p)
    {
        if ((p.currentState == Player.State.RIGHT_STOP)
        || (p.currentState == Player.State.RIGHT_SHOOT)
        || (p.currentState == Player.State.RIGHT_WALK))
        {
            if (!shotFired)
            {
                //Faz o posicionamento do tiro na tela
                rect.X = p.obj.X + p.obj.Width;
                rect.Y = p.obj.Y + 37;
                currentOrientation = Orientation.RIGHT;
                shotFired = true;
            }
        }
        else if ((p.currentState == Player.State.LEFT_STOP)
        || (p.currentState == Player.State.LEFT_SHOOT)
        || (p.currentState == Player.State.LEFT_WALK))
        {
            if (!shotFired)
            {
                //Faz o posicionamento do tiro na tela
                rect.X = p.obj.X - rect.Width;
                rect.Y = p.obj.Y + 37;
                currentOrientation = Orientation.LEFT;
                shotFired = true;
            }
        }
    }

    public void Update(GameTime gameTime, int time, List<GameObject> objects)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            if (currentOrientation == Orientation.RIGHT)
            {
                rect.X += 15;

                for (int x = 0; x < objects.Count; x++)
                {
                    if (objects[x] is Enemy)
                    {
                        if (rect.Intersects(objects[x].rect))
                        {
                            shotFired = false;
                            currentOrientation = Orientation.STOPPED;
                            objects.RemoveAt(x);
                            x--;
                        }
                    }
                }

                if (rect.X > 800) //800 é a largura da tela em pixels
                {
                    shotFired = false;
                    currentOrientation = Orientation.STOPPED;
                }
            }
            else if (currentOrientation == Orientation.LEFT)
            {
                rect.X -= 15;

                for (int x = 0; x < objects.Count; x++)
                {
                    if (objects[x] is Enemy)
                    {
                        if (rect.Intersects(objects[x].rect))
                        {
                            shotFired = false;
                            currentOrientation = Orientation.STOPPED;
                            objects.RemoveAt(x);
                            x--;
                        }
                    }
                }

                if (rect.X < -rect.Width) //Sair da tela pela esquerda ?
                {
                    shotFired = false;
                    currentOrientation = Orientation.STOPPED;
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
