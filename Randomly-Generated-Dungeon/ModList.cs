using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomly_Generated_Dungeon
{
    public partial class ModList : Form
    {
        List<DataEntities.ModFile> _ModFiles = new List<DataEntities.ModFile>();

        public ModList(List<DataEntities.ModFile> modFiles)
        {
            InitializeComponent();

            _ModFiles = modFiles;

            listBox1.DataSource = modFiles;
            listBox1.DisplayMember = "Name";

            listBox1_SelectedValueChanged(null, null);
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var mod = (DataEntities.ModFile)listBox1.SelectedItem;

                lblName.Text = mod.Name;
                lblVersion.Text = mod.Version;
                lblCreator.Text = mod.ModCreator;
                lblUrl.Text = mod.Url;
                richTextBox1.Text = mod.Description;
            }
            else
            {
                lblName.Text = "";
                lblVersion.Text = "";
                lblCreator.Text = "";
                lblUrl.Text = "";
                richTextBox1.Text = "";
            }
        }

        private void lblUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblUrl.LinkVisited = true;

            var uri = GetUri(lblUrl.Text);

            ProcessStartInfo sInfo = new ProcessStartInfo(uri.ToString());
            Process.Start(sInfo);
        }

        public Uri GetUri(string s)
        {
            return new UriBuilder(s).Uri;
        }

    }
}
