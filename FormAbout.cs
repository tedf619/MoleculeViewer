namespace MoleculeViewer;

public partial class FormAbout : Form
{
  public FormAbout()
  {
    InitializeComponent();

    linkLabelWwpdb.Links.Add(0, linkLabelWwpdb.Text.Length, "https://www.wwpdb.org/");
  }

  private void LinkLabelWwpdb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    string? url = e.Link?.LinkData as string;

    try
    {
      System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
      {
        FileName = url,
        UseShellExecute = true
      });
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
}
