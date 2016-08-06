using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class WaitImagePanel : Panel
    {
        private PictureBox picBoxAnimatedGIF = null;
        private Label label1 = null;

        public WaitImagePanel() : base()
        {
            this.picBoxAnimatedGIF = new PictureBox();
            this.picBoxAnimatedGIF.Name = "pictureBoxAnimatedGif";
            this.picBoxAnimatedGIF.Size = new Size(100, 100);
            this.picBoxAnimatedGIF.Location = new Point(50, 15);
            this.picBoxAnimatedGIF.SizeMode = PictureBoxSizeMode.Zoom;
            this.picBoxAnimatedGIF.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));

            // label
            this.label1 = new Label();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(75, 117);
            this.label1.Text = "Logobot is getting your logos...";
            this.label1.Size = new Size(150, 25);
            this.label1.TextAlign = ContentAlignment.TopLeft;
            this.label1.BackColor = Color.White;
            this.label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.label1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));


            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBoxAnimatedGIF);

            //InitializeComponent();
        }
    }
}
