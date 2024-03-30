namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public interface Pixmap
    {
        int width { get; }
        int height { get; }
        Image bitmap { get; }
    }
}
