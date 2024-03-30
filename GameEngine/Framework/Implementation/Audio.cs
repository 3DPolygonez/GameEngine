namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Media;

    public class Audio : Interface.Audio
    {
        private Interface.FileIO _fileIO;

        public Audio(Interface.FileIO fileIO)
        {
            this._fileIO = fileIO;
            return;
        }

        Interface.Music Interface.Audio.newMusic(string fileName)
        {
            return new Music(
                fileName);
        }

        Interface.Sound Interface.Audio.newSound(string fileName)
        {
            return new Sound(
                this._fileIO.readFile(fileName));
        }
    }
}
