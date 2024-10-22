using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NaziHunt
{
    class MenuItem
    {
        Texture2D menu_item_off, menu_item_on;
        Rectangle obj_menu, obj_mouse;

        enum StatusMouse { DENTRO, FORA }

        StatusMouse statusMouse;
        bool ja_clicou;

        public MenuItem(Game g, String imagem_off, String imagem_on, int x, int y, int w, int h)
        {
            obj_menu = new Rectangle(x, y, w, h);
            obj_mouse = new Rectangle(0, 0, 0, 0);
            menu_item_off = g.Content.Load<Texture2D>(imagem_off);
            menu_item_on = g.Content.Load<Texture2D>(imagem_on);
            ja_clicou = false;

            statusMouse = StatusMouse.FORA;
        }

        public void CheckMouseOver(MouseState m)
        {

            obj_mouse.X = m.X;
            obj_mouse.Y = m.Y;
            obj_mouse.Width = 32;
            obj_mouse.Height = 32;

            if (obj_mouse.Intersects(obj_menu))
            {
                statusMouse = StatusMouse.DENTRO;
            }
            else
            {
                statusMouse = StatusMouse.FORA;
            }
        }

        public bool Clicou(MouseState m)
        {
            obj_mouse.X = m.X;
            obj_mouse.Y = m.Y;
            obj_mouse.Width = 50;
            obj_mouse.Height = 50;
            if (m.LeftButton == ButtonState.Pressed)
            {
                if (!ja_clicou)
                {
                    ja_clicou = true;
                    if (obj_mouse.Intersects(obj_menu))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ja_clicou = false;
                return false;
            }
        }
        public void DesenharNaTela(SpriteBatch tela)
        {
            if(statusMouse == StatusMouse.DENTRO)
              tela.Draw(menu_item_on, obj_menu, Color.White);
            else
              tela.Draw(menu_item_off, obj_menu, Color.White);


        }
    }
}
