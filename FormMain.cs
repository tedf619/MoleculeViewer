namespace MoleculeViewer;

public partial class FormMain : Form
{
  PdbFile pdbFile = new();  // Protein Data Bank file handler
  FormAbout formAbout = new();

  public FormMain()
  {
    InitializeComponent();
  }

  void ToolStripMenuItemFileOpen_Click(object sender, EventArgs e)
  {
    if (openFileDialog.ShowDialog() != DialogResult.OK) return;

    LoadPdbFile(openFileDialog.FileName);
  }

  void ToolStripMenuItemFileExit_Click(object sender, EventArgs e)
  {
    Close();
  }

  void ToolStripMenuItemBallAndStick_Click(object sender, EventArgs e)
  {
    toolStripMenuItemBallAndStick.Checked = true;
    toolStripMenuItemSpheres.Checked = false;
    toolStripMenuItemWireframe.Checked = false;

    SetViewStyle(ViewingStyle.BallAndStick);
  }

  void ToolStripMenuItemSpheres_Click(object sender, EventArgs e)
  {
    toolStripMenuItemBallAndStick.Checked = false;
    toolStripMenuItemSpheres.Checked = true;
    toolStripMenuItemWireframe.Checked = false;

    SetViewStyle(ViewingStyle.Spheres);
  }

  void ToolStripMenuItemWireframe_Click(object sender, EventArgs e)
  {
    toolStripMenuItemBallAndStick.Checked = false;
    toolStripMenuItemSpheres.Checked = false;
    toolStripMenuItemWireframe.Checked = true;

    SetViewStyle(ViewingStyle.Wireframe);
  }

  void ToolStripMenuItemShowHydrogens_Click(object sender, EventArgs e)
  {
    moleculeViewer.ShowHydrogens = toolStripMenuItemShowHydrogens.Checked;
    moleculeViewer.Invalidate();
  }

  void ToolStripMenuItemShowBonds_Click(object sender, EventArgs e)
  {
    moleculeViewer.ShowBonds = toolStripMenuItemShowBonds.Checked;
    moleculeViewer.Invalidate();
  }

  void ToolStripMenuItemResetView_Click(object sender, EventArgs e)
  {
    moleculeViewer.ResetView();
  }

  void ToolStripMenuItemHelpAbout_Click(object sender, EventArgs e)
  {
    formAbout.ShowDialog();
  }

  void SetViewStyle(ViewingStyle style)
  {
    labelStatusViewStyle.Text = style.ToString();
    moleculeViewer.Style = style;
    moleculeViewer.Invalidate();
  }

  void LoadPdbFile(string filePath)
  {

    Cursor = Cursors.WaitCursor;
    try
    {
      var structure = pdbFile.Parse(filePath);

      if (structure.Atoms.Count == 0)
      {
        MessageBox.Show("No ATOM/HETATM records were found in this file. Make sure it's a valid PDB file.",
                        "No atoms found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      moleculeViewer.LoadStructure(structure.Atoms);

      labelStatusMessage.Text = System.IO.Path.GetFileName(filePath);

      string fileDescription = !string.IsNullOrWhiteSpace(structure.Title) ? structure.Title : Path.GetFileName(filePath);
      Text = $"MoleculeViewer - {fileDescription}";

      labelStatusAtomCount.Text = $"{structure.Atoms.Count:N0} atoms";
      labelStatusViewStyle.Text = moleculeViewer.Style.ToString();
    }
    catch (Exception ex)
    {
      Cursor = Cursors.Default;
      MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
      Cursor = Cursors.Default;
    }
  }

}
