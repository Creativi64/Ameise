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
using AmeisenGame;

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

        private Stopwatch st;

        public Guid AktiveAmeise { get; set; }

        public Guid AktivesNest { get; set; }

        public void updateall()
        {
            this.AktiveAmei.Text = AktiveAmeise.ToString();
            this.AktiveNest.Text = AktivesNest.ToString();
            lib_Ameisen.Refresh();
            lib_Nester.Refresh();
        }
  
        public Form1()
        {
            AllocConsole();
            InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            //Thread.Sleep(250);
            Game.init(this.CreateGraphics(), this.Size.Width, this.Size.Height, SmoothingMode.HighSpeed, true, true, 3);

            //Game.Feld[0][0].Nest = this.Nest;
            AktivesNest = Game.Nester[0].Idenifier;
            AktiveAmeise = Nest.peekFirstAmeiseFromNest(AktivesNest).Idenifier;

            lib_Ameisen.DataSource = Game.AlleAmeisen;

            lib_Ameisen.IntegralHeight = true;
            Graphics g = lib_Ameisen.CreateGraphics();
            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(lib_Ameisen.Items[lib_Ameisen.Items.Count - 1].ToString(), lib_Ameisen.Font).Width;
            // Set the HorizontalExtent property.
            lib_Ameisen.HorizontalExtent = hzSize;

            lib_Nester.DataSource = Game.AlleNester;

            lib_Nester.IntegralHeight = true;
            g = lib_Nester.CreateGraphics();
            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            hzSize = (int)g.MeasureString(lib_Nester.Items[lib_Nester.Items.Count - 1].ToString(), lib_Nester.Font).Width;
            // Set the HorizontalExtent property.
            lib_Nester.HorizontalExtent = hzSize;

            g.Dispose();

            lib_Ameisen.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            lib_Nester.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            Console.WriteLine("START");

            Engine.Marks = Chb_Marks.Checked;
            this.BgW_Ameins.WorkerSupportsCancellation = true;

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
            Game.ResetGame();
            Engine.Draw();
        }

        private void Btn_SetEnd_Click(object sender, EventArgs e)
        {
            Game.Nester[Nest.getActiveNest(AktivesNest)].depolyAmeiseFromNest(AktivesNest);
            updateall();
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

            updateall();

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
            updateall();
            Engine.Draw();
        }

        private void TxtB_Move_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Console.WriteLine("Up");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {
                    Nest.getAmeiseInField(AktiveAmeise).MoveUp();
                } 
            }
            if (e.KeyCode == Keys.Down)
            {
                Console.WriteLine("Down");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {
                    Nest.getAmeiseInField(AktiveAmeise).MoveDown();
                } 
            }
            if (e.KeyCode == Keys.Right)
            {
                Console.WriteLine("Right");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {
                    Nest.getAmeiseInField(AktiveAmeise).MoveRight();
                } 
            }
            if (e.KeyCode == Keys.Left)
            {
                Console.WriteLine("Left");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {
                    Nest.getAmeiseInField(AktiveAmeise).MoveLeft();
                } 
            }

            if (Nest.getAmeiseInField(AktiveAmeise) != null)
            {
                toolStripPos.Text = Nest.getAmeiseInField(AktiveAmeise).Pos.ToString();

                //Console.WriteLine(Game.AlleAmeisen[Game.AlleAmeisen.FindIndex(n => n.Idenifier == AktiveAmeise)].Pos);
                //Console.WriteLine(Nest.getAmeiseInField(AktiveAmeise).Pos);

                Game.AlleAmeisen[Game.AlleAmeisen.FindIndex(n => n.Idenifier == AktiveAmeise)].Pos = Nest.getAmeiseInField(AktiveAmeise).Pos;

                //Console.WriteLine("--------");
                //Console.WriteLine(Game.AlleAmeisen[Game.AlleAmeisen.FindIndex(n => n.Idenifier == AktiveAmeise)].Pos);
                //Console.WriteLine(Nest.getAmeiseInField(AktiveAmeise).Pos);
                updateall();
            }
             

            if (e.KeyCode == Keys.Space)
            {
                Console.WriteLine("TryCollect");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {
                    Nest.getAmeiseInField(AktiveAmeise).TryCollect();
                    foreach (var item in Nest.getAmeiseInField(AktiveAmeise).Inventar)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                 
            }

            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("TryPlace");
                if (Nest.getAmeiseInField(AktiveAmeise) != null)
                {

                    Nest.getAmeiseInField(AktiveAmeise).TryPlaceBlock();
                    Engine.Draw();
                }
            }
            if (e.KeyCode == Keys.S)
            {
                if (BgW_Ameins.IsBusy == false)
                {
                    Console.WriteLine("Suche");
                    st = new Stopwatch();
                     
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
            // THE OTHER WAY
            AktiveAmeise = ((Entry)lib_Ameisen.SelectedItem).Idenifier;

            updateall();
        }

        private void lib_Nester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // THE WAY

            Entry nest = lib_Nester.SelectedItem as Entry;
            AktivesNest = nest.Idenifier;
            AktiveAmeise = Nest.peekFirstAmeiseFromNest(AktivesNest).Idenifier;
            if (AktiveAmei != null)
            {
                lib_Ameisen.SelectedIndex = Game.AlleAmeisen.FindIndex(n => n.Idenifier == AktiveAmeise);
            }

            updateall();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game.AlleAmeisen.Add(new Entry(Guid.Empty, Vector2.Zero, Color.Aqua, "test"));
            lib_Ameisen.Refresh();
            lib_Ameisen.Update();
        }

        private void lib_Ameisen_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based
            // on the index of the item to draw.

            myBrush = new SolidBrush(((Entry)lib_Ameisen.Items[e.Index]).Team);

            // Draw the current item text based on the current Font
            // and the custom brush settings.
            e.Graphics.DrawString(lib_Ameisen.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void lib_Nester_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based
            // on the index of the item to draw.

            myBrush = new SolidBrush(((Entry)lib_Nester.Items[e.Index]).Team);

            // Draw the current item text based on the current Font
            // and the custom brush settings.
            e.Graphics.DrawString(lib_Nester.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
    }
}