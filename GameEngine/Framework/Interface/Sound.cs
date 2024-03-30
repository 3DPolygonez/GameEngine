namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface Sound
    {
        void play(float volume);
        void dispose();
    }
}
