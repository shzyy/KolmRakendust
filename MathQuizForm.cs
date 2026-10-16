using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    /// <summary>
    /// RAKENDUS 2: Matemaatiline äraarvamismäng (Math Quiz).
    /// Põhineb Microsofti õpetusel "Create a math quiz WinForms app".
    /// Kogu kasutajaliides on loodud koodiga (Toolbox'i ei kasutata).
    ///
    /// Mängu käik:
    ///  - Nupp "Start the quiz" genereerib juhuslikud arvud (+, -, ×, ÷)
    ///    ja käivitab 30-sekundilise taimeri.
    ///  - Mängija sisestab vastused NumericUpDown kastidesse.
    ///  - Aja lõppedes (või kui kõik vastused on õiged varem) kontrollitakse
    ///    vastuseid ja näidatakse tulemus MessageBox'iga.
    ///
    /// EDASIARENDUSE IDEED:
    ///  1. Lisa raskusastme valik (kerge: arvud 1-10, keskmine: 1-50, raske: 1-100).
    ///  2. Lisa punktiarvestus - iga õige vastus annab punkte ja tulemused salvestatakse.
    ///  3. Lisa juhuslikud tehted - iga vooruga võib tehete märgid juhuslikult muutuda.
    /// </summary>
    public class MathQuizForm : Form
    {
        // Sildid (+ tehe)
        private Label plusLeftLabel, plusRightLabel;
        private NumericUpDown sum;
        // Sildid (- tehe)
        private Label minusLeftLabel, minusRightLabel;
        private NumericUpDown difference;
        // Sildid (× tehe)
        private Label timesLeftLabel, timesRightLabel;
        private NumericUpDown product;
        // Sildid (÷ tehe)
        private Label dividedLeftLabel, dividedRightLabel;
        private NumericUpDown quotient;

        private Label timeLabelText;  // "Time Left"
        private Label timeLabel;      // järelejäänud aeg
        private Button startButton;
        private System.Windows.Forms.Timer timer1;

        // Juhuslike arvude generaator
        private Random randomizer = new Random();

        // Tehete väärtused
        private int addend1, addend2;
        private int minuend, subtrahend;
        private int multiplicand, multiplier;
        private int dividend, divisor;

        // Järelejäänud aeg sekundites
        private int timeLeft;

        public MathQuizForm()
        {
            LooKasutajaliides();
        }

        private void LooKasutajaliides()
        {
            this.Text = "Matemaatika viktoriini";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Font = new Font("Microsoft Sans Serif", 15.75F);

            // --- Aja rida ---
            timeLabel = new Label();
            timeLabel.Text = "";
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Size = new Size(200, 30);
            timeLabel.Location = new Point(272, 9);
            timeLabel.Font = new Font("Microsoft Sans Serif", 15.75F);
            this.Controls.Add(timeLabel);

            timeLabelText = new Label();
            timeLabelText.Text = "Time Left";
            timeLabelText.AutoSize = true;
            timeLabelText.Location = new Point(165, 10);
            this.Controls.Add(timeLabelText);

            // --- Nelja tehte read (vt. ekraanipilti) ---
            int reaY = 75, vahe = 60;

            LooTehteRida(reaY, out plusLeftLabel, "+", out plusRightLabel, out sum);
            LooTehteRida(reaY + vahe, out minusLeftLabel, "-", out minusRightLabel, out difference);
            LooTehteRida(reaY + 2 * vahe, out timesLeftLabel, "×", out timesRightLabel, out product);
            LooTehteRida(reaY + 3 * vahe, out dividedLeftLabel, "÷", out dividedRightLabel, out quotient);

            // --- Start nupp ---
            startButton = new Button();
            startButton.Text = "Alusta viktoriini";
            startButton.AutoSize = true;
            startButton.Font = new Font("Microsoft Sans Serif", 14F);
            startButton.Location = new Point(170, 310);
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);

            // --- Taimer (1 sekund) ---
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
        }

        // Abimeetod: loob ühe tehte rea (arv märk arv = vastus)
        private void LooTehteRida(int y, out Label vasak, string mark,
                                  out Label parem, out NumericUpDown vastus)
        {
            vasak = new Label();
            vasak.Text = "?";
            vasak.AutoSize = true;
            vasak.Location = new Point(50, y);
            this.Controls.Add(vasak);

            Label markLabel = new Label();
            markLabel.Text = mark;
            markLabel.AutoSize = true;
            markLabel.Location = new Point(128, y);
            this.Controls.Add(markLabel);

            parem = new Label();
            parem.Text = "?";
            parem.AutoSize = true;
            parem.Location = new Point(198, y);
            this.Controls.Add(parem);

            Label vordub = new Label();
            vordub.Text = "=";
            vordub.AutoSize = true;
            vordub.Location = new Point(268, y);
            this.Controls.Add(vordub);

            vastus = new NumericUpDown();
            vastus.Size = new Size(100, 35);
            vastus.Location = new Point(328, y - 4);
            vastus.Maximum = 1000;
            vastus.Minimum = -1000;
            vastus.Enter += Vastus_Enter;  // valib kogu arvu, kui kasti klikitakse
            this.Controls.Add(vastus);
        }

        /// <summary>
        /// Käivitab viktoriini: täidab kõik tehted juhuslike arvudega
        /// ja käivitab taimeri.
        /// </summary>
        public void StartTheQuiz()
        {
            // Liitmine
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Lahutamine
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Korrutamine
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Jagamine
            divisor = randomizer.Next(2, 11);
            int ajutineQuotient = randomizer.Next(2, 11);
            dividend = divisor * ajutineQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Taimer
            timeLeft = 30;
            timeLabel.Text = "30 secondid";
            timer1.Start();
            startButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
        }

        /// <summary>
        /// Kontrollib, kas kõik vastused on õiged.
        /// </summary>
        private bool CheckTheAnswer()
        {
            return (addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Kõik õige enne aja lõppu - mängija võitis!
                timer1.Stop();
                MessageBox.Show("Vastasid kõikidele küsimustele õigesti! Palju õnne!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Aeg tiksub
                timeLeft--;
                timeLabel.Text = timeLeft + " secondid";

                // Viimased 5 sekundit - aeg muutub punaseks
                if (timeLeft <= 5)
                    timeLabel.BackColor = Color.Red;
            }
            else
            {
                // Aeg sai otsa - näitame õigeid vastuseid
                timer1.Stop();
                timeLabel.Text = "Aeg on läbi!";
                MessageBox.Show("Sa ei jõudnud õigeks ajaks lõpetada.","Vabandust!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                timeLabel.BackColor = SystemColors.Control;
                startButton.Enabled = true;
            }
        }

        // Kui mängija klikib vastusekasti, valitakse kogu number,
        // et oleks lihtsam uus vastus sisestada.
        private void Vastus_Enter(object sender, EventArgs e)
        {
            NumericUpDown vastuseKast = sender as NumericUpDown;
            if (vastuseKast != null)
            {
                int pikkus = vastuseKast.Value.ToString().Length;
                vastuseKast.Select(0, pikkus);
            }
        }
    }
}
