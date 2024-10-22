using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NaziHunt;

public class Player
{
    public enum State
    {
        LEFT_STOP,
        RIGHT_STOP,
        LEFT_WALK,
        RIGHT_WALK,
        LEFT_JUMP,
        RIGHT_JUMP,
        RIGHT_SHOOT,
        LEFT_SHOOT
    }

    public enum GroundState { ON_GROUND, FALLING, JUMPING };
    public GroundState currentGroundState;
    private AnimationSprites leftWalk, rightWalk, leftJump, rightJump;
    public State currentState;
    private Texture2D baseSprite, rightStopSprite, leftStopSprite, rightShootSprite, leftShootSprite;
    public Rectangle obj;
    private int jumpCount;
    private int elapsedTime;
    private readonly Game game;

    public Player(Game g, int l, int t, int w, int h)
    {
        game = g;
        obj = new Rectangle(l, t, w, h);
        elapsedTime = 0;
        rightStopSprite = g.Content.Load<Texture2D>("images/player_jump_1.png");
        leftStopSprite = Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_1.png"), false, true);

        rightWalk = new AnimationSprites();
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_1.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_2.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_3.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_4.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_5.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_6.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_7.png"));
        rightWalk.Add(g.Content.Load<Texture2D>("images/player_walk_8.png"));

        leftWalk = new AnimationSprites();
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_1.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_2.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_3.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_4.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_5.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_6.png"), false, true));
        leftWalk.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_walk_8.png"), false, true));

        rightJump = new AnimationSprites();
        rightJump.Add(g.Content.Load<Texture2D>("images/player_jump_2.png"));
        //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 3.png"));
        //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 2.png"));
        //aPulandoDireita.Add(g.Content.Load<Texture2D>("images/megamanx_pulando 1.png"));

        leftJump = new AnimationSprites();
        leftJump.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_2.png"), false, true));
        //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 3.png"), false, true));
        //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 2.png"), false, true));
        //aPulandoEsquerda.Add(Flip.FlipImage(g.Content.Load<Texture2D>("images/megamanx_pulando 1.png"), false, true));

        rightShootSprite = g.Content.Load<Texture2D>("images/player_jump_1.png");
        leftShootSprite = Flip.FlipImage(g.Content.Load<Texture2D>("images/player_jump_1.png"), false, true);
        currentGroundState = GroundState.ON_GROUND;
        currentState = State.RIGHT_STOP;
        baseSprite = rightStopSprite;

        elapsedTime = 0;
    }

    public void MoveToTheRight()
    {
        if ((currentState != State.RIGHT_WALK)
        && (currentState != State.RIGHT_JUMP)
        && (currentState != State.LEFT_JUMP))
        {
            currentState = State.RIGHT_WALK;
            rightWalk.StartAnimation(300);
        }
        // obj.X += 10;
    }

    public void MoveToTheLeft()
    {
        if ((currentState != State.LEFT_WALK)
        && (currentState != State.RIGHT_JUMP)
        && (currentState != State.LEFT_JUMP))
        {
            currentState = State.LEFT_WALK;
            leftWalk.StartAnimation(300);
        }
        //  obj.X -= 10;
    }
    public void Jump()
    {
        if ((currentState != State.RIGHT_JUMP)
        && (currentState != State.LEFT_JUMP))
        {
            jumpCount = 0;
            currentGroundState = GroundState.JUMPING;

            if (currentState == State.RIGHT_STOP)
            {
                currentState = State.RIGHT_JUMP;
                rightJump.StartAnimation(100);
            }
            else
            {
                currentState = State.LEFT_JUMP;
                leftJump.StartAnimation(100);
            }
        }
    }

    public void Shoot()
    {
        if ((currentState == State.RIGHT_STOP))
        {
            currentState = State.RIGHT_SHOOT;
        }
        else if ((currentState == State.LEFT_STOP))
        {
            currentState = State.LEFT_SHOOT;
        }
    }

    public void Stop()
    {
        if (currentState == State.RIGHT_WALK)
        {
            currentState = State.RIGHT_STOP;
            baseSprite = rightStopSprite;
            //  aCorrendoDireita.StopAnimation();
        }
        else if (currentState == State.LEFT_WALK)
        {
            currentState = State.LEFT_STOP;
            baseSprite = leftStopSprite;
            //    aCorrendoEsquerda.StopAnimation();
        }
    }

    public void Draw(SpriteBatch screen)
    {
        GameTime currentTime = game.CurrentTime;
        if ((currentState == State.RIGHT_STOP) || (currentState == State.LEFT_STOP))
        {
            screen.Draw(baseSprite, obj, Color.White);
        }
        else if (currentState == State.RIGHT_WALK)
        {
            screen.Draw(rightWalk.GetImage(currentTime), obj, Color.White);
        }
        else if (currentState == State.LEFT_WALK)
        {
            screen.Draw(leftWalk.GetImage(currentTime), obj, Color.White);
        }

        if (currentState == State.LEFT_JUMP)
        {
            screen.Draw(leftJump.GetImage(currentTime), obj, Color.White);
        }
        else if (currentState == State.RIGHT_JUMP)
        {
            screen.Draw(rightJump.GetImage(currentTime), obj, Color.White);
        }
        else if (currentState == State.RIGHT_SHOOT)
        {
            screen.Draw(rightShootSprite, obj, Color.White);
        }
        else if (currentState == State.LEFT_SHOOT)
        {
            screen.Draw(leftShootSprite, obj, Color.White);
        }
    }

    public void Update(GameTime gameTime, int time, List<GameObject> gameObjects)
    {
        elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

        if (elapsedTime >= time)
        {
            elapsedTime = 0;
            if (((currentState == State.LEFT_JUMP) || (currentState == State.RIGHT_JUMP))
            && (currentGroundState == GroundState.JUMPING))
            {
                jumpCount++;
                //aqui o deslocamento y sera do personagem
                if (jumpCount <= 8)
                {
                    obj.Y -= 25;
                }
                else if (jumpCount == 9)
                {
                    currentGroundState = GroundState.FALLING;
                }
            }

            else if (((currentState == State.LEFT_JUMP) || (currentState == State.RIGHT_JUMP))
            && (currentGroundState == GroundState.FALLING))
            {
                obj.Y += 25;

                for (int x = 0; x < gameObjects.Count; x++)
                {
                    if ((obj.Intersects(gameObjects[x].rect)) &&
                      ((obj.Y + obj.Height) <= gameObjects[x].rect.Y + 30))
                    {
                        obj.Y = gameObjects[x].rect.Y - obj.Height;

                        if (currentState == State.LEFT_JUMP)
                        {
                            currentState = State.LEFT_STOP;
                        }
                        else if (currentState == State.RIGHT_JUMP)
                        {
                            currentState = State.RIGHT_STOP;
                        }

                        currentGroundState = GroundState.ON_GROUND;
                    }
                }
            }

            else if (((currentState == State.RIGHT_WALK) || (currentState == State.LEFT_WALK))
            && (currentGroundState == GroundState.ON_GROUND))
            {
                //Verifico se ele pode estar fora de algum sólido
                bool canFall = true;
                for (int x = 0; x < gameObjects.Count; x++)
                {
                    if ((gameObjects[x].rect.Y == (obj.Y + obj.Height)) && (((obj.X + obj.Width) >= gameObjects[x].rect.X) && (obj.X <= (gameObjects[x].rect.X + gameObjects[x].rect.Width))))
                    {
                        canFall = false;
                        break;
                    }
                }
                if (canFall)
                {
                    if (currentState == State.RIGHT_WALK)
                    {
                        currentState = State.RIGHT_JUMP;
                    }
                    else if (currentState == State.LEFT_WALK)
                    {
                        currentState = State.LEFT_JUMP;
                    }

                    currentGroundState = GroundState.FALLING;
                }
            }
        }
    }
}
