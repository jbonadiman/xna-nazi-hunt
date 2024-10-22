using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt;

public class MenuItem
{
    private readonly Texture2D menuItemOff;
    private readonly Texture2D menuItemOn;
    private StatusMouse currentMouseStatus;
    Rectangle menuRect, mouseRect;
    enum StatusMouse { OUTSIDE, INSIDE }
    bool wasClicked;

    public MenuItem(Game g, string pathSpriteOff, string pathSpriteOn, int x, int y, int w, int h)
    {
        menuRect = new Rectangle(x, y, w, h);
        mouseRect = new Rectangle(0, 0, 0, 0);
        menuItemOff = g.Content.Load<Texture2D>(pathSpriteOff);
        menuItemOn = g.Content.Load<Texture2D>(pathSpriteOn);
        wasClicked = false;

        currentMouseStatus = StatusMouse.INSIDE;
    }

    public void CheckMouseOver(MouseState m)
    {
        mouseRect.X = m.X;
        mouseRect.Y = m.Y;
        mouseRect.Width = 32;
        mouseRect.Height = 32;

        if (mouseRect.Intersects(menuRect))
        {
            currentMouseStatus = StatusMouse.OUTSIDE;
        }
        else
        {
            currentMouseStatus = StatusMouse.INSIDE;
        }
    }

    public bool WasClicked(MouseState m)
    {
        mouseRect.X = m.X;
        mouseRect.Y = m.Y;
        mouseRect.Width = 50;
        mouseRect.Height = 50;
        if (m.LeftButton == ButtonState.Pressed)
        {
            if (!wasClicked)
            {
                wasClicked = true;
                if (mouseRect.Intersects(menuRect))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            wasClicked = false;
            return false;
        }
    }

    public void Draw(SpriteBatch screen)
    {
        if (currentMouseStatus == StatusMouse.OUTSIDE)
        {
            screen.Draw(menuItemOn, menuRect, Color.White);
        }
        else
        {
            screen.Draw(menuItemOff, menuRect, Color.White);
        }
    }
}
