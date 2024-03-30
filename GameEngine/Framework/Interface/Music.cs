namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface Music
    {
        void play();
        void stop();
        void pause();
        bool looping { get; set; }
        float volume { get; set; }
        bool isPlaying { get; }
        void dispose();
    }
}
