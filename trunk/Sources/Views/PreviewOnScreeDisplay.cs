using ScreenCapture.Processors;
using ScreenCapture.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenCapture.Views
{
    public partial class PreviewOnScreeDisplay : Form
    {

        CaptureKeyboard capture;
        OptionViewModel viewModel;

        public PreviewOnScreeDisplay(OptionViewModel viewModel)
            : this()
        {
            this.viewModel = viewModel;
        }

        public PreviewOnScreeDisplay()
        {
            InitializeComponent();
        }

        private void PreviewOnScreeDisplay_Load(object sender, EventArgs e)
        {
            capture = new CaptureKeyboard()
            {
                Preview = true
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            capture.Font = new Font(viewModel.FontFamily, viewModel.FontSize);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (capture != null)
                capture.Draw(e.Graphics);
        }

        private void PreviewOnScreeDisplay_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
