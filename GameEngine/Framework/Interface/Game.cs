namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface Game
    {
        string title { get; set; }
        Input input { get; }
        FileIO fileIO { get; }
        Graphics graphics { get; }
        Audio audio { get; }
        Screen screen { get; set; }
        void close();
        void setCursor(string filename);
        double fps { get; set; }
    }
}
