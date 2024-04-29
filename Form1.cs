using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameRandomButtons
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Counter = 0;
        bool IsOverGame = false;
        int BtnSpeed = 5;
        Random random = new Random();

        void CreateRandomButton()
        {
            Button btn = new Button();
            btn.Top = 0;
            btn.Width = 100;
            btn.Height = 30;
            btn.Left = random.Next(0, ClientSize.Width - btn.Width);
            btn.BackColor = Color.LightGoldenrodYellow;
            btn.Text = random.Next(1, 100).ToString();
            btn.Click += button_Click;
            this.Controls.Add(btn);

        }
        void btnMoving(Button btn)
        {
            btn.Top += BtnSpeed;

        }

        void MoveAllButtons()
        {
            foreach (Button btn in this.Controls.OfType<Button>())
            {
                btnMoving(btn);

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CreateRandomButton();
            timer1.Start();
            this.Controls.Remove((Button)sender);
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Controls.Remove((Button)sender);
            Counter++;
            lblCounter.Text = Counter.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CreateRandomButton();
            MoveAllButtons();
            if (GameEnd() == true)
            {
                return;
            }
        }
        private void ResetForm()
        {
            // Reset counter
            Counter = 0;
            lblCounter.Text = "0";

            // Remove all buttons from the form
            foreach (Button btn in this.Controls.OfType<Button>().ToList())
            {
                this.Controls.Remove(btn);
            }

            // Reset game state
            IsOverGame = false;
            this.Controls.Add(btnStart);
        }
        bool GameEnd()
        {
            foreach (Button btn in Controls.OfType<Button>())
            {
                if (btn.Top == ClientSize.Height)
                {
                    timer1.Stop();
                    MessageBox.Show("Game Over (-");
                    // Reset the form
                    ResetForm();
                    return true;
                }
            }
            return false;
        }
    }
}
