using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomly_Generated_Dungeon
{
    public partial class Form1 : Form
    {
        DungeonHandler.GameHandler GameHandler = new DungeonHandler.GameHandler();

        public Form1()
        {
            InitializeComponent();

            ShowReturnResultInListBox(GameHandler.GetLocation());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = GameHandler.Input(textBox1.Text);

            richTextBox1.AppendText("You entered: " + textBox1.Text + Environment.NewLine, Color.SlateGray);

            ShowReturnResultInListBox(s);

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

            textBox1.Text = "";
            textBox1.Focus();
        }

        private void ShowReturnResultInListBox(DataEntities.ReturnResult results)
        {
            foreach (var result in results)
            {
                if (result.ShowInUI == true)
                {
                    ShowReturnResultInListBox(result);
                }
            }
        }

        private void ShowReturnResultInListBox(DataEntities.ReturnResult.Result result)
        {
            richTextBox1.AppendText(result.ResultMessage + Environment.NewLine, result.Color);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameHandler.SaveGame();
        }

        private void viewModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var modFiles = GameHandler.GetModFiles();

            ModList frm = new ModList(modFiles);
            frm.Parent = this.ParentForm;
            frm.TopMost = true;
            frm.Show();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
