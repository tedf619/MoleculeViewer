namespace MoleculeViewer
{
  partial class FormAbout
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
      label1 = new Label();
      buttonOk = new Button();
      label2 = new Label();
      label3 = new Label();
      linkLabelWwpdb = new LinkLabel();
      SuspendLayout();
      // 
      // label1
      // 
      label1.Dock = DockStyle.Top;
      label1.ForeColor = Color.Black;
      label1.Location = new Point(0, 0);
      label1.Name = "label1";
      label1.Size = new Size(708, 117);
      label1.TabIndex = 0;
      label1.Text = resources.GetString("label1.Text");
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // buttonOk
      // 
      buttonOk.Location = new Point(315, 374);
      buttonOk.Name = "buttonOk";
      buttonOk.Size = new Size(75, 23);
      buttonOk.TabIndex = 1;
      buttonOk.Text = "OK";
      buttonOk.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      label2.Dock = DockStyle.Top;
      label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      label2.ForeColor = Color.Black;
      label2.Location = new Point(0, 140);
      label2.Name = "label2";
      label2.Size = new Size(708, 40);
      label2.TabIndex = 2;
      label2.Text = "About PDB Files";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      label3.Dock = DockStyle.Top;
      label3.ForeColor = Color.Black;
      label3.Location = new Point(0, 180);
      label3.Name = "label3";
      label3.Size = new Size(708, 188);
      label3.TabIndex = 3;
      label3.Text = resources.GetString("label3.Text");
      label3.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // linkLabelWwpdb
      // 
      linkLabelWwpdb.Dock = DockStyle.Top;
      linkLabelWwpdb.Location = new Point(0, 117);
      linkLabelWwpdb.Name = "linkLabelWwpdb";
      linkLabelWwpdb.Size = new Size(708, 23);
      linkLabelWwpdb.TabIndex = 5;
      linkLabelWwpdb.TabStop = true;
      linkLabelWwpdb.Text = "https://www.wwpdb.org/";
      linkLabelWwpdb.LinkClicked += LinkLabelWwpdb_LinkClicked;
      // 
      // FormAbout
      // 
      AcceptButton = buttonOk;
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      CancelButton = buttonOk;
      ClientSize = new Size(708, 422);
      Controls.Add(label3);
      Controls.Add(label2);
      Controls.Add(buttonOk);
      Controls.Add(linkLabelWwpdb);
      Controls.Add(label1);
      FormBorderStyle = FormBorderStyle.FixedSingle;
      Name = "FormAbout";
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterScreen;
      Text = "About MoleculeViewer";
      ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private Button buttonOk;
    private Label label2;
    private Label label3;
    private LinkLabel linkLabelWwpdb;
  }
}