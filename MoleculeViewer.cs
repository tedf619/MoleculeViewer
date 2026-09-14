using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace MoleculeViewer
{
  // The three viewing styles supported by the molecule viewer.
  public enum ViewingStyle
  {
    BallAndStick, // classic style: atoms as spheres, bonds as sticks
    Spheres,      // atoms as spheres, no bonds
    Wireframe     // lines only: atoms as points at ends of bond lines
  }

  public partial class MoleculeViewer : UserControl
  {
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ViewingStyle Style { get; set; } = ViewingStyle.BallAndStick;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowHydrogens { get; set; } = true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowBonds { get; set; } = true;

    IReadOnlyList<Atom> atoms = Array.Empty<Atom>();
    List<(int A, int B)> bonds = new();
    Vector3 centerOfMolecule;
    float baseSpan = 20f, yaw = 0.4f, pitch = 0.3f, zoom = 1f;
    PointF pan, lastMouse;
    bool isRotating, isPanning;

    public MoleculeViewer()
    {
      InitializeComponent();

      SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

      MouseWheel += OnMouseWheel;  // event not exposed by designer, so hook it up manually
    }
    public void LoadStructure(IReadOnlyList<Atom> allAtoms)
    {
      atoms = allAtoms;
      bonds = Atom.ComputeBonds(allAtoms);
      ResetView();
    }

    // resets the view to show the entire molecule, centered and scaled to fit within the control
    public void ResetView()
    {
      if (atoms.Count == 0)
      {
        centerOfMolecule = Vector3.Zero;
        baseSpan = 20f;
      }
      else
      {
        var min = new Vector3(float.MaxValue);
        var max = new Vector3(float.MinValue);
        var sum = Vector3.Zero;

        foreach (var atom in atoms)
        {
          sum += atom.Position;
          min = Vector3.Min(min, atom.Position);
          max = Vector3.Max(max, atom.Position);
        }

        centerOfMolecule = sum / atoms.Count;

        var size = max - min;
        float maxDim = MathF.Max(size.X, MathF.Max(size.Y, size.Z));
        baseSpan = maxDim > 0 ? maxDim : 20f;
      }

      yaw = 0.4f;
      pitch = 0.3f;
      zoom = 1f;
      pan = PointF.Empty;
      Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      if (atoms.Count == 0) return;
      
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;

      // precompute sine and cosine of yaw and pitch for rotation
      float sinYaw = MathF.Sin(yaw);
      float cosYaw = MathF.Cos(yaw);
      float sinPitch = MathF.Sin(pitch);
      float cosPitch = MathF.Cos(pitch);

      // compute scale factor to fit the molecule within the control's dimensions, with some padding
      float scale = (Math.Min(Width, Height) * 0.8f / baseSpan) * zoom;

      // Array of atoms project to a 2D screen.
      // Each item has screen coordinates, z-depth, element symbol, and whether it's hydrogen.
      var projected = new (PointF Screen, float Depth, string Symbol, bool IsHydrogen)[atoms.Count];

      // project each atom's 3D position to 2D screen coordinates, applying rotation and scaling
      for (int i = 0; i < atoms.Count; i++)
      {
        // Algorithm:
        //   1. Translate the atom's position to be relative to the center of the molecule.
        //   2. Apply yaw and pitch rotations.
        //   3. Translate (pan) to screen coordinates and include scaling (zoom) factor

        // The rotation is done using a standard 3D rotation matrix, first around the Y-axis and then around the X-axis
        // The Y axis (yaw) is vertical, positive upwards.
        // The X axis (pitch) is horizontal, positive to the right.
        // The Z axis is depth (positive comingout of the screen).

        var p = atoms[i].Position - centerOfMolecule;

        // rotate around Y axis
        float x1 = p.X * cosYaw + p.Z * sinYaw;
        float z1 = -p.X * sinYaw + p.Z * cosYaw;
        float y1 = p.Y;

        // rotate around X axis
        float y2 = y1 * cosPitch - z1 * sinPitch;
        float z2 = y1 * sinPitch + z1 * cosPitch;
        float x2 = x1;

        // pan to screen coordinates
        float screenX = Width / 2f + pan.X + x2 * scale;
        float screenY = Height / 2f + pan.Y - y2 * scale;

        string symbol = atoms[i].Element; // e.g., "C", "H", "Fe", etc.

        // temporarily save the projected screen coordinates, depth, symbol, and whether it's hydrogen
        projected[i] = (new PointF(screenX, screenY), z2, symbol, symbol.Equals("H", StringComparison.OrdinalIgnoreCase));
      }

      if (ShowBonds && Style != ViewingStyle.Spheres)
        DrawBonds(g, projected, scale);

      if (Style != ViewingStyle.Wireframe)
      {
        // determine back-to-front so farther atoms appear behind nearer ones
        var order = Enumerable.Range(0, atoms.Count)
            .Where(i => ShowHydrogens || !projected[i].IsHydrogen)
            .OrderBy(i => projected[i].Depth)
            .ToArray();

        float sphereFactor = Style == ViewingStyle.BallAndStick ? 0.45f : 1f;

        // draw atoms in back-to-front order
        foreach (int i in order)
        {
          var (screen, _, element, _) = projected[i];
          float radius = Math.Max(Constants.CovalentRadius(element) * sphereFactor * scale, 2f);
          DrawShadedSphere(g, screen, radius, Constants.Color(element));
        }
      }
    }

    /// <summary>
    /// Draws each bond as two half-length segments, colored to match each end
    /// atom's element (the classic two-tone "stick" look used by molecular
    /// viewers like Mol*/PyMOL), with rounded caps so sticks read as solid rods.
    /// </summary>
    void DrawBonds(Graphics g, (PointF Screen, float Depth, string Element, bool IsHydrogen)[] projected, float scale)
    {
      bool wireframe = Style == ViewingStyle.Wireframe;
      float stickWidth = wireframe ? 1.5f : Math.Max(scale * 0.12f, 2f);

      foreach (var (a, b) in bonds)
      {
        if (!ShowHydrogens && (projected[a].IsHydrogen || projected[b].IsHydrogen)) continue;

        var pA = projected[a].Screen;
        var pB = projected[b].Screen;

        if (wireframe)
        {
          using var pen = new Pen(Color.FromArgb(200, 200, 200), stickWidth);
          g.DrawLine(pen, pA, pB);
          continue;
        }

        var mid = new PointF((pA.X + pB.X) / 2f, (pA.Y + pB.Y) / 2f);
        var colorA = Constants.Color(projected[a].Element);
        var colorB = Constants.Color(projected[b].Element);

        using var penA = new Pen(colorA, stickWidth) { StartCap = LineCap.Round, EndCap = LineCap.Round };
        using var penB = new Pen(colorB, stickWidth) { StartCap = LineCap.Round, EndCap = LineCap.Round };

        g.DrawLine(penA, pA, mid);
        g.DrawLine(penB, mid, pB);
      }
    }

    /// <summary>
    /// Draws an atom as a radially-shaded sphere: a highlight offset toward
    /// the upper-left, darkening toward the rim, to simulate the glossy
    /// look of a real 3D-rendered sphere.
    /// </summary>
    void DrawShadedSphere(Graphics g, PointF center, float radius, Color baseColor)
    {
      var bounds = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);

      using var path = new GraphicsPath();
      path.AddEllipse(bounds);

      using var brush = new PathGradientBrush(path)
      {
        CenterPoint = new PointF(center.X - radius * 0.35f, center.Y - radius * 0.35f),
        CenterColor = Lighten(baseColor, 0.65f),
        SurroundColors = new[] { Darken(baseColor, 0.3f) }
      };

      g.FillEllipse(brush, bounds);

      using var outline = new Pen(Color.FromArgb(90, 0, 0, 0));
      g.DrawEllipse(outline, bounds);
    }

    Color Lighten(Color c, float amount)
    {
      int r = c.R + (int)((255 - c.R) * amount);
      int gr = c.G + (int)((255 - c.G) * amount);
      int b = c.B + (int)((255 - c.B) * amount);
      return Color.FromArgb(Math.Clamp(r, 0, 255), Math.Clamp(gr, 0, 255), Math.Clamp(b, 0, 255));
    }

    Color Darken(Color c, float amount)
    {
      int r = (int)(c.R * (1 - amount));
      int g = (int)(c.G * (1 - amount));
      int b = (int)(c.B * (1 - amount));
      return Color.FromArgb(Math.Clamp(r, 0, 255), Math.Clamp(g, 0, 255), Math.Clamp(b, 0, 255));
    }

    void OnMouseDown(object? sender, MouseEventArgs e)
    {
      lastMouse = e.Location;
      isRotating = e.Button == MouseButtons.Left;
      isPanning = e.Button is MouseButtons.Right or MouseButtons.Middle;
    }

    void OnMouseMove(object? sender, MouseEventArgs e)
    {
      if (isRotating)
      {
        yaw += (e.X - lastMouse.X) * 0.01f;
        pitch += (e.Y - lastMouse.Y) * 0.01f;
        lastMouse = e.Location;
        Invalidate();
      }
      else if (isPanning)
      {
        pan = new PointF(pan.X + (e.X - lastMouse.X), pan.Y + (e.Y - lastMouse.Y));
        lastMouse = e.Location;
        Invalidate();
      }
    }

    void OnMouseUp(object? sender, MouseEventArgs e)
    {
      isRotating = false;
      isPanning = false;
    }

    void OnMouseWheel(object? sender, MouseEventArgs e)
    {
      float factor = e.Delta > 0 ? 1.1f : 1f / 1.1f;
      zoom = Math.Clamp(zoom * factor, 0.1f, 20f);
      Invalidate();
    }
  }
}
