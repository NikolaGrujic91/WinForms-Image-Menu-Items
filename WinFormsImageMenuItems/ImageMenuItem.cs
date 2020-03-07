using System.Drawing;
using System.Windows.Forms;

namespace WinFormsImageMenuItems
{
    internal class ImageMenuItem : MenuItem
    {
        #region Constructors

        public ImageMenuItem(string text, Font font, Image image, Color foreColor) : base(text)
        {
            this.Font = font;
            this.Image = image;
            this.ForeColor = foreColor;
            this.OwnerDraw = true;
        }

        public ImageMenuItem(string text, Image image) : base(text)
        {
            // Choose a suitable default color and font.
            this.Font = new Font("Tahoma", 8);
            this.Image = image;
            this.ForeColor = SystemColors.MenuText;
            this.OwnerDraw = true;
        }

        #endregion

        #region Properties

        private Font Font { get; set; }

        private Image Image { get; set; }

        private Color ForeColor { get; set; }

        #endregion

        #region Overrides

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);

            // Measure size needed to display text.
            e.ItemHeight = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 5;
            e.ItemWidth = (int)e.Graphics.MeasureString(this.Text, this.Font).Width + 30;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            // Determine whether disabled text is needed.
            Color textColor;

            if (this.Enabled)
            {
                e.DrawBackground();
                textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? 
                            SystemColors.HighlightText : 
                            this.ForeColor;
            }
            else
            {
                textColor = SystemColors.GrayText;
            }

            // Draw the image.
            if (this.Image != null)
            {
                if (this.Enabled)
                {
                    e.Graphics.DrawImage(this.Image, e.Bounds.Left + 3, e.Bounds.Top + 2);
                }
                else
                {
                    ControlPaint.DrawImageDisabled(e.Graphics, this.Image, e.Bounds.Left + 3, e.Bounds.Top + 2, SystemColors.Menu);
                }
            }

            // Draw the text with the supplied colors and in the set region.
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(textColor), e.Bounds.Left + 25, e.Bounds.Top + 3);
        }

        #endregion
    }
}
