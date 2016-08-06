using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class Zoom_Popup : Form
    {
        public Zoom_Popup()
        {
            InitializeComponent();
        }

        public Zoom_Popup(PictureBox picBox)
        {
            InitializeComponent();

            int imgWidth;
            int imgHeight;
            try
            {
                this.pictureBoxZoomPopup.Image = picBox.Image;
                imgWidth = picBox.Image.Width;
                imgHeight = picBox.Image.Height;

                if (imgWidth < pictureBoxZoomPopup.Width)
                {
                    pictureBoxZoomPopup.Width = imgWidth;

                }

                if (imgHeight < pictureBoxZoomPopup.Height)
                {
                    pictureBoxZoomPopup.Height = imgHeight;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
