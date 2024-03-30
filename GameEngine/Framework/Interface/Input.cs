namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public interface Input
    {
        bool mouseLeftButtonDown { get; set; }
        bool mouseMiddleButtonDown { get; set; }
        bool mouseRightButtonDown { get; set; }
        int mouseX { get; set; }
        int mouseY { get; set; }
        int mouseMiddleButtonDelta { get; set; }
        bool mouseInBounds(int x, int y, int width, int height);
        bool mouseInBounds(Rectangle rect);
        bool isKeyPressed(int keyCode);
        List<int> keyEvents { get; }
    }
}
