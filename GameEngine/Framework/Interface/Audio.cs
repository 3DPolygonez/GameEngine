namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface Audio
    {
        Music newMusic(
            string fileName);
        Sound newSound(
            string fileName);
    }
}
