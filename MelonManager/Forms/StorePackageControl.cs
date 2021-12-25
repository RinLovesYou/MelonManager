using MelonManager.PackageStore;
using MetroFramework.Controls;
using System.Drawing;

namespace MelonManager.Forms
{
    public partial class StorePackageControl : MetroUserControl
    {
        public PackageInfo package;

        public StorePackageControl(PackageInfo pkg)
        {
            package = pkg;
            InitializeComponent();

            packageNameText.Text = pkg.name;
            packageNameText.Location = new Point((Size.Width - this.packageNameText.Size.Width) / 2, packageNameText.Location.Y);
        }
    }
}
