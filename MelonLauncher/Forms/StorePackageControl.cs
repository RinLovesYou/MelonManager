using MelonLauncher.PackageStore;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelonLauncher.Forms
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
