using System.Numerics;

namespace MoleculeViewer;

/// <summary>
/// A single atom parsed from an ATOM or HETATM record in a PDB file.
/// </summary>
public class Atom
{
  const float BondToleranceFactor = 1.2f;
  const float MaxBondDistance = 2.4f; // in Angstroms — applies to standard single/double covalent bonds

  public int SerialNumber { get; init; }
  public string Name { get; init; } = string.Empty;
  public string Element { get; init; } = string.Empty;
  public string ResidueName { get; init; } = string.Empty;
  public char ChainId { get; init; }
  public int ResidueSequence { get; init; }
  public System.Numerics.Vector3 Position { get; init; }
  public bool IsHetAtom { get; init; }

  /// <summary>
  /// Infers covalent bonds from atomic distances (PDB files don't reliably
  /// include CONECT records for every bond). Uses a spatial hash grid so this
  /// stays fast even on structures with thousands of atoms.
  /// 
  /// In a 3D periodic crystal lattice (or a periodic boundary box in molecular dynamics), 
  /// the spaces that can be occupied around an atom are called cells.
  /// Cells can be surrounded by up to 26 neighboring cells:
  ///
  /// - 6 face-sharing neighbors
  /// - 12 edge-sharing neighbors
  /// - 8 corner-sharing neighbors

  /// An atom positioned inside a central unit cell can physically interact with atoms in 
  /// those neighboring cells, depending on its proximity to the unit cell boundary and
  /// the cutoff radius specified for non-bonded interactions
  /// (e.g., van der Waals or electrostatic interactions).
  /// </summary>
  static public List<(int A, int B)> ComputeBonds(IReadOnlyList<Atom> atoms)
  {
    var bonds = new List<(int, int)>();
    if (atoms.Count == 0) return bonds;

    const float cellSize = MaxBondDistance;
    var grid = new Dictionary<(int, int, int), List<int>>();

    (int, int, int) CellOf(Vector3 p) =>
        ((int)MathF.Floor(p.X / cellSize),
         (int)MathF.Floor(p.Y / cellSize),
         (int)MathF.Floor(p.Z / cellSize));

    for (int i = 0; i < atoms.Count; i++)
    {
      var cell = CellOf(atoms[i].Position);
      if (!grid.TryGetValue(cell, out var list))
        grid[cell] = list = new List<int>();
      list.Add(i);
    }

    // check each atom against its neighboring cells
    for (int i = 0; i < atoms.Count; i++)
    {
      var (cx, cy, cz) = CellOf(atoms[i].Position);

      for (int dx = -1; dx <= 1; dx++)
      {
        for (int dy = -1; dy <= 1; dy++)
        {
          for (int dz = -1; dz <= 1; dz++)
          {
            // find neighboring cells within a covalent bond distance
            if (!grid.TryGetValue((cx + dx, cy + dy, cz + dz), out var neighbors)) continue;

            foreach (int j in neighbors)
            {
              if (j <= i) continue;

              float distance = Vector3.Distance(atoms[i].Position, atoms[j].Position);
              if (distance > MaxBondDistance) continue;

              float cutoff = (Constants.CovalentRadius(atoms[i].Element) +
                              Constants.CovalentRadius(atoms[j].Element)) * BondToleranceFactor;

              if (distance <= cutoff)
                bonds.Add((i, j));
            }
          }
        }
      }
    }

    return bonds;
  }
}

