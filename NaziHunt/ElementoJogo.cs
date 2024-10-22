using Microsoft.Xna.Framework;

namespace NaziHunt
{
    class ElementoJogo
    {

        public Rectangle obj;

        public void DeslocarObjeto(int left)
        {
            obj.X += left;
        }


        public void DeslocarObjetoX(int l)
        {
            obj.X += l;
        }

        public void DeslocarObjetoY(int t)
        {
            obj.Y += t;
        }



    }
}
