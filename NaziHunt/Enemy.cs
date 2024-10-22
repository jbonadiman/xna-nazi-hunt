using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class Enemy : GameObject
{
    public enum EnemyState
    {
        RIGHT_WALK, LEFT_WALK, LEFT_SHOOT, RIGHT_SHOOT
    }
    public EnemyState state;

    private readonly AnimationSprites rightWalk;
    private readonly AnimationSprites leftWalk;
    private readonly Texture2D leftShoot;
    private readonly Texture2D rightShoot;
    int shootElapsedTime;
    int elapsedTime;
    readonly Game game;

    public void Shoot(GameTime gameTime)
    {
        //Atira a cada 3 segundos
        shootElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
        if (shootElapsedTime >= 3000)
        {
            if (state == EnemyState.RIGHT_WALK)
            {
                state = EnemyState.RIGHT_SHOOT;
            }
            else if (state == EnemyState.LEFT_WALK)
            {
                state = EnemyState.LEFT_SHOOT;
            }

            shootElapsedTime = 0;
        }
    }

    public Enemy(Game g, int l, int t, int w, int h)
    {
        game = g;

        rightShoot = Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_shoot.png"), false, true);
        leftShoot = g.Content.Load<Texture2D>("images/enemy_shoot.png");

        rightWalk = new AnimationSprites();
        //       AndandoDireita.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/inimigo_andando_1.png"), false, true));
        rightWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_walk_2.png"), false, true));
        rightWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_walk_3.png"), false, true));
        rightWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_walk_4.png"), false, true));
        rightWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_walk_5.png"), false, true));
        rightWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/enemy_walk_6.png"), false, true));
        rightWalk.StartAnimation(100);

        leftWalk = new AnimationSprites();
        //     AndandoEsquerda.Add(g.Content.Load<Texture2D>("images/enemy_walk_1.png"));
        leftWalk.Add(g.Content.Load<Texture2D>("images/enemy_walk_2.png"));
        leftWalk.Add(g.Content.Load<Texture2D>("images/enemy_walk_3.png"));
        leftWalk.Add(g.Content.Load<Texture2D>("images/enemy_walk_4.png"));
        leftWalk.Add(g.Content.Load<Texture2D>("images/enemy_walk_5.png"));
        leftWalk.Add(g.Content.Load<Texture2D>("images/enemy_walk_6.png"));
        leftWalk.StartAnimation(100);

        rect = new Rectangle(l, t, w, h);

        state = EnemyState.LEFT_WALK;

        elapsedTime = 0;
        shootElapsedTime = 0;
    }

    public void Processar(GameTime gameTime, int time, List<EnemyBullet> bullets)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        Shoot(gameTime);

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            if ((state == EnemyState.RIGHT_SHOOT) || (state == EnemyState.LEFT_SHOOT))
            {
                EnemyBullet enemyBullet = new EnemyBullet(game);
                bullets.Add(enemyBullet);
                enemyBullet.Shoot(this);

                if (state == EnemyState.RIGHT_SHOOT)
                {
                    state = EnemyState.RIGHT_WALK;
                }
                else if (state == EnemyState.LEFT_SHOOT)
                {
                    state = EnemyState.LEFT_WALK;
                }
            }
            else if (state == EnemyState.RIGHT_WALK)
            {
                rect.X += 5;
            }
            else if (state == EnemyState.LEFT_WALK)
            {
                rect.X -= 5;
            }
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch screen)
    {
        if (state == EnemyState.RIGHT_WALK)
        {
            screen.Draw(rightWalk.GetImage(gameTime), rect, Color.White);
        }
        else if (state == EnemyState.LEFT_WALK)
        {
            screen.Draw(leftWalk.GetImage(gameTime), rect, Color.White);
        }
        else if (state == EnemyState.RIGHT_SHOOT)
        {
            screen.Draw(rightShoot, rect, Color.White);
        }
        else if (state == EnemyState.LEFT_SHOOT)
        {
            screen.Draw(leftShoot, rect, Color.White);
        }
    }
}
