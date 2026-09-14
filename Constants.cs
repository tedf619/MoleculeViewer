namespace MoleculeViewer;

public static class Constants
{
  // key is element symbol, value is color according to CPK convention (created by Corey-Pauling-Koltun in the 1950s)
  static readonly Dictionary<string, Color> ElementColors = new(StringComparer.OrdinalIgnoreCase)
  {
    ["H"] = System.Drawing.Color.WhiteSmoke,
    ["C"] = System.Drawing.Color.FromArgb(80, 80, 80),
    ["N"] = System.Drawing.Color.FromArgb(48, 80, 248),
    ["O"] = System.Drawing.Color.FromArgb(255, 13, 13),
    ["S"] = System.Drawing.Color.FromArgb(255, 200, 50),
    ["P"] = System.Drawing.Color.FromArgb(255, 128, 0),
    ["F"] = System.Drawing.Color.FromArgb(144, 224, 80),
    ["CL"] = System.Drawing.Color.FromArgb(31, 240, 31),
    ["BR"] = System.Drawing.Color.FromArgb(166, 41, 41),
    ["I"] = System.Drawing.Color.FromArgb(148, 0, 148),
    ["FE"] = System.Drawing.Color.FromArgb(224, 102, 51),
    ["ZN"] = System.Drawing.Color.FromArgb(125, 128, 176),
    ["MG"] = System.Drawing.Color.FromArgb(138, 255, 0),
    ["CA"] = System.Drawing.Color.FromArgb(61, 255, 0),
    ["NA"] = System.Drawing.Color.FromArgb(171, 92, 242),
    ["K"] = System.Drawing.Color.FromArgb(143, 64, 21),
  };

  // key is element symbol, value is radius in Angstroms
  static readonly Dictionary<string, float> ElementRadii = new(StringComparer.OrdinalIgnoreCase)
  {
    ["H"] = 0.31f,
    ["C"] = 0.76f,
    ["N"] = 0.71f,
    ["O"] = 0.66f,
    ["S"] = 1.05f,
    ["P"] = 1.07f,
    ["F"] = 0.57f,
    ["CL"] = 1.02f,
    ["BR"] = 1.20f,
    ["I"] = 1.39f,
    ["FE"] = 1.32f,
    ["ZN"] = 1.22f,
    ["MG"] = 1.41f,
    ["CA"] = 1.76f,
    ["NA"] = 1.66f,
    ["K"] = 1.96f,
  };

  public static Color Color(string element) =>
      ElementColors.TryGetValue(element, out var c) ? c 
      : System.Drawing.Color.FromArgb(255, 20, 147);  // default to deep pink for unknown elements

  public static float CovalentRadius(string element) =>
      ElementRadii.TryGetValue(element, out var r) ? r : 0.75f;
}
