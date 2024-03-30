namespace GameEngine.Framework.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    public class FormState
    {
        private FormWindowState winState;
        private FormBorderStyle brdStyle;
        private bool topMost;
        private Rectangle bounds;

        private bool IsMaximized = false;

        public void Maximize(Form targetForm)
        {
            if (!this.IsMaximized)
            {
                this.IsMaximized = true;
                this.Save(targetForm);
                targetForm.WindowState = FormWindowState.Maximized;
                targetForm.FormBorderStyle = FormBorderStyle.None;
                targetForm.TopMost = true;
                NativeMethods.SetWinFullScreen(targetForm.Handle);
            }
        }

        public void Save(Form targetForm)
        {
            this.winState = targetForm.WindowState;
            this.brdStyle = targetForm.FormBorderStyle;
            this.topMost = targetForm.TopMost;
            this.bounds = targetForm.Bounds;
        }

        public void Restore(Form targetForm)
        {
            targetForm.WindowState = this.winState;
            targetForm.FormBorderStyle = this.brdStyle;
            targetForm.TopMost = this.topMost;
            targetForm.Bounds = this.bounds;
            this.IsMaximized = false;
        }
    }
}
