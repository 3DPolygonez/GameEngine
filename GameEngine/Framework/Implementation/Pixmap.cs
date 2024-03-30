namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Pixmap : GameEngine.Framework.Interface.Pixmap
    {
        private Image _bitmap;

        public Pixmap(Image bitmap) : base()
        {
            this._bitmap = bitmap;
            return;
        }

        ~Pixmap()
        {
            this._bitmap.Dispose();
            this._bitmap = null;
            return;
        }

        public int width
        {
            get
            {
                return this._bitmap.Width;
            }
        }

        public int height
        {
            get
            {
                return this._bitmap.Height;
            }
        }

        public Image bitmap
        {
            get 
            {
                return this._bitmap;
            }
        }

    }
}
