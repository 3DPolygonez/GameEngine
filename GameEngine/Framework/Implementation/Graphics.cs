namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;

    public class Graphics : GameEngine.Framework.Interface.Graphics
    {
        private BufferedGraphics _grafx;
        private int _width;
        private int _height;

        public Graphics(Form form)
        {
            this._width = form.Width;
            this._height = form.Height;
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this._width + 1, this._height + 1);
            this._grafx = context.Allocate(
                form.CreateGraphics(),
                new Rectangle(0, 0, this._width, this._height));
            this._grafx.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this._grafx.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            form.Paint += new System.Windows.Forms.PaintEventHandler(this.form_Paint);
            return;
        }

        protected void form_Paint(object sender, PaintEventArgs e)
        {
            this._grafx.Render(e.Graphics);
            return;
        }

        public Interface.Pixmap newPixmap(string fileName)
        {
            return new Pixmap(Image.FromFile(fileName));
        }

        public void drawPoly(Point[] points, Brush brush, Pen pen)
        {
            if (points.Length != 0)
            {
                if (brush != null)
                {
                    this._grafx.Graphics.FillPolygon(brush, points);
                }
                if (pen != null)
                {
                    this._grafx.Graphics.DrawPolygon(pen, points);
                }
            }
            return;
        }

        public void drawPoly(Point[] points, Point offset, Brush brush, Pen pen)
        {
            if (points.Length != 0)
            {
                var newPoints = new Point[points.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    newPoints[i].X += points[i].X + offset.X;
                    newPoints[i].Y += points[i].Y + offset.Y;
                }
                if (brush != null)
                {
                    this._grafx.Graphics.FillPolygon(brush, newPoints);
                }
                if (pen != null)
                {
                    this._grafx.Graphics.DrawPolygon(pen, newPoints);
                }
            }
            return;
        }

        public void drawEllipse(int x, int y, int width, int height, Brush brush, Pen pen)
        {
            if (brush != null)
            {
                this._grafx.Graphics.FillEllipse(brush, x, y, width, height);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawEllipse(pen, x, y, width, height);            
            }
            return;
        }

        public void drawEllipse(Rectangle rectangle, Brush brush, Pen pen)
        {
            if (brush != null)
            {
                this._grafx.Graphics.FillEllipse(brush, rectangle);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawEllipse(pen, rectangle);
            }
            return;
        }

        public void drawEllipse(Rectangle rectangle, Point offset, Brush brush, Pen pen)
        {
            rectangle.X += offset.X;
            rectangle.Y += offset.Y;
            if (brush != null)
            {
                this._grafx.Graphics.FillEllipse(brush, rectangle);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawEllipse(pen, rectangle);
            }
            return;
        }

        public void drawArc(Rectangle boundingRectangle, int startAngle, int sweepAngle, Brush brush, Pen pen)
        {
            if (brush != null)
            {
                this._grafx.Graphics.FillPie(brush, boundingRectangle, startAngle - 90, sweepAngle);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawArc(pen, boundingRectangle, startAngle - 90, sweepAngle);
            }
            return;
        }

        public void clear(Color color)
        {
            this._grafx.Graphics.Clear(color);
            return;
        }

        public void drawPixel(int x, int y, int color)
        {
            throw new NotImplementedException();
        }

        public void drawLine(int x1, int y1, int x2, int y2, Pen color)
        {
            this._grafx.Graphics.DrawLine(color, x1, y1, x2, y2);
        }

        public void drawRect(int x, int y, int width, int height, Brush brush, Pen pen)
        {
            if (brush != null)
            {
                this._grafx.Graphics.FillRectangle(brush, x, y, width, height);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawRectangle(pen, x, y, width, height);
            }
            return;
        }

        public void drawRect(Rectangle rectangle, Brush brush, Pen pen)
        {
            if (brush != null)
            {
                this._grafx.Graphics.FillRectangle(brush, rectangle);
            }
            if (pen != null)
            {
                this._grafx.Graphics.DrawRectangle(pen, rectangle);
            }
            return;
        }

        public void drawPixmap(Interface.Pixmap pixmap, int x, int y, int srcX, int srcY, int srcWidth, int srcHeight)
        {
            Rectangle destRect = new Rectangle(x, y, srcWidth, srcHeight);
            Rectangle srcRect = new Rectangle(srcX, srcY, srcWidth, srcHeight);
            this._grafx.Graphics.DrawImage(pixmap.bitmap, destRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
            return;
        }

        public void drawPixmap(Interface.Pixmap pixmap, int x, int y)
        {
            this._grafx.Graphics.DrawImage(pixmap.bitmap, x, y);
            return;
        }

        public void drawText(string text, int x, int y)
        {
            this._grafx.Graphics.DrawString(
                text,
                new Font("Arial", 8),
                Brushes.White,
                x,
                y);
        }

        public void drawText(string text, int x, int y, GameEngine.Framework.Interface.Pixmap textImage, int textImageCharacterWidth, int textImageCharacterHeight)
        {
            const string numbers = "01234567890";
            const string letters = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < text.Length; i++)
            {
                string s = text.Substring(i, 1).ToLower();
                int p = -1;
                int tP = x + i * textImageCharacterWidth;
                int letterAdjustment = 0;

                if (numbers.Contains(s))
                {
                    p = numbers.IndexOf(s);
                }
                else if (letters.Contains(s))
                {
                    p = letters.IndexOf(s);
                    letterAdjustment = 10 * textImageCharacterWidth;
                }

                if (p >= 0)
                {
                    drawPixmap(
                        textImage,
                        tP,
                        y,
                        p * textImageCharacterWidth + letterAdjustment,
                        0,
                        textImageCharacterWidth - 1,
                        textImageCharacterHeight);
                }
            }
            return;
        }

        public int width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public int height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }
    }
}
