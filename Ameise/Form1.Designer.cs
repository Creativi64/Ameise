
namespace Ameise
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.EngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disposeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPos = new System.Windows.Forms.ToolStripTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Btn_SetStart = new System.Windows.Forms.Button();
            this.Btn_SetEnd = new System.Windows.Forms.Button();
            this.Btn_SetStein = new System.Windows.Forms.Button();
            this.TxtB_Move = new System.Windows.Forms.TextBox();
            this.Chb_Marks = new System.Windows.Forms.CheckBox();
            this.ChB_Scan = new System.Windows.Forms.CheckBox();
            this.BgW_Ameins = new System.ComponentModel.BackgroundWorker();
            this.TimeElapsed = new System.Windows.Forms.Label();
            this.Recall = new System.Windows.Forms.Button();
            this.lib_Nester = new System.Windows.Forms.ListBox();
            this.lib_Ameisen = new System.Windows.Forms.ListBox();
            this.lab_AmeisenImNest = new System.Windows.Forms.Label();
            this.lab_Nester = new System.Windows.Forms.Label();
            this.lib_AmeisenImFeld = new System.Windows.Forms.ListBox();
            this.lab_AmeisenImFeld = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EngineToolStripMenuItem,
            this.toolStripPos});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(961, 27);
            this.MenuStrip1.TabIndex = 1;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // EngineToolStripMenuItem
            // 
            this.EngineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.CreateToolStripMenuItem,
            this.ResetToolStripMenuItem,
            this.disposeToolStripMenuItem});
            this.EngineToolStripMenuItem.Name = "EngineToolStripMenuItem";
            this.EngineToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
            this.EngineToolStripMenuItem.Text = "Engine";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(112, 6);
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.CreateToolStripMenuItem.Text = "Create";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.FindPathToolStripMenuItem_Click);
            // 
            // ResetToolStripMenuItem
            // 
            this.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem";
            this.ResetToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.ResetToolStripMenuItem.Text = "Reset";
            this.ResetToolStripMenuItem.Click += new System.EventHandler(this.EnginToolStripMenuItem_Click);
            // 
            // disposeToolStripMenuItem
            // 
            this.disposeToolStripMenuItem.Name = "disposeToolStripMenuItem";
            this.disposeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.disposeToolStripMenuItem.Text = "Dispose";
            this.disposeToolStripMenuItem.Click += new System.EventHandler(this.DisposeToolStripMenuItem_Click);
            // 
            // toolStripPos
            // 
            this.toolStripPos.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripPos.Name = "toolStripPos";
            this.toolStripPos.ReadOnly = true;
            this.toolStripPos.Size = new System.Drawing.Size(100, 23);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // Btn_SetStart
            // 
            this.Btn_SetStart.Location = new System.Drawing.Point(12, 31);
            this.Btn_SetStart.Name = "Btn_SetStart";
            this.Btn_SetStart.Size = new System.Drawing.Size(75, 23);
            this.Btn_SetStart.TabIndex = 8;
            this.Btn_SetStart.Text = "ReSet";
            this.Btn_SetStart.UseVisualStyleBackColor = true;
            this.Btn_SetStart.Click += new System.EventHandler(this.Btn_SetStart_Click);
            // 
            // Btn_SetEnd
            // 
            this.Btn_SetEnd.Location = new System.Drawing.Point(12, 60);
            this.Btn_SetEnd.Name = "Btn_SetEnd";
            this.Btn_SetEnd.Size = new System.Drawing.Size(75, 23);
            this.Btn_SetEnd.TabIndex = 9;
            this.Btn_SetEnd.Text = "DeployAmeise";
            this.Btn_SetEnd.UseVisualStyleBackColor = true;
            this.Btn_SetEnd.Click += new System.EventHandler(this.Btn_SetEnd_Click);
            // 
            // Btn_SetStein
            // 
            this.Btn_SetStein.Location = new System.Drawing.Point(12, 89);
            this.Btn_SetStein.Name = "Btn_SetStein";
            this.Btn_SetStein.Size = new System.Drawing.Size(75, 23);
            this.Btn_SetStein.TabIndex = 10;
            this.Btn_SetStein.Text = "Debug";
            this.Btn_SetStein.UseVisualStyleBackColor = true;
            this.Btn_SetStein.Click += new System.EventHandler(this.Btn_SetStein_Click);
            // 
            // TxtB_Move
            // 
            this.TxtB_Move.Location = new System.Drawing.Point(207, 30);
            this.TxtB_Move.Name = "TxtB_Move";
            this.TxtB_Move.ReadOnly = true;
            this.TxtB_Move.Size = new System.Drawing.Size(100, 23);
            this.TxtB_Move.TabIndex = 11;
            this.TxtB_Move.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtB_Move_KeyDown);
            // 
            // Chb_Marks
            // 
            this.Chb_Marks.AutoSize = true;
            this.Chb_Marks.Location = new System.Drawing.Point(113, 27);
            this.Chb_Marks.Margin = new System.Windows.Forms.Padding(1);
            this.Chb_Marks.Name = "Chb_Marks";
            this.Chb_Marks.Size = new System.Drawing.Size(58, 19);
            this.Chb_Marks.TabIndex = 12;
            this.Chb_Marks.Text = "Marks";
            this.Chb_Marks.UseVisualStyleBackColor = true;
            this.Chb_Marks.CheckedChanged += new System.EventHandler(this.Chb_Marks_CheckedChanged);
            // 
            // ChB_Scan
            // 
            this.ChB_Scan.AutoSize = true;
            this.ChB_Scan.Location = new System.Drawing.Point(113, 50);
            this.ChB_Scan.Name = "ChB_Scan";
            this.ChB_Scan.Size = new System.Drawing.Size(51, 19);
            this.ChB_Scan.TabIndex = 13;
            this.ChB_Scan.Text = "Scan";
            this.ChB_Scan.UseVisualStyleBackColor = true;
            this.ChB_Scan.CheckedChanged += new System.EventHandler(this.ChB_Scan_CheckedChanged);
            // 
            // BgW_Ameins
            // 
            this.BgW_Ameins.WorkerSupportsCancellation = true;
            this.BgW_Ameins.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Bg_Ameins_DoWork);
            this.BgW_Ameins.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgW_Ameins_ProgressChanged);
            this.BgW_Ameins.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Bg_Ameins_RunWorkerCompleted);
            // 
            // TimeElapsed
            // 
            this.TimeElapsed.AutoSize = true;
            this.TimeElapsed.Location = new System.Drawing.Point(329, 31);
            this.TimeElapsed.Name = "TimeElapsed";
            this.TimeElapsed.Size = new System.Drawing.Size(0, 15);
            this.TimeElapsed.TabIndex = 14;
            // 
            // Recall
            // 
            this.Recall.Location = new System.Drawing.Point(12, 118);
            this.Recall.Name = "Recall";
            this.Recall.Size = new System.Drawing.Size(75, 23);
            this.Recall.TabIndex = 15;
            this.Recall.Text = "Recall";
            this.Recall.UseVisualStyleBackColor = true;
            this.Recall.Click += new System.EventHandler(this.Recall_Click);
            // 
            // lib_Nester
            // 
            this.lib_Nester.FormattingEnabled = true;
            this.lib_Nester.HorizontalScrollbar = true;
            this.lib_Nester.ItemHeight = 15;
            this.lib_Nester.Location = new System.Drawing.Point(12, 176);
            this.lib_Nester.Name = "lib_Nester";
            this.lib_Nester.Size = new System.Drawing.Size(120, 94);
            this.lib_Nester.TabIndex = 16;
            this.lib_Nester.SelectedIndexChanged += new System.EventHandler(this.lib_Nester_SelectedIndexChanged);
            // 
            // lib_Ameisen
            // 
            this.lib_Ameisen.FormattingEnabled = true;
            this.lib_Ameisen.HorizontalScrollbar = true;
            this.lib_Ameisen.ItemHeight = 15;
            this.lib_Ameisen.Location = new System.Drawing.Point(12, 289);
            this.lib_Ameisen.Name = "lib_Ameisen";
            this.lib_Ameisen.Size = new System.Drawing.Size(120, 199);
            this.lib_Ameisen.TabIndex = 17;
            this.lib_Ameisen.SelectedIndexChanged += new System.EventHandler(this.lib_Ameisen_SelectedIndexChanged);
            // 
            // lab_AmeisenImNest
            // 
            this.lab_AmeisenImNest.AutoSize = true;
            this.lab_AmeisenImNest.Location = new System.Drawing.Point(12, 273);
            this.lab_AmeisenImNest.Name = "lab_AmeisenImNest";
            this.lab_AmeisenImNest.Size = new System.Drawing.Size(53, 15);
            this.lab_AmeisenImNest.TabIndex = 18;
            this.lab_AmeisenImNest.Text = "Ameisen";
            // 
            // lab_Nester
            // 
            this.lab_Nester.AutoSize = true;
            this.lab_Nester.Location = new System.Drawing.Point(13, 155);
            this.lab_Nester.Name = "lab_Nester";
            this.lab_Nester.Size = new System.Drawing.Size(41, 15);
            this.lab_Nester.TabIndex = 19;
            this.lab_Nester.Text = "Nester";
            // 
            // lib_AmeisenImFeld
            // 
            this.lib_AmeisenImFeld.FormattingEnabled = true;
            this.lib_AmeisenImFeld.HorizontalScrollbar = true;
            this.lib_AmeisenImFeld.ItemHeight = 15;
            this.lib_AmeisenImFeld.Location = new System.Drawing.Point(135, 470);
            this.lib_AmeisenImFeld.Name = "lib_AmeisenImFeld";
            this.lib_AmeisenImFeld.Size = new System.Drawing.Size(120, 94);
            this.lib_AmeisenImFeld.TabIndex = 20;
            this.lib_AmeisenImFeld.SelectedIndexChanged += new System.EventHandler(this.lib_AmeisenImFeld_SelectedIndexChanged);
            // 
            // lab_AmeisenImFeld
            // 
            this.lab_AmeisenImFeld.AutoSize = true;
            this.lab_AmeisenImFeld.Location = new System.Drawing.Point(135, 452);
            this.lab_AmeisenImFeld.Name = "lab_AmeisenImFeld";
            this.lab_AmeisenImFeld.Size = new System.Drawing.Size(92, 15);
            this.lab_AmeisenImFeld.TabIndex = 21;
            this.lab_AmeisenImFeld.Text = "Ameisen m Feld";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 613);
            this.Controls.Add(this.lab_AmeisenImFeld);
            this.Controls.Add(this.lib_AmeisenImFeld);
            this.Controls.Add(this.lab_Nester);
            this.Controls.Add(this.lab_AmeisenImNest);
            this.Controls.Add(this.lib_Ameisen);
            this.Controls.Add(this.lib_Nester);
            this.Controls.Add(this.Recall);
            this.Controls.Add(this.TimeElapsed);
            this.Controls.Add(this.ChB_Scan);
            this.Controls.Add(this.Chb_Marks);
            this.Controls.Add(this.TxtB_Move);
            this.Controls.Add(this.Btn_SetStein);
            this.Controls.Add(this.Btn_SetEnd);
            this.Controls.Add(this.Btn_SetStart);
            this.Controls.Add(this.MenuStrip1);
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_Resize);
            this.SizeChanged += new System.EventHandler(this.Form1_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EngineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Button Btn_SetStart;
        private System.Windows.Forms.Button Btn_SetEnd;
        private System.Windows.Forms.Button Btn_SetStein;
        private System.Windows.Forms.TextBox TxtB_Move;
        private System.Windows.Forms.ToolStripTextBox toolStripPos;
        private System.Windows.Forms.ToolStripMenuItem disposeToolStripMenuItem;
        private System.Windows.Forms.CheckBox Chb_Marks;
        private System.Windows.Forms.CheckBox ChB_Scan;
        private System.ComponentModel.BackgroundWorker BgW_Ameins;
        private System.Windows.Forms.Label TimeElapsed;
        private System.Windows.Forms.Button Recall;
        private System.Windows.Forms.ListBox lib_Nester;
        private System.Windows.Forms.ListBox lib_Ameisen;
        private System.Windows.Forms.Label lab_AmeisenImNest;
        private System.Windows.Forms.Label lab_Nester;
        private System.Windows.Forms.ListBox lib_AmeisenImFeld;
        private System.Windows.Forms.Label lab_AmeisenImFeld;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

