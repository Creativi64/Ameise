using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Ameise
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        /*
         Y
         |
         O__X
         */

        //public Amei ameise;

        //public Nest Nest;

        //public Amei ameise1;
        private Stopwatch st;

        public Guid AktiveAmeise;
        public Guid AktivesNest;

        public Form1()
        {
            AllocConsole();
            InitializeComponent();
            Thread.Sleep(250);
            Game.init(this.CreateGraphics(), this.Size.Width, this.Size.Height, SmoothingMode.HighQuality, true, true, 3);

            //Game.Feld[0][0].Nest = this.Nest;
            AktivesNest = Game.Nester[0].Idenifier;
            foreach (var item in Game.Nester)
            {
                lib_Nester.Items.Add(item.Idenifier);
            }

            foreach (var item in Game.Nester[Nest.getActiveNest(AktivesNest)].ameisen)
            {
                lib_Ameisen.Items.Add(item.Idenifier);
            }

            AktiveAmeise = Nest.peekFirstAmeiseFromNest(AktivesNest).Idenifier;

            Console.WriteLine("START");

            Engine.Marks = Chb_Marks.Checked;
            this.BgW_Ameins.WorkerSupportsCancellation = true;

            //ameise = (new Amei(new Vector2(0, 0), Color.CadetBlue));

            //toolStripPos.Text = ameise.Pos.ToString();

            TxtB_Move.Focus();
            this.ActiveControl = TxtB_Move;

            this.BeginInvoke((MethodInvoker)delegate
            {
                Engine.Draw();
            });
        }

        private void FindPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void EnginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.FeldGrafik.Clear(this.BackColor);
            Engine.Draw();

            //GC.Collect();
        }

        private void Btn_SetStart_Click(object sender, EventArgs e)
        {
            Game.Nester[Nest.getActiveNest(AktivesNest)].RecallAmeis();
            Game.ResetGame();
            Engine.Draw();
        }

        private void Btn_SetEnd_Click(object sender, EventArgs e)
        {
            Game.Nester[Nest.getActiveNest(AktivesNest)].depolyAmeiseFromNest(AktivesNest);
            lib_Ameisen.Items.Clear();
            foreach (var item in Game.Nester[Nest.getActiveNest(AktivesNest)].ameisen)
            {
                lib_Ameisen.Items.Add(item.Idenifier);
            }
            lib_AmeisenImFeld.Items.Clear();
            foreach (var item in Nest.getAllAmeiseInField())
            {
                lib_AmeisenImFeld.Items.Add(item.ToString());
            }
            Engine.Draw();
        }

        private void Btn_SetStein_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Draw();
        }

        private void Recall_Click(object sender, EventArgs e)
        {
            Game.Nester[Nest.getActiveNest(AktivesNest)].RecallAmeis();

            lib_Ameisen.Items.Clear();
            foreach (var item in Game.Nester[Nest.getActiveNest(AktivesNest)].ameisen)
            {
                lib_Ameisen.Items.Add(item.Idenifier);
            }
            lib_AmeisenImFeld.Items.Clear();
            foreach (var item in Nest.getAllAmeiseInField())
            {
                lib_AmeisenImFeld.Items.Add(item.ToString());
            }

            Engine.Draw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool cancaled = false;
            if (this.BgW_Ameins.IsBusy)
            {
                this.BgW_Ameins.CancelAsync();
                while (this.BgW_Ameins.CancellationPending)
                {
                    Console.WriteLine(this.BgW_Ameins.CancellationPending);
                    Application.DoEvents();
                }

                cancaled = true;
            }

            Game.HandelResizeEvent(this.CreateGraphics(), this.Size.Width, this.Size.Height, this.BackColor);

            if (cancaled == true)
            {
                this.BgW_Ameins.RunWorkerAsync();
            }

            //GC.Collect();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Engine.Draw();
        }

        private void TxtB_Move_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Console.WriteLine("Up");
                Nest.getAmeiseInField(AktiveAmeise).MoveUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                Console.WriteLine("Down");
                Nest.getAmeiseInField(AktiveAmeise).MoveDown();
            }
            if (e.KeyCode == Keys.Right)
            {
                Console.WriteLine("Right");
                Nest.getAmeiseInField(AktiveAmeise).MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                Console.WriteLine("Left");
                Nest.getAmeiseInField(AktiveAmeise).MoveLeft();
            }

            toolStripPos.Text = Nest.getAmeiseInField(AktiveAmeise).Pos.ToString();

            if (e.KeyCode == Keys.Space)
            {
                Console.WriteLine("TryCollect");
                Nest.getAmeiseInField(AktiveAmeise).TryCollect();
                foreach (var item in Nest.getAmeiseInField(AktiveAmeise).Inventar)
                {
                    Console.WriteLine(item.Name);
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("TryPlace");
                Nest.getAmeiseInField(AktiveAmeise).TryPlaceBlock();
                Engine.Draw();
            }
            if (e.KeyCode == Keys.S)
            {
                if (BgW_Ameins.IsBusy == false)
                {
                    Console.WriteLine("Suche");
                    st = new Stopwatch();
                    //ameise.Search(Fld);
                    st.Start();
                    TimeSpan ts;
                    this.BgW_Ameins.RunWorkerAsync();
                   
                    while (this.BgW_Ameins.I‎sBusy)
                    {
                        ts = st.Elapsed;

                        // Format and display the TimeSpan value.
                        TimeElapsed.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds / 10);
                        Application.DoEv‎ents();
                         
                    }
                }
                else
                {
                    BgW_Ameins.Cancel‎Async();
                }
            }

            //GC.Collect();
        }

        private void DisposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game.ResetGame();

            Engine.Draw();
        }

        private void Chb_Marks_CheckedChanged(object sender, EventArgs e)
        {
            bool cancaled = false;
            if (this.BgW_Ameins.IsBusy)
            {
                this.BgW_Ameins.CancelAsync();
                while (this.BgW_Ameins.CancellationPending)
                {
                    Console.WriteLine(this.BgW_Ameins.CancellationPending);
                    Application.DoEvents();
                }

                cancaled = true;
            }

            Engine.Marks = Chb_Marks.Checked;
            Engine.Draw();

            if (cancaled == true)
            {
                this.BgW_Ameins.RunWorkerAsync();
            }
        }

        private void ChB_Scan_CheckedChanged(object sender, EventArgs e)
        {
            bool cancaled = false;
            if (this.BgW_Ameins.IsBusy)
            {
                this.BgW_Ameins.CancelAsync();
                while (this.BgW_Ameins.CancellationPending)
                {
                    Console.WriteLine(this.BgW_Ameins.CancellationPending);
                    Application.DoEvents();
                }

                cancaled = true;
            }

            Engine.Scan = ChB_Scan.Checked;
            Engine.Draw();

            if (cancaled == true)
            {
                this.BgW_Ameins.RunWorkerAsync();
            }
        }

        private void Bg_Ameins_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            // Start the time-consuming operation.
            if (Nest.getAmeiseInField(AktiveAmeise) != null)
            {
                Nest.getAmeiseInField(AktiveAmeise).StartCollect(bw);
            }

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Bg_Ameins_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Caneled");
            }
            else if (e.Error != null)
            {
                Console.WriteLine("error:" + e.Error);
            }
            else
            {
                st.Stop();
                Console.WriteLine("Fertig");
            }
        }

        private void BgW_Ameins_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        private void lib_Ameisen_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktiveAmeise = Guid.Parse(lib_Ameisen.SelectedItem.ToString());
        }

        private void lib_Nester_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktivesNest = Guid.Parse(lib_Nester.SelectedItem.ToString());
            AktiveAmeise = Nest.peekFirstAmeiseFromNest(AktivesNest).Idenifier;
            lib_Ameisen.Items.Clear();

            foreach (var item in Game.Nester[Nest.getActiveNest(AktivesNest)].ameisen)
            {
                lib_Ameisen.Items.Add(item.Idenifier);
            }
        }

        private void lib_AmeisenImFeld_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktiveAmeise = Guid.Parse(lib_AmeisenImFeld.SelectedItem.ToString());
        }
    }
}