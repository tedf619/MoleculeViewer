namespace MoleculeViewer
{
  partial class FormMain
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
      menuStrip = new MenuStrip();
      fileToolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItemFileOpen = new ToolStripMenuItem();
      toolStripMenuItemFileExit = new ToolStripMenuItem();
      viewToolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItemBallAndStick = new ToolStripMenuItem();
      toolStripMenuItemSpheres = new ToolStripMenuItem();
      toolStripMenuItemWireframe = new ToolStripMenuItem();
      toolStripMenuItem1 = new ToolStripSeparator();
      toolStripMenuItemShowHydrogens = new ToolStripMenuItem();
      toolStripMenuItemShowBonds = new ToolStripMenuItem();
      toolStripMenuItem2 = new ToolStripSeparator();
      toolStripMenuItemResetView = new ToolStripMenuItem();
      helpToolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItemHelpAbout = new ToolStripMenuItem();
      panel1 = new Panel();
      labelStatusMessage = new Label();
      labelStatusAtomCount = new Label();
      labelStatusViewStyle = new Label();
      labeStatusMouseInstructions = new Label();
      openFileDialog = new OpenFileDialog();
      moleculeViewer = new MoleculeViewer();
      panelColors = new Panel();
      label15 = new Label();
      label14 = new Label();
      label13 = new Label();
      label12 = new Label();
      label11 = new Label();
      label10 = new Label();
      label9 = new Label();
      label8 = new Label();
      label7 = new Label();
      label6 = new Label();
      label5 = new Label();
      label4 = new Label();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      toolTip = new ToolTip(components);
      label16 = new Label();
      menuStrip.SuspendLayout();
      panel1.SuspendLayout();
      panelColors.SuspendLayout();
      SuspendLayout();
      // 
      // menuStrip
      // 
      menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
      menuStrip.Location = new Point(0, 0);
      menuStrip.Name = "menuStrip";
      menuStrip.Size = new Size(868, 24);
      menuStrip.TabIndex = 0;
      menuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemFileOpen, toolStripMenuItemFileExit });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.Size = new Size(37, 20);
      fileToolStripMenuItem.Text = "&File";
      // 
      // toolStripMenuItemFileOpen
      // 
      toolStripMenuItemFileOpen.Name = "toolStripMenuItemFileOpen";
      toolStripMenuItemFileOpen.Size = new Size(112, 22);
      toolStripMenuItemFileOpen.Text = "&Open...";
      toolStripMenuItemFileOpen.Click += ToolStripMenuItemFileOpen_Click;
      // 
      // toolStripMenuItemFileExit
      // 
      toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
      toolStripMenuItemFileExit.Size = new Size(112, 22);
      toolStripMenuItemFileExit.Text = "E&xit";
      // 
      // viewToolStripMenuItem
      // 
      viewToolStripMenuItem.Checked = true;
      viewToolStripMenuItem.CheckState = CheckState.Checked;
      viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemBallAndStick, toolStripMenuItemSpheres, toolStripMenuItemWireframe, toolStripMenuItem1, toolStripMenuItemShowHydrogens, toolStripMenuItemShowBonds, toolStripMenuItem2, toolStripMenuItemResetView });
      viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      viewToolStripMenuItem.Size = new Size(44, 20);
      viewToolStripMenuItem.Text = "&View";
      // 
      // toolStripMenuItemBallAndStick
      // 
      toolStripMenuItemBallAndStick.Checked = true;
      toolStripMenuItemBallAndStick.CheckOnClick = true;
      toolStripMenuItemBallAndStick.CheckState = CheckState.Checked;
      toolStripMenuItemBallAndStick.Name = "toolStripMenuItemBallAndStick";
      toolStripMenuItemBallAndStick.Size = new Size(164, 22);
      toolStripMenuItemBallAndStick.Text = "&Ball && Stick";
      toolStripMenuItemBallAndStick.Click += ToolStripMenuItemBallAndStick_Click;
      // 
      // toolStripMenuItemSpheres
      // 
      toolStripMenuItemSpheres.Name = "toolStripMenuItemSpheres";
      toolStripMenuItemSpheres.Size = new Size(164, 22);
      toolStripMenuItemSpheres.Text = "&Spheres";
      toolStripMenuItemSpheres.Click += ToolStripMenuItemSpheres_Click;
      // 
      // toolStripMenuItemWireframe
      // 
      toolStripMenuItemWireframe.Name = "toolStripMenuItemWireframe";
      toolStripMenuItemWireframe.Size = new Size(164, 22);
      toolStripMenuItemWireframe.Text = "&Wireframe";
      toolStripMenuItemWireframe.Click += ToolStripMenuItemWireframe_Click;
      // 
      // toolStripMenuItem1
      // 
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(161, 6);
      // 
      // toolStripMenuItemShowHydrogens
      // 
      toolStripMenuItemShowHydrogens.Checked = true;
      toolStripMenuItemShowHydrogens.CheckOnClick = true;
      toolStripMenuItemShowHydrogens.CheckState = CheckState.Checked;
      toolStripMenuItemShowHydrogens.Name = "toolStripMenuItemShowHydrogens";
      toolStripMenuItemShowHydrogens.Size = new Size(164, 22);
      toolStripMenuItemShowHydrogens.Text = "Show &Hydrogens";
      toolStripMenuItemShowHydrogens.Click += ToolStripMenuItemShowHydrogens_Click;
      // 
      // toolStripMenuItemShowBonds
      // 
      toolStripMenuItemShowBonds.Checked = true;
      toolStripMenuItemShowBonds.CheckOnClick = true;
      toolStripMenuItemShowBonds.CheckState = CheckState.Checked;
      toolStripMenuItemShowBonds.Name = "toolStripMenuItemShowBonds";
      toolStripMenuItemShowBonds.Size = new Size(164, 22);
      toolStripMenuItemShowBonds.Text = "Show B&onds";
      toolStripMenuItemShowBonds.Click += ToolStripMenuItemShowBonds_Click;
      // 
      // toolStripMenuItem2
      // 
      toolStripMenuItem2.Name = "toolStripMenuItem2";
      toolStripMenuItem2.Size = new Size(161, 6);
      // 
      // toolStripMenuItemResetView
      // 
      toolStripMenuItemResetView.Name = "toolStripMenuItemResetView";
      toolStripMenuItemResetView.Size = new Size(164, 22);
      toolStripMenuItemResetView.Text = "Reset &View";
      toolStripMenuItemResetView.Click += ToolStripMenuItemResetView_Click;
      // 
      // helpToolStripMenuItem
      // 
      helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemHelpAbout });
      helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      helpToolStripMenuItem.Size = new Size(44, 20);
      helpToolStripMenuItem.Text = "&Help";
      // 
      // toolStripMenuItemHelpAbout
      // 
      toolStripMenuItemHelpAbout.Name = "toolStripMenuItemHelpAbout";
      toolStripMenuItemHelpAbout.Size = new Size(116, 22);
      toolStripMenuItemHelpAbout.Text = "&About...";
      toolStripMenuItemHelpAbout.Click += ToolStripMenuItemHelpAbout_Click;
      // 
      // panel1
      // 
      panel1.BorderStyle = BorderStyle.Fixed3D;
      panel1.Controls.Add(labelStatusMessage);
      panel1.Controls.Add(labelStatusAtomCount);
      panel1.Controls.Add(labelStatusViewStyle);
      panel1.Controls.Add(labeStatusMouseInstructions);
      panel1.Dock = DockStyle.Bottom;
      panel1.Location = new Point(0, 445);
      panel1.Name = "panel1";
      panel1.Size = new Size(868, 32);
      panel1.TabIndex = 1;
      // 
      // labelStatusMessage
      // 
      labelStatusMessage.Dock = DockStyle.Fill;
      labelStatusMessage.Location = new Point(0, 0);
      labelStatusMessage.Name = "labelStatusMessage";
      labelStatusMessage.Size = new Size(407, 28);
      labelStatusMessage.TabIndex = 3;
      labelStatusMessage.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelStatusAtomCount
      // 
      labelStatusAtomCount.BorderStyle = BorderStyle.Fixed3D;
      labelStatusAtomCount.Dock = DockStyle.Right;
      labelStatusAtomCount.Location = new Point(407, 0);
      labelStatusAtomCount.Name = "labelStatusAtomCount";
      labelStatusAtomCount.Size = new Size(94, 28);
      labelStatusAtomCount.TabIndex = 2;
      labelStatusAtomCount.Text = "Atom Count";
      labelStatusAtomCount.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // labelStatusViewStyle
      // 
      labelStatusViewStyle.BorderStyle = BorderStyle.Fixed3D;
      labelStatusViewStyle.Dock = DockStyle.Right;
      labelStatusViewStyle.Location = new Point(501, 0);
      labelStatusViewStyle.Name = "labelStatusViewStyle";
      labelStatusViewStyle.Size = new Size(100, 28);
      labelStatusViewStyle.TabIndex = 1;
      labelStatusViewStyle.Text = "View Style";
      labelStatusViewStyle.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // labeStatusMouseInstructions
      // 
      labeStatusMouseInstructions.BorderStyle = BorderStyle.Fixed3D;
      labeStatusMouseInstructions.Dock = DockStyle.Right;
      labeStatusMouseInstructions.Location = new Point(601, 0);
      labeStatusMouseInstructions.Name = "labeStatusMouseInstructions";
      labeStatusMouseInstructions.Size = new Size(263, 28);
      labeStatusMouseInstructions.TabIndex = 0;
      labeStatusMouseInstructions.Text = "drag = rotate, right-drag = pan, wheel = zoom";
      labeStatusMouseInstructions.TextAlign = ContentAlignment.MiddleRight;
      // 
      // openFileDialog
      // 
      openFileDialog.FileName = "openFileDialog";
      openFileDialog.Title = "Open a PDB File";
      // 
      // moleculeViewer
      // 
      moleculeViewer.BackColor = Color.FromArgb(18, 18, 22);
      moleculeViewer.Dock = DockStyle.Fill;
      moleculeViewer.Location = new Point(39, 24);
      moleculeViewer.Name = "moleculeViewer";
      moleculeViewer.Size = new Size(829, 421);
      moleculeViewer.TabIndex = 2;
      // 
      // panelColors
      // 
      panelColors.BorderStyle = BorderStyle.FixedSingle;
      panelColors.Controls.Add(label16);
      panelColors.Controls.Add(label15);
      panelColors.Controls.Add(label14);
      panelColors.Controls.Add(label13);
      panelColors.Controls.Add(label12);
      panelColors.Controls.Add(label11);
      panelColors.Controls.Add(label10);
      panelColors.Controls.Add(label9);
      panelColors.Controls.Add(label8);
      panelColors.Controls.Add(label7);
      panelColors.Controls.Add(label6);
      panelColors.Controls.Add(label5);
      panelColors.Controls.Add(label4);
      panelColors.Controls.Add(label3);
      panelColors.Controls.Add(label2);
      panelColors.Controls.Add(label1);
      panelColors.Dock = DockStyle.Left;
      panelColors.Location = new Point(0, 24);
      panelColors.Name = "panelColors";
      panelColors.Size = new Size(39, 421);
      panelColors.TabIndex = 3;
      // 
      // label15
      // 
      label15.BackColor = Color.WhiteSmoke;
      label15.BorderStyle = BorderStyle.FixedSingle;
      label15.Location = new Point(5, 3);
      label15.Name = "label15";
      label15.Size = new Size(28, 23);
      label15.TabIndex = 14;
      label15.Text = "H";
      label15.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label15, "Hydrogen");
      // 
      // label14
      // 
      label14.BackColor = Color.FromArgb(171, 92, 242);
      label14.BorderStyle = BorderStyle.FixedSingle;
      label14.ForeColor = Color.White;
      label14.Location = new Point(5, 367);
      label14.Name = "label14";
      label14.Size = new Size(28, 23);
      label14.TabIndex = 13;
      label14.Text = "Na";
      label14.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label14, "Sodium");
      // 
      // label13
      // 
      label13.BackColor = Color.FromArgb(61, 255, 0);
      label13.BorderStyle = BorderStyle.FixedSingle;
      label13.Location = new Point(5, 341);
      label13.Name = "label13";
      label13.Size = new Size(28, 23);
      label13.TabIndex = 12;
      label13.Text = "Ca";
      label13.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label13, "Calcium");
      // 
      // label12
      // 
      label12.BackColor = Color.FromArgb(138, 255, 0);
      label12.BorderStyle = BorderStyle.FixedSingle;
      label12.Location = new Point(5, 315);
      label12.Name = "label12";
      label12.Size = new Size(28, 23);
      label12.TabIndex = 11;
      label12.Text = "Mg";
      label12.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label12, "Magnesium");
      // 
      // label11
      // 
      label11.BackColor = Color.FromArgb(125, 128, 176);
      label11.BorderStyle = BorderStyle.FixedSingle;
      label11.ForeColor = Color.White;
      label11.Location = new Point(5, 289);
      label11.Name = "label11";
      label11.Size = new Size(28, 23);
      label11.TabIndex = 10;
      label11.Text = "Zn";
      label11.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label11, "Zinc");
      // 
      // label10
      // 
      label10.BackColor = Color.FromArgb(224, 102, 51);
      label10.BorderStyle = BorderStyle.FixedSingle;
      label10.ForeColor = Color.White;
      label10.Location = new Point(5, 263);
      label10.Name = "label10";
      label10.Size = new Size(28, 23);
      label10.TabIndex = 9;
      label10.Text = "Fe";
      label10.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label10, "Iron");
      // 
      // label9
      // 
      label9.BackColor = Color.FromArgb(148, 0, 148);
      label9.BorderStyle = BorderStyle.FixedSingle;
      label9.ForeColor = Color.White;
      label9.Location = new Point(5, 237);
      label9.Name = "label9";
      label9.Size = new Size(28, 23);
      label9.TabIndex = 8;
      label9.Text = "I";
      label9.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label9, "Iodiine");
      // 
      // label8
      // 
      label8.BackColor = Color.FromArgb(166, 41, 41);
      label8.BorderStyle = BorderStyle.FixedSingle;
      label8.ForeColor = Color.White;
      label8.Location = new Point(5, 211);
      label8.Name = "label8";
      label8.Size = new Size(28, 23);
      label8.TabIndex = 7;
      label8.Text = "Br";
      label8.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label8, "Bromine");
      // 
      // label7
      // 
      label7.BackColor = Color.FromArgb(31, 240, 31);
      label7.BorderStyle = BorderStyle.FixedSingle;
      label7.Location = new Point(5, 185);
      label7.Name = "label7";
      label7.Size = new Size(28, 23);
      label7.TabIndex = 6;
      label7.Text = "Cl";
      label7.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label7, "Chlorine");
      // 
      // label6
      // 
      label6.BackColor = Color.FromArgb(144, 224, 80);
      label6.BorderStyle = BorderStyle.FixedSingle;
      label6.Location = new Point(5, 159);
      label6.Name = "label6";
      label6.Size = new Size(28, 23);
      label6.TabIndex = 5;
      label6.Text = "F";
      label6.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label6, "Fluorine");
      // 
      // label5
      // 
      label5.BackColor = Color.FromArgb(255, 128, 0);
      label5.BorderStyle = BorderStyle.FixedSingle;
      label5.Location = new Point(5, 133);
      label5.Name = "label5";
      label5.Size = new Size(28, 23);
      label5.TabIndex = 4;
      label5.Text = "P";
      label5.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label5, "Phosphorus");
      // 
      // label4
      // 
      label4.BackColor = Color.FromArgb(255, 200, 50);
      label4.BorderStyle = BorderStyle.FixedSingle;
      label4.Location = new Point(5, 107);
      label4.Name = "label4";
      label4.Size = new Size(28, 23);
      label4.TabIndex = 3;
      label4.Text = "S";
      label4.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label4, "Sulfur");
      // 
      // label3
      // 
      label3.BackColor = Color.FromArgb(255, 13, 13);
      label3.BorderStyle = BorderStyle.FixedSingle;
      label3.Location = new Point(5, 81);
      label3.Name = "label3";
      label3.Size = new Size(28, 23);
      label3.TabIndex = 2;
      label3.Text = "O";
      label3.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label3, "Oxygen");
      // 
      // label2
      // 
      label2.BackColor = Color.FromArgb(48, 80, 248);
      label2.BorderStyle = BorderStyle.FixedSingle;
      label2.ForeColor = Color.White;
      label2.Location = new Point(5, 55);
      label2.Name = "label2";
      label2.Size = new Size(28, 23);
      label2.TabIndex = 1;
      label2.Text = "N";
      label2.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label2, "Nitrogen");
      // 
      // label1
      // 
      label1.BackColor = Color.Gray;
      label1.BorderStyle = BorderStyle.FixedSingle;
      label1.ForeColor = Color.White;
      label1.Location = new Point(5, 29);
      label1.Name = "label1";
      label1.Size = new Size(28, 23);
      label1.TabIndex = 0;
      label1.Text = "C";
      label1.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label1, "Carbon");
      // 
      // toolTip
      // 
      toolTip.AutoPopDelay = 5000;
      toolTip.InitialDelay = 50;
      toolTip.ReshowDelay = 100;
      // 
      // label16
      // 
      label16.BackColor = Color.FromArgb(143, 64, 21);
      label16.BorderStyle = BorderStyle.FixedSingle;
      label16.ForeColor = Color.White;
      label16.Location = new Point(4, 393);
      label16.Name = "label16";
      label16.Size = new Size(28, 23);
      label16.TabIndex = 15;
      label16.Text = "K";
      label16.TextAlign = ContentAlignment.MiddleCenter;
      toolTip.SetToolTip(label16, "Potassium");
      // 
      // FormMain
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(868, 477);
      Controls.Add(moleculeViewer);
      Controls.Add(panelColors);
      Controls.Add(panel1);
      Controls.Add(menuStrip);
      MainMenuStrip = menuStrip;
      Name = "FormMain";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "MoleculeViewer";
      menuStrip.ResumeLayout(false);
      menuStrip.PerformLayout();
      panel1.ResumeLayout(false);
      panelColors.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItemFileOpen;
    private ToolStripMenuItem toolStripMenuItemFileExit;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItemBallAndStick;
    private ToolStripMenuItem toolStripMenuItemSpheres;
    private ToolStripMenuItem toolStripMenuItemWireframe;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItemShowHydrogens;
    private ToolStripMenuItem toolStripMenuItemShowBonds;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItemResetView;
    private Panel panel1;
    private Label labelStatusMessage;
    private Label labelStatusAtomCount;
    private Label labelStatusViewStyle;
    private Label labeStatusMouseInstructions;
    private OpenFileDialog openFileDialog;
    private MoleculeViewer moleculeViewer;
    private Panel panelColors;
    private Label label15;
    private Label label14;
    private Label label13;
    private Label label12;
    private Label label11;
    private Label label10;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label6;
    private Label label5;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label label1;
    private ToolTip toolTip;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItemHelpAbout;
    private Label label16;
  }
}
