using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Take_Home_Assignment_Week_7.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace Take_Home_Assignment_Week_7
{

    public partial class Form2 : Form
    {
        Button button;
        Button buttonabjad;
        List<Button> buttonkeyboard = new List<Button>();
        List<Button> buttongame = new List<Button>();
        List<string> listkata = new List<string>();
        int inputuser = 0;
        int counter = 0;
        int rowcounter = 0;
        string jawaban;
        string jawabanbaru;
        string cekjawaban;
        string lines = Properties.Resources.Wordle_Word_List;
        string[] tampunglist;
        string[] abjad = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "Enter", "z", "x", "c", "v", "b", "n", "m", "Delete" };
        bool salah = true;
        char[] ch;



        public Form2(int inputform1)
        {
            InitializeComponent();
            inputuser = inputform1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //randomize jawaban dari txt
            tampunglist = lines.ToString().Split(',');
            for (int i = 0; i < tampunglist.Length; i++)
            {
                listkata.Add(tampunglist[i]);
            }
            Random random = new Random();
            int randomindex = random.Next(listkata.Count);
            jawaban = listkata[randomindex];

            MessageBox.Show(jawaban);
            ch = jawaban.ToCharArray();

            //bikin keyboard
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    buttonabjad = new Button();
                    buttonabjad.Size = new Size(45, 45);
                    buttonabjad.Location = new Point(300 + (50 * i), 60 + (50 * j));
                    this.Controls.Add(buttonabjad);
                    buttonkeyboard.Add(buttonabjad);
                    buttonabjad.Click += button_Click;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    buttonabjad = new Button();
                    buttonabjad.Size = new Size(45, 45);
                    buttonabjad.Location = new Point(300 + (50 * i) + 25, 110 + (50 * j));
                    this.Controls.Add(buttonabjad);
                    buttonkeyboard.Add(buttonabjad);
                    buttonabjad.Click += button_Click;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    buttonabjad = new Button();
                    buttonabjad.Size = new Size(45, 45);
                    buttonabjad.Location = new Point(300 + (50 * i) + 25, 160 + (50 * j));
                    if (j == 0 && i == 0)
                    {
                        buttonabjad.Location = new Point(245 + (50 * i) + 25, 160 + (50 * j));
                        buttonabjad.Size = new Size(100, 45);
                    }
                    else if (j == 0 && i == 8)
                    {
                        buttonabjad.Size = new Size(100, 45);
                    }
                    this.Controls.Add(buttonabjad);
                    buttonkeyboard.Add(buttonabjad);
                    buttonabjad.Click += button_Click;
                }
            }
            for (int i = 0; i < buttonkeyboard.Count; i++)
            {
                buttonkeyboard[i].Text = abjad[i].ToString();
            }

            //create button sesuai input user
            for (int i = 0; i < inputuser; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    button = new Button();
                    button.Location = new Point(50 * j, 50 * i);
                    button.Size = new Size(50, 50);
                    this.Controls.Add(button);
                    buttongame.Add(button);
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            jawabanbaru = jawaban;
            Button button = (Button)sender;
            salah = true;

            //delete key
            if (button == buttonkeyboard[27])
            {
                if (counter != 0)
                {
                    if (counter> (rowcounter*5) - 1)
                    {
                        if (counter != rowcounter * 5)
                        {
                            buttongame[counter - 1].Text = "";
                            counter--;
                        }
                    }
                }
            }

            //enter key
            else if (button == buttonkeyboard[19])
            {
                for (int i = 5 * rowcounter; i < (5 * rowcounter) + 5; i++)
                {
                    cekjawaban = cekjawaban + buttongame[i].Text;
                }

                for (int i = 0; i < listkata.Count; i++)
                {
                    if (cekjawaban == listkata[i])
                    {
                        salah = false;
                    }
                }


                if (salah == false)
                {
                    if (cekjawaban == jawaban)
                    {
                        MessageBox.Show("YOU WIN, the answer is " + cekjawaban);
                        this.Hide();
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        if (cekjawaban[i] == jawabanbaru[i])
                        {
                            buttongame[i + (rowcounter * 5)].BackColor = Color.LightGreen;
                            ch[i] = '_';
                            jawabanbaru = new string(ch);
                        }
                        else if (jawabanbaru.Contains(cekjawaban[i]))
                        {
                            buttongame[i + (rowcounter * 5)].BackColor = Color.LightYellow;
                            ch[i] = '_';
                            jawabanbaru = new string(ch);
                        }
                    }

                    MessageBox.Show(jawabanbaru);
                    cekjawaban = "";
                    rowcounter++;

                    if (rowcounter == inputuser && cekjawaban != jawaban)
                    {
                        MessageBox.Show("YOU LOSE, the answer is " + jawaban);
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show(cekjawaban + " is not on the word list");
                    cekjawaban = "";
                }

            }

            //word key
            else
            {
                for (int i = 0; i < buttonkeyboard.Count; i++)
                {
                    if (button == buttonkeyboard[i] && counter < (5 * rowcounter) + 5)
                    {
                        buttongame[counter].Text = buttonkeyboard[i].Text;
                        counter++;
                    }

                }
            }

        }

    }
}