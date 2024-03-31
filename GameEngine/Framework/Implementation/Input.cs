namespace GameEngine.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public class Input : GameEngine.Framework.Interface.Input
    {
        public bool mouseLeftButtonDown { get; set; }
        public bool mouseMiddleButtonDown { get; set; }
        public bool mouseRightButtonDown { get; set; }
        public int mouseX { get; set; }
        public int mouseY { get; set; }
        private int _mouseMiddleButtonDelta = 0;
        private List<int> _keyEvents;
        
        public Input(Form form)
        {
            form.MouseEnter += new System.EventHandler(this.mouse_Enter);
            form.MouseLeave += new System.EventHandler(this.mouse_Leave);
            form.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mouse_Wheel);
            form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_Move);
            form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Down);
            form.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouse_Up);
            form.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_Down);
            form.KeyUp += new System.Windows.Forms.KeyEventHandler(this.key_Up);
            return;
        }

        public int mouseMiddleButtonDelta
        {
            get
            {
                return this._mouseMiddleButtonDelta;
            }
            set
            {
                if (value > 0)
                {
                    this._mouseMiddleButtonDelta++;
                }
                else
                {
                    this._mouseMiddleButtonDelta--;
                }
            }
        }

        public bool mouseInBounds(int x, int y, int width, int height)
        {
            if (this.mouseX > x && this.mouseX < x + width && this.mouseY > y && this.mouseY < y + height)
            {
			    return true;
		    }
		    else
            {
			    return false;	
		    }
        }

        public List<int> keyEvents
        {
            get 
            {
                if (this._keyEvents == null)
                {
                    this._keyEvents = new List<int>();
                }
                return this._keyEvents;
            }
        }

        public bool isKeyPressed(int keyCode)
        {
            if (this.keyEvents.Any())
            {
                if (this.keyEvents.Contains(keyCode))
                {
                    return true;
                }
            }
            return false;
        }

        public bool mouseInBounds(System.Drawing.Rectangle rect)
        {
            throw new NotImplementedException();
        }

        protected void mouse_Enter(object sender, System.EventArgs e)
        {
            // Cursor.Hide();
            return;
        }

        protected void mouse_Leave(object sender, System.EventArgs e)
        {
            // Cursor.Show();
            return;
        }

        protected void mouse_Wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.mouseMiddleButtonDelta = e.Delta;
            return;
        }

        protected void mouse_Move(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.mouseX = e.X;
            this.mouseY = e.Y;
            return;
        }

        protected void mouse_Down(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.mouseLeftButtonDown)
            {
                this.mouseLeftButtonDown = e.Button == System.Windows.Forms.MouseButtons.Left;
            }
            if (!this.mouseMiddleButtonDown)
            {
                this.mouseMiddleButtonDown = e.Button == System.Windows.Forms.MouseButtons.Middle;
            }
            if (!this.mouseRightButtonDown)
            {
                this.mouseRightButtonDown = e.Button == System.Windows.Forms.MouseButtons.Right;
            }
            return;
        }

        protected void mouse_Up(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.mouseLeftButtonDown)
            {
                this.mouseLeftButtonDown = false;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle && this.mouseMiddleButtonDown)
            {
                this.mouseMiddleButtonDown = false;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right && this.mouseRightButtonDown)
            {
                this.mouseRightButtonDown = false;
            }
            return;
        }

        protected void key_Down(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            byte[] keys = new byte[255];
            Support.NativeMethods.GetKeyboardState(keys);
            setKeyEvents(keys);
            return;
        }

        protected void key_Up(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            byte[] keys = new byte[255];
            Support.NativeMethods.GetKeyboardState(keystate: keys);
            setKeyEvents(keys: keys);
            return;
        }

        private void setKeyEvents(byte[] keys)
        {
            this._keyEvents = new List<int>();
            if (keys[(int)Keys.W] > 100)
            {
                this._keyEvents.Add((int)Keys.Up);
            }
            if (keys[(int)Keys.X] > 100)
            {
                this._keyEvents.Add((int)Keys.Down);
            }
            if (keys[(int)Keys.Q] > 100)
            {
                this._keyEvents.Add((int)Keys.Left);
            }
            if (keys[(int)Keys.E] > 100)
            {
                this._keyEvents.Add((int)Keys.Right);
            }
            if (keys[(int)Keys.Space] > 100)
            {
                this._keyEvents.Add((int)Keys.Space);
            }
            if (keys[(int)Keys.Escape] > 100)
            {
                this._keyEvents.Add((int)Keys.Escape);
            }
            if (keys[(int)Keys.D0] > 100)
            {
                this._keyEvents.Add((int)Keys.D0);
            }
            if (keys[(int)Keys.D1] > 100)
            {
                this._keyEvents.Add((int)Keys.D1);
            }
            if (keys[(int)Keys.D2] > 100)
            {
                this._keyEvents.Add((int)Keys.D2);
            }
            if (keys[(int)Keys.D3] > 100)
            {
                this._keyEvents.Add((int)Keys.D3);
            }
            if (keys[(int)Keys.D4] > 100)
            {
                this._keyEvents.Add((int)Keys.D4);
            }
            return;
        }
    }
}
