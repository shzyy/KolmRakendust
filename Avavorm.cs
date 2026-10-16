using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    /// <summary>
    /// AVAVORM - harjutus "Kolm rakendust".
    ///
    /// Avavormil on kolm nuppu, mille igaüks avab ühe rakenduse:
    ///   1. Pildi vaatamise programm (Picture Viewer)
    ///   2. Matemaatiline äraarvamismäng (Math Quiz)
    ///   3. Sarnaste piltide leidmise mäng (Matching Game)
    ///
    /// Iga rakendus on eraldi klass (OOP printsiip - iga vorm vastutab
    /// ainult oma loogika eest) ja kogu kasutajaliides on tehtud koodiga.
    /// </summary>
    public class Avavorm : Form
    {
        private Label pealkiri;
        private Button btnPildiVaataja;
        private Button btnMatemaatika;
        private Button btnMang;
        private Button btnValju;

        public Avavorm()
        {
            this.Text = "Kolm rakendust - Avavorm";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            pealkiri = new Label();
            pealkiri.Text = "Vali rakendus:";
            pealkiri.Font = new Font("Arial", 18, FontStyle.Bold);
            pealkiri.AutoSize = true;
            pealkiri.Location = new Point(115, 30);
            this.Controls.Add(pealkiri);

            btnPildiVaataja = LooNupp("1. Pildi vaatamise programm", 100);
            btnPildiVaataja.Click += (s, e) => AvaVorm(new PictureViewerForm());

            btnMatemaatika = LooNupp("2. Matemaatiline äraarvamismäng", 160);
            btnMatemaatika.Click += (s, e) => AvaVorm(new MathQuizForm());

            btnMang = LooNupp("3. Sarnaste piltide leidmise mäng", 220);
            btnMang.Click += (s, e) => AvaVorm(new MatchingGameForm());

            btnValju = LooNupp("Välju", 290);
            btnValju.BackColor = Color.LightCoral;
            btnValju.Click += (s, e) => this.Close();
        }

        // Abimeetod nupu loomiseks (et koodi ei kordaks - OOP)
        private Button LooNupp(string tekst, int y)
        {
            Button nupp = new Button();
            nupp.Text = tekst;
            nupp.Size = new Size(290, 45);
            nupp.Location = new Point(70, y);
            nupp.Font = new Font("Arial", 11);
            this.Controls.Add(nupp);
            return nupp;
        }

        // Abimeetod vormi avamiseks
        private void AvaVorm(Form vorm)
        {
            vorm.StartPosition = FormStartPosition.CenterScreen;
            vorm.ShowDialog();
        }
    }
}
