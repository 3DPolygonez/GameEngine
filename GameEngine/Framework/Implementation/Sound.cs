namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Media;
    using System.IO;

    public class Sound : Interface.Sound
    {
        private SoundPlayer _player;

        public Sound(Stream streamToplay)
        {
            this._player = new SoundPlayer();
            this._player.Stream = streamToplay;
            return;
        }

        void Interface.Sound.play(float volume)
        {
            this._player.Play();
            return;
        }

        void Interface.Sound.dispose()
        {
            this._player.Stream.Close();
            this._player.Stream.Dispose();
            this._player.Dispose();
            return;
        }
    }
}
