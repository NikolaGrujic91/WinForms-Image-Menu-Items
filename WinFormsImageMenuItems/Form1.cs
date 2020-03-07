using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinFormsImageMenuItems
{
    public partial class Form1 : Form
    {
        #region Fields

        private MainMenu mainMenu;
        private MenuItem fileMenuItem;
        private MenuItem fontMenuItem;

        #endregion

        #region Constructors

        public Form1()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.CreateMenu();
            this.Menu = this.mainMenu;
        }

        #endregion

        #region Methods

        private void CreateMenu()
        {
            var resources = new System.Resources.ResourceManager(typeof(Form1));

            this.imageList = new ImageList(this.components)
            {
                ColorDepth = ColorDepth.Depth8Bit,
                ImageSize = new Size(16, 16),
                ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream"),
                TransparentColor = Color.Transparent
            };
            this.imageList.Images.SetKeyName(0, string.Empty);
            this.imageList.Images.SetKeyName(1, string.Empty);
            this.imageList.Images.SetKeyName(2, string.Empty);

            this.fontMenuItem = new MenuItem { Text = "Fonts" };
            var fonts = new InstalledFontCollection();

            foreach (FontFamily family in fonts.Families)
            {
                try
                {
                    fontMenuItem.MenuItems.Add(new ImageMenuItem(family.Name, new Font(family, 10), null, Color.CornflowerBlue));
                }
                catch
                {
                    // Catch invalid fonts/styles and ignore them.
                }
            }

            this.fileMenuItem = new MenuItem();

            this.fileMenuItem.MenuItems.Add(this.fontMenuItem);
            this.fileMenuItem.MenuItems.Add(new ImageMenuItem("New", this.imageList.Images[0]));
            this.fileMenuItem.MenuItems.Add(new ImageMenuItem("Open", this.imageList.Images[1]));
            this.fileMenuItem.MenuItems.Add(new ImageMenuItem("Save", this.imageList.Images[2]));
            this.fileMenuItem.Text = "File";

            this.mainMenu = new MainMenu(this.components);
            this.mainMenu.MenuItems.Add(this.fileMenuItem);
        }

        #endregion
    }
}
