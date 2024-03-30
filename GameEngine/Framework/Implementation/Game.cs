namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Diagnostics;

    public class Game : Form, Interface.Game
    {
        private GameEngine.Framework.Interface.Screen _screen;
        private Audio _audio;
        private FileIO _fileIO;
        private Graphics _graphics;
        private Input _input;
        private GameEngine.Framework.Support.FormState _formState = new GameEngine.Framework.Support.FormState();
        private Timer _timer;
        private bool _inTimer = false;
        private long _lastTime = 0;
        private int _lastSecond = 0;
        private int _fpsCount = 0;
        public double fps { get; set; }
        private Stopwatch _watch = Stopwatch.StartNew();
        long _ticksPerMillisecond = TimeSpan.TicksPerMillisecond;

        public Game() : base()
        {           
            // set the basic properties for the form
            ClientSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this._formState.Maximize(this);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            StartPosition = FormStartPosition.CenterScreen;

            // instantiate class variables
            this._fileIO = new FileIO();
            this._audio = new Audio(this._fileIO);
            this._graphics = new Graphics(this);
            this._input = new Input(this);
            
            // kick off the first screen (this call will go outside of the GameEngine to the screen-based classes)
            this._screen = getStartScreen();

            // set up event handlers
            Resize += new EventHandler(this.form_Resize);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Closing);
            
            // System.Windows.Forms.Application.Idle += new EventHandler(this.gameLoop);

            // set up timer
            this._timer = new Timer();
            this._timer.Tick += new System.EventHandler(this.timer_Tick);
            this._timer.Interval = 1;
            this._timer.Start();
            return;
        }

        public virtual GameEngine.Framework.Interface.Screen getStartScreen() 
        {
            throw new NotImplementedException("You need to implement getStartScreen() into the class inheriting from Game");
        }

        public void setCursor(string filename)
        {
            this.Cursor = GameEngine.Framework.Support.NativeMethods.LoadCustomCursor(filename);
            return;
        }

        public void close()
        {
            Close();
            return;
        }

        public string title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public Interface.Input input
        {
            get
            {
                return this._input;
            }
        }

        public Interface.FileIO fileIO
        {
            get
            {
                return this._fileIO;
            }
        }

        public Interface.Graphics graphics
        {
            get
            {
                return this._graphics;
            }
        }

        public Interface.Audio audio
        {
            get
            {
                return this._audio;
            }
        }

        public Interface.Screen screen
        {
            get
            {
                return this._screen;
            }
            set
            {
                this._screen = value;
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            if (!this._inTimer)
            {
                int localSecond = DateTime.Now.Second;
                if (this._lastSecond != localSecond)
                {
                    this.fps = this._fpsCount;
                    this._fpsCount = 0;
                }
                this._fpsCount++;
                this._lastSecond = localSecond;
                this._inTimer = true;
                long elapsedTicks = this._watch.ElapsedTicks;
                long deltaTime = (elapsedTicks - this._lastTime) / _ticksPerMillisecond;
                this.screen.update(deltaTime);
                this.screen.draw(deltaTime);
                this._lastTime = elapsedTicks;
                this._inTimer = false;
                Refresh();
            }
            return;
        }

        // protected void gameLoop(object sender, EventArgs e)
        // {
        //    Stopwatch watch = Stopwatch.StartNew();
        //    long startTime = watch.ElapsedMilliseconds;
        //    while (GameEngine.Framework.Support.NativeMethods.AppStillIdle && !this._shouldStop)
        //    {
        //        float deltaTime = (watch.ElapsedMilliseconds - startTime) / 1000.0f;
        //        startTime = watch.ElapsedMilliseconds;

        //        this.screen.update(deltaTime);
        //        this.screen.draw(deltaTime);
        //        Refresh();
        //    }
        //    watch.Stop();
        //    return;
        // }

        protected void form_Resize(object sender, System.EventArgs e)
        {
            this._graphics.width = Width;
            this._graphics.height = Height;
            return;
        }

        protected void form_Closing(object sender, FormClosingEventArgs e)
        {
            // this._shouldStop = true;
            return;
        }

    }
}
