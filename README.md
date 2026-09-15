# MoleculeViewer
### C# with .NET 10 and Windows Forms
MoleculeViewer is a desktop application that lets you visualize and interact with molecular structures in 3D. 
It supports standard PDB (Protein Data Bank) files.

You can view molecules in three different standardized styles:

  1. *Ball and Stick.*  Atoms appear as 3D balls. Covalent bonds appear as pipes.
  2. *Spheres.* Atoms only shown. No bonds.
  3. *Wireframe.* Atoms are collapsed down to points. Covalent bonds appear as simple lines.

The following figures show a deoxyribose molecule in various styles.

<img width="870" height="482" alt="image" src="https://github.com/user-attachments/assets/47b4296a-fc26-4d99-85b2-2971745dddb0" />

*Figure 1 - The Ball and Stick style.*
<br><br>

<img width="870" height="482" alt="image" src="https://github.com/user-attachments/assets/884e996c-6aa4-46be-b53a-d50d8b422173" />

*Figure 2 - The Spheres style.*
<br><br>

<img width="870" height="482" alt="image" src="https://github.com/user-attachments/assets/a8e2c5ea-dccb-4f4c-890a-a9f0aaeecabd" />

*Figure 3 - The Wireframe style.*
<br><br>

The vertical bar along the left shows the most common atom colors, based on the CPK convention (created by Corey-Pauling-Koltun in the 1950s).
Atoms not contained in the list are colored in dark pink.

## Additional Viewing Options

With larger molecules the screen can get really busy. To reduce screen clutter there are two more settings 

  1. *Show/hide hydrogen atoms.* Given the large number of hydrogen atoms that often occur as bond terminations in organic molecules, it is common to suppress hydrogen.
  2. *Show/hide bonds.* By suppressing bonds, you can focus just on the atoms.

These two options apply mostly to the Ball and Stick style. The following figure shows deoxyribose with hydrogen atoms hidden.

<img width="870" height="482" alt="image" src="https://github.com/user-attachments/assets/46bf31ac-d531-4e3b-b702-4a2cc3a61972" />

*Figure 4 - A deoxyribose molecule with hidden hydrogen atoms.*
<br><br>

The following figure shows deoxyribose with covalent bonds hidden.

<img width="870" height="482" alt="image" src="https://github.com/user-attachments/assets/bd04d078-06fb-4b76-afbf-3db372f3a971" />

*Figure 5 - An deoxyribose molecule with hidden covalent bonds.*
<br><br>

## UI Layout

There are three docked items that make up the UI, as shown in the following figure.

<img width="800" height="482" alt="image" src="https://github.com/user-attachments/assets/648eb03c-67c9-4fe0-adab-90832d0873ee" />

*Figure 6 - The three docked items of the user interface.*

To achieve the required layout, the items must be added in order of docking priority:

  1. Statusbar Panel. This goes first since we want it to take up the entire width along the bottom edge.
  2. Color Panel. This goes next, so it takes up the remaining height along the left edge.
  3. MoleculeViewer Control. This goes last, since it will take up all remaining screen space.

## Painting Molecules in 3D

Perhaps the most interesting aspect of the app is how it paints atoms and bonds in 3D, supporting rotation, panning and zooming.
The mouse interactions and the painting are handled by the MoleculeViewer Control. The following listing shows the painting code.

```csharp
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
```
*Listing 1 - The code to paint molecules in 3D.*

The perspective and size of the molecule are affected by the following variables:

  * *yaw.* The amount of rotation around the vertical (Y) axis.
  * *pitch.* The amount of rotation around the horizontal (X) axis.
  * *pan.* The amount of up-down-left-right displacement of the molecule on the screen.
  * *scale.* The zoom factor, where values larger than one magnify the image.

These variables are manipulated in the mouse event handlers, as shown below.

```csharp
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

```
*Listing 2 - The mouse event handlers.*

## PDB Files

For many years the Protein Data Bank (PDB) file format was the standard format adopted by the Worldwide Protein Data Bank (wwPdb).
It is a text-based standard used to store 3D atomic coordinates and structural metadata of molecules, primarily of biological nature.
Developed in the early 1970s, it uses an 80-character fixed-column ASCII structure derived from the IBM punch card that was common with mainframes at the time.
Today newer and more flexible formats have been developed, but PDB files are still widely used.

Every record in a PDB file is a separate text line that can contain at most 80 characters.
The first 6-character field is the record type. Here are the most common types:

```
"HEADER". Contains the classification, the publication date and a unique 4-character PDB ID.
"TITLE ". Describes the molecule.
"COMPND". Describes the compound, biological entity or species.
"SOURCE". Same as above.
"REMARK". A general comment.
"ATOM  ". Used with nucleic acids and amino acids.
"HETATM". Defines a so-called "heteroatom", which is something other than an ATOM record above.
"TER   ". Marks the termination of a polypeptide or nucleic acid chain.
"CONECT". Defines a bond between atoms.
"END   ". Marks the last record of a PDB file.
```
*Table 1 - The main PDB record types.*

The most common records are ATOM and HETATM types, which both have the same layout.
Text fields are right-padded with spaces. Numeric fields are left-padded with spaces.
In MoleculeViewer we only care about the some of the available PDB fields, as defined below.

```
Field Name                    Columns        Description

Record Type                   1-6            "ATOM  " or "HETATM"
Atom Number                   7-11           Atom serial number 1, 2, 3, ..n
Atom Name                     13-16          Element symbol or abbreviation
Chain Identifier              22             which chain an atom belongs to
X Coordinate                  31-38          8.3 floating point format (in Angstoms)
Y Coordinate                  39-46          8.3 floating point format (in Angstoms)
Z Coordinate                  47-54          8.3 floating point format (in Angstoms)
Element Symbol                77-78          Atomic symbol, e.g. H, C, Fe
Others...
```
*Table 2 - The fields we process in the ATOM/HETATM record.*

For more information about the PDB format, visit the Worldwide Protein Data Bank site at www.wwpdb.org.
