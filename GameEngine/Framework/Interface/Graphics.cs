namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;

    public interface Graphics
    {
        Pixmap newPixmap(string fileName);
        void clear(Color color);
        void drawPoly(Point[] points, Brush brush, Pen pen);
        void drawPoly(Point[] points, Point offset, Brush brush, Pen pen);
        void drawPixel(int x, int y, int color);
        void drawLine(int x1, int y1, int x2, int y2, Pen color);
        void drawRect(int x, int y, int width, int height, Brush brush, Pen pen);
        void drawRect(Rectangle rectangle, Brush brush, Pen pen);
        void drawEllipse(int x, int y, int width, int height, Brush brush, Pen pen);
        void drawArc(Rectangle boundingRectangle, int startAngle, int sweepAngle, Brush brush, Pen pen);
        void drawEllipse(Rectangle rectangle, Brush brush, Pen pen);
        void drawEllipse(Rectangle rectangle, Point offset, Brush brush, Pen pen);
        void drawPixmap(Pixmap pixmap, int x, int y, int srcX, int srcY, int srcWidth, int srcHeight);
        void drawPixmap(Pixmap pixmap, int x, int y);
        void drawText(string text, int x, int y);
        void drawText(string text, int x, int y, Pixmap textImage, int textImageCharacterWidth, int textImageCharacterHeight);
        int width { get; set; }
        int height { get; set; }
    }
}
