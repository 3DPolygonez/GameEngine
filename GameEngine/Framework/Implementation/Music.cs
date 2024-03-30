namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    public class Music : GameEngine.Framework.Interface.Music
    {
        private string _command;
        private bool _isOpen;
        private bool _isPlaying;
        private bool _looping;
        private float _volume;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        private string _fileToPlay;

        public Music(string fileToplay)
        {
            this._fileToPlay = fileToplay;
            return;
        }

        void Interface.Music.play()
        {
            if (!this._isPlaying)
            {
                if (!this._isOpen)
                {
                    this._command = "open \"" + this._fileToPlay + "\" type mpegvideo alias MediaFile";
                    long ret = mciSendString(this._command, null, 0, IntPtr.Zero);
                    this._isOpen = true;
                }
                if (this._isOpen)
                {
                    this._isPlaying = true;
                    this._command = "play MediaFile";
                    if (this._looping)
                        this._command += " REPEAT";
                    mciSendString(this._command, null, 0, IntPtr.Zero);
                }
            }
            return;
        }

        bool Interface.Music.looping
        {
            get
            {
                return this._looping;
            }
            set
            {
                this._looping = value;
            }
        }

        float Interface.Music.volume
        {
            get
            {
                return this._volume;
            }
            set
            {
                this._volume = value;
            }
        }

        bool Interface.Music.isPlaying
        {
            get 
            {
                return this._isPlaying;
            }
        }

        void Interface.Music.stop()
        {
            this._command = "close MediaFile";
            mciSendString(this._command, null, 0, IntPtr.Zero);
            this._isPlaying = false;
            this._isOpen = false;
            return;
        }

        void Interface.Music.pause()
        {
            throw new NotImplementedException();
        }

        void Interface.Music.dispose()
        {
            return;
        }
    }
}
