using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label iconLabel)
                {
                    int randonNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randonNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randonNumber);
                }
            }
        }

        private void CheckForWinner()
        {
            // Tüm labelların ön rengi ile yazı rengini karşılaştır
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Label iconLabel)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // hepsi açılmışsa hepsi doğru bilinmiş demektir.
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (sender is Label clickedLabel)
            {
                // zaten tıklanmış bir label'a tıklarsa hiçbir şey yapma
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // ilk label'ı tıklıyorsa rengini değiştir çık
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // ikincisi tıklanıyorsa rengini değiştir
                if (secondClicked == null)
                {
                    secondClicked = clickedLabel;
                    secondClicked.ForeColor = Color.Black;
                }
                
                // hepsi açılmış mı kontrolü yap
                CheckForWinner();

                // ilk tıklanan ikinciye eşitse seçilileri açık bırak seçim değişkenlerini sıfırla
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // timer sadece bir kere çalışacak
            timer1.Stop();

            // doğru değilse renkleri eski haline getir
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
    }
}
