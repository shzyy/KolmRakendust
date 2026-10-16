using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    /// <summary>
    /// RAKENDUS 3: Sarnaste piltide leidmise mäng (Matching Game).
    /// Põhineb Microsofti õpetusel "Create a matching game WinForms app".
    /// Kogu kasutajaliides on loodud koodiga (Toolbox'i ei kasutata).
    ///
    /// Mängu käik:
    ///  - Väljakul on 16 ruutu (4x4), iga ikoon on kahes ruudus.
    ///  - Mängija klõpsab kahte ruutu - kui ikoonid klapivad, jäävad nad nähtavale,
    ///    kui ei klapi, peidetakse mõlemad 0,75 sekundi pärast (Timer).
    ///  - Kui kõik paarid on leitud, näidatakse võiduteadet (MessageBox).
    ///
    /// EDASIARENDUSE IDEED:
    ///  1. Lisa tegelikud pildid sümbolite asemel (PictureBox'id piltidega
    ///     kaustast "Images" Webdings ikoonide asemel).
    ///  2. Lisa taimer ja punktisüsteem - loe, mitme klõpsuga mängija võidab.
    ///  3. Lisa tasemed - 4x4, 6x6 ja 8x8 väljak, mida mängija saab valida.
    /// </summary>
    public class MatchingGameForm : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;

        // Juhuslike arvude generaator ikoonide segamiseks
        private Random random = new Random();

        // Iga ikoon on nimekirjas kaks korda - kokku 8 paari (16 ruutu)
        private List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // Esimene ja teine klõpsatud silt
        private Label firstClicked = null;
        private Label secondClicked = null;

        public MatchingGameForm()
        {
            LooKasutajaliides();
            AssignIconsToSquares();
        }

        private void LooKasutajaliides()
        {
            this.Text = "Matching Game";
            this.Size = new Size(550, 550);
            this.BackColor = Color.CornflowerBlue;

            // 4x4 ruudustik
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.BackColor = Color.CornflowerBlue;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.RowCount = 4;
            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }
            this.Controls.Add(tableLayoutPanel1);

            // Iga ruutu on Label, millel on peidetud Webdings ikoon
            for (int rida = 0; rida < 4; rida++)
            {
                for (int veerg = 0; veerg < 4; veerg++)
                {
                    Label silt = new Label();
                    silt.Dock = DockStyle.Fill;
                    silt.BackColor = Color.CornflowerBlue;
                    silt.ForeColor = Color.CornflowerBlue; // ikoon on "peidetud"
                    silt.Font = new Font("Webdings", 48F, FontStyle.Bold);
                    silt.TextAlign = ContentAlignment.MiddleCenter;
                    silt.Text = "c";
                    silt.Click += Label_Click;
                    tableLayoutPanel1.Controls.Add(silt, veerg, rida);
                }
            }

            // Taimer - peidab mitteklappivad ikoonid 0,75 sekundi pärast
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 750;
            timer1.Tick += Timer1_Tick;
        }

        /// <summary>
        /// Iga ikoon määratakse juhuslikku ruutu.
        /// </summary>
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        /// <summary>
        /// Iga ruudu klõps käsitletakse siin.
        /// </summary>
        private void Label_Click(object sender, EventArgs e)
        {
            // Kui taimer töötab, ignoreeritakse klõpse
            if (timer1.Enabled) return;

            Label clickedLabel = sender as Label;
            if (clickedLabel == null) return;

            // Kui ruut on juba nähtav (must), ignoreeritakse klõpsu
            if (clickedLabel.ForeColor == Color.Black) return;

            // Esimene klõps
            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            // Teine klõps
            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            // Kontrollime, kas mängija on võitnud
            CheckForWinner();

            // Kui ikoonid klapivad, jätame nad nähtavale ja nullime valiku
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
                return;
            }

            // Kui ikoonid EI klapi, käivitame taimeri, mis need peidab
            timer1.Start();
        }

        /// <summary>
        /// Peidab kaks mitteklappivat ikooni ja nullib valiku.
        /// </summary>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// Kontrollib kõiki ruute - kui kõik ikoonid on nähtavad, on mängija võitnud.
        /// </summary>
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return; // leidub veel peidetud ikoon
            }

            // Kõik paarid leitud!
            MessageBox.Show("Leidsid kõik sobivad ikoonid! Palju õnne!");
            Close();
        }
    }
}
