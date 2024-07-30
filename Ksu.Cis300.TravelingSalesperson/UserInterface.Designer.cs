namespace Ksu.Cis300.TravelingSalesperson
{
    partial class UserInterface
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            uxMenuBar = new ToolStrip();
            uxFindCircuit = new ToolStripButton();
            uxClear = new ToolStripButton();
            uxSplitContainer = new SplitContainer();
            uxPoints = new ListBox();
            uxDrawing = new DrawingCanvas();
            uxMouseLocation = new ToolTip(components);
            uxMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)uxSplitContainer).BeginInit();
            uxSplitContainer.Panel1.SuspendLayout();
            uxSplitContainer.Panel2.SuspendLayout();
            uxSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // uxMenuBar
            // 
            uxMenuBar.ImageScalingSize = new Size(24, 24);
            uxMenuBar.Items.AddRange(new ToolStripItem[] { uxFindCircuit, uxClear });
            uxMenuBar.Location = new Point(0, 0);
            uxMenuBar.Name = "uxMenuBar";
            uxMenuBar.Padding = new Padding(0, 0, 3, 0);
            uxMenuBar.Size = new Size(1143, 34);
            uxMenuBar.TabIndex = 0;
            uxMenuBar.Text = "toolStrip1";
            // 
            // uxFindCircuit
            // 
            uxFindCircuit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            uxFindCircuit.Enabled = false;
            uxFindCircuit.Image = (Image)resources.GetObject("uxFindCircuit.Image");
            uxFindCircuit.ImageTransparentColor = Color.Magenta;
            uxFindCircuit.Name = "uxFindCircuit";
            uxFindCircuit.Size = new Size(104, 29);
            uxFindCircuit.Text = "Find Circuit";
            uxFindCircuit.Click += FindCircuitClick;
            // 
            // uxClear
            // 
            uxClear.DisplayStyle = ToolStripItemDisplayStyle.Text;
            uxClear.Image = (Image)resources.GetObject("uxClear.Image");
            uxClear.ImageTransparentColor = Color.Magenta;
            uxClear.Name = "uxClear";
            uxClear.Size = new Size(55, 29);
            uxClear.Text = "Clear";
            uxClear.Click += ClearClick;
            // 
            // uxSplitContainer
            // 
            uxSplitContainer.Dock = DockStyle.Fill;
            uxSplitContainer.Location = new Point(0, 34);
            uxSplitContainer.Margin = new Padding(4, 5, 4, 5);
            uxSplitContainer.Name = "uxSplitContainer";
            // 
            // uxSplitContainer.Panel1
            // 
            uxSplitContainer.Panel1.Controls.Add(uxPoints);
            // 
            // uxSplitContainer.Panel2
            // 
            uxSplitContainer.Panel2.Controls.Add(uxDrawing);
            uxSplitContainer.Size = new Size(1143, 716);
            uxSplitContainer.SplitterDistance = 184;
            uxSplitContainer.SplitterWidth = 6;
            uxSplitContainer.TabIndex = 1;
            // 
            // uxPoints
            // 
            uxPoints.Dock = DockStyle.Fill;
            uxPoints.FormattingEnabled = true;
            uxPoints.ItemHeight = 25;
            uxPoints.Location = new Point(0, 0);
            uxPoints.Margin = new Padding(4, 5, 4, 5);
            uxPoints.Name = "uxPoints";
            uxPoints.Size = new Size(184, 716);
            uxPoints.TabIndex = 0;
            // 
            // uxDrawing
            // 
            uxDrawing.Dock = DockStyle.Fill;
            uxDrawing.Location = new Point(0, 0);
            uxDrawing.Margin = new Padding(6, 8, 6, 8);
            uxDrawing.Name = "uxDrawing";
            uxDrawing.Size = new Size(953, 716);
            uxDrawing.TabIndex = 0;
            uxDrawing.MouseClick += DrawingMouseClick;
            uxDrawing.MouseMove += DrawingMouseMove;
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(uxSplitContainer);
            Controls.Add(uxMenuBar);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UserInterface";
            Text = "Shortest Circuit";
            uxMenuBar.ResumeLayout(false);
            uxMenuBar.PerformLayout();
            uxSplitContainer.Panel1.ResumeLayout(false);
            uxSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)uxSplitContainer).EndInit();
            uxSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip uxMenuBar;
        private ToolStripButton uxFindCircuit;
        private ToolStripButton uxClear;
        private SplitContainer uxSplitContainer;
        private ListBox uxPoints;
        private DrawingCanvas uxDrawing;
        private ToolTip uxMouseLocation;
    }
}