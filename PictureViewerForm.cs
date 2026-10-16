using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    /// <summary>
    /// Rakendus 1: Piltide vaatamise programm.
    /// Kasutajaliides on loodud täielikult koodi abil.
    /// </summary>
    public class PictureViewerForm : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button showButton;
        private Button clearButton;
        private Button backgroundButton;
        private Button closeButton;
        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;

        public PictureViewerForm()
        {
            LooKasutajaliides();
        }

        private void LooKasutajaliides()
        {
            // Akna seaded
            this.Text = "Piltide vaataja";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tabeli loomine
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 2;

            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 85F));

            tableLayoutPanel1.RowStyles.Add(
                new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(
                new RowStyle(SizeType.Percent, 10F));

            this.Controls.Add(tableLayoutPanel1);

            // Pildi kuvamise ala
            pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;

            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.SetColumnSpan(pictureBox1, 2);

            // Pildi venitamise märkeruut
            checkBox1 = new CheckBox();
            checkBox1.Text = "Venita pilt";
            checkBox1.AutoSize = true;
            checkBox1.Dock = DockStyle.Fill;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;

            tableLayoutPanel1.Controls.Add(checkBox1, 0, 1);

            // Nuppude paigutus
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection =
                FlowDirection.RightToLeft;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;

            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 1);

            // Nuppude loomine
            closeButton = LooNupp("Sulge");
            closeButton.Click += CloseButton_Click;

            clearButton = LooNupp("Eemalda pilt");
            clearButton.Click += ClearButton_Click;

            backgroundButton = LooNupp("Muuda taustavärvi");
            backgroundButton.Click += BackgroundButton_Click;

            showButton = LooNupp("Ava pilt");
            showButton.Click += ShowButton_Click;

            // Pildifaili avamise dialoog
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter =
                "JPEG-pildid (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "PNG-pildid (*.png)|*.png|" +
                "BMP-pildid (*.bmp)|*.bmp|" +
                "Kõik failid (*.*)|*.*";

            openFileDialog1.Title = "Vali pildifail";
            openFileDialog1.RestoreDirectory = true;

            // Taustavärvi valimise dialoog
            colorDialog1 = new ColorDialog();
        }

        // Loob uue nupu
        private Button LooNupp(string tekst)
        {
            Button nupp = new Button();
            nupp.Text = tekst;
            nupp.AutoSize = true;

            flowLayoutPanel1.Controls.Add(nupp);

            return nupp;
        }

        // Avab pildifaili
        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Pilti ei õnnestunud avada. " +
                        "Kontrolli, kas fail on kehtiv pildifail.",
                        "Viga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Eemaldab pildi
        private void ClearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        // Muudab taustavärvi
        private void BackgroundButton_Click(
            object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        // Sulgeb programmi akna
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Lülitab pildi venitamise sisse või välja
        private void CheckBox1_CheckedChanged(
            object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.SizeMode =
                    PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode =
                    PictureBoxSizeMode.Normal;
            }
        }
    }
}
