using System.Globalization;
using System.Numerics;
using System.Text;

namespace MoleculeViewer;

// PDB (Protein Data Bank) files describe molecule structure in text format.
// For full details see: https://www.wwpdb.org/documentation/file-format

/// <summary>
/// The parsed contents of a PDB file that we care about: 
/// Title and IdCode, pluse the list of atoms to render.
/// </summary>
public sealed class PdbStructure
{
  public string Title { get; init; } = string.Empty;
  public string IdCode { get; init; } = string.Empty;
  public IReadOnlyList<Atom> Atoms { get; init; } = Array.Empty<Atom>();
}

/// <summary>
/// Parses the records of a PDB file.
/// Malformed individual lines are skipped rather than aborting the whole file,
/// since real-world PDB files occasionally have minor formatting quirks.
/// There are vaious types of lines in a PDB file. There are designated with a starting string:
/// 
///   1. HEADER
///   2. TITLE
///   3. COMPND
///   4. REMARK
///   5. ATOM or HETATM
///   6. CONECT
///   7. TER
///   8. END
///   
/// HEADER lines contain the PDB ID code (columns 63-66) and the deposition date (columns 51-59).
/// TITLE lines contain a human-readable description of the molecule.
/// COMPND lines contain the chemical name of the molecule.
/// REMARK lines contain comments about the molecule.
///   ATOM indicates the atom belongs to either:
///     1. one of the 20 standard amino acids.
///     2. one of the DNA or RNA nucleotides (A, C, G, T, U).
///   HETATM indicates the atom belongs to something other than the above.
/// CONECT lines contain the connectivity information between atoms.
/// TER lines indicate the termination of a chain of residues.
/// END lines indicate the end of the PDB file.
/// </summary>
/* 
Here is a synopsis of a PDB file:

HEADER    SUGAR                                   11-SEP-26   NONE
TITLE     3D COORDINATES FOR 2-DEOXY-D-RIBOSE (CYCLIC FORM)
COMPND    MOL_ID: 1; MOLECULE: 2-DEOXY-D-RIBOFURANOSE; RESIDUE: ORP
HETATM    1  C1  ORP A   1       1.212   0.380   0.241  1.00 15.00           C  
...
HETATM   19 HO5  ORP A   1      -4.153   0.781   0.852  1.00 15.00           H  
CONECT    1    2    5    6   10
...
CONECT    9    8   19
END
*/

public class PdbFile
{
  HashSet<string> KnownTwoLetterElements = new(StringComparer.OrdinalIgnoreCase)
  {
    "CL", "BR", "FE", "ZN", "MG", "NA", "MN", "CU", "NI", "CO"
  };

  public PdbStructure Parse(string filePath)
  {
    var atoms = new List<Atom>();
    var titleBuilder = new StringBuilder();
    string idCode = string.Empty;

    foreach (var rawLine in File.ReadLines(filePath))
    {
      if (rawLine.Length < 6) continue;
      var record = rawLine[..6].TrimEnd();

      switch (record)
      {
        case "ATOM":
        case "HETATM":
          var atom = TryParseAtomLine(rawLine, isHeteroAtom: record == "HETATM");
          if (atom != null) atoms.Add(atom);
          break;

        case "TITLE":
          if (rawLine.Length > 10)
            titleBuilder.Append(rawLine[10..].Trim()).Append(' ');
          break;

        case "HEADER":
          if (rawLine.Length >= 66)
            idCode = rawLine[62..66].Trim();
          break;
      }
    }

    return new PdbStructure
    {
      Title = titleBuilder.ToString().Trim(),
      IdCode = idCode,
      Atoms = atoms
    };
  }

  /// <summary>
  /// Parses a single ATOM or HETATM line from the PDB file.
  /// PDB files contain one or more ATOM/HETATM lines, which have up to 15 fields.
  /// The fields have a fixed-column format. Numerical fields are right-aligned, and string fields are left-aligned.
  /// Examples:
  ///  HETATM    9  O5 ORP A   1      -3.411   0.446   0.342  1.00 15.00           O
  ///  ATOM    145  N  VAL A  25      11.240  19.531  29.178  1.00 24.80           N
  /// </summary>

  /*
  Columns  Length  Format   Field Name       Description
  ----------------------------------------------------------------------------------
   1 -  6    6     String   Record Name      "ATOM  " or "HETATM"
   7 - 11    5     Integer  Atom Serial      Unique atom ID number
  13 - 16    4     String   Atom Name        Atom name (e.g., CA, N, CB)
  17         1     Char     AltLoc           Alternate location indicator
  18 - 20    3     String   Residue Name     3-letter amino acid/nucleic code (e.g., VAL)
  22         1     Char     Chain ID         Chain identifier (e.g., A)
  23 - 26    4     Integer  Residue Sequence Residue sequence number
  27         1     Char     AChar            Code for insertion of residues
  31 - 38    8     Float    X Coordinate     Orthogonal X coordinate in Angstroms (F8.3)
  39 - 46    8     Float    Y Coordinate     Orthogonal Y coordinate in Angstroms (F8.3)
  47 - 54    8     Float    Z Coordinate     Orthogonal Z coordinate in Angstroms (F8.3)
  55 - 60    6     Float    Occupancy        Fractional occupancy (F6.2)
  61 - 66    6     Float    B-factor         Temperature factor in Angstroms^2 (F6.2)
  77 - 78    2     String   Element          Element symbol, right-justified (e.g., N)
  79 - 80    2     String   Charge           Formal charge on atom (e.g., 1+)
  */

  Atom? TryParseAtomLine(string line, bool isHeteroAtom)
  {
    try
    {
      int atomSerial = int.Parse(line[6..11].Trim(), CultureInfo.InvariantCulture);
      string atomName = line[12..16].Trim();
      string residueName = line.Length >= 20 ? line[17..20].Trim() : string.Empty;
      char chainId = line.Length >= 22 ? line[21] : ' ';
      int residueSequence = line.Length >= 26 ? ParseIntOrDefault(line[22..26]) : 0;

      double x = double.Parse(line[30..38].Trim(), CultureInfo.InvariantCulture);
      double y = double.Parse(line[38..46].Trim(), CultureInfo.InvariantCulture);
      double z = double.Parse(line[46..54].Trim(), CultureInfo.InvariantCulture);

      string elementSymbol = line.Length >= 78 ? line[76..78].Trim() : string.Empty;
      if (string.IsNullOrEmpty(elementSymbol))
      {
        // Older PDB files sometimes omit the element column.
        // Best-effort guess from the atom name (imperfect for cases like
        // "CA", which means alpha-carbon in a protein backbone but
        // calcium as a HETATM — good enough for a visualization demo).
        elementSymbol = GuessElementFromAtomName(atomName);
      }

      return new Atom
      {
        SerialNumber = atomSerial,
        Name = atomName,
        Element = elementSymbol,
        ResidueName = residueName,
        ChainId = chainId,
        ResidueSequence = residueSequence,
        Position = new Vector3((float)x, (float)y, (float)z),
        IsHetAtom = isHeteroAtom
      };
    }
    catch
    {
      return null;
    }
  }

  int ParseIntOrDefault(string s)
  {
    return int.TryParse(s.Trim(), out var value) ? value : 0;
  }

  string GuessElementFromAtomName(string atomName)
  {
    var letters = new string(atomName.Where(char.IsLetter).ToArray());
    if (letters.Length == 0) return "X";

    return letters.Length >= 2 && IsKnownTwoLetterElement(letters[..2]) ? letters[..2] : letters[..1];
  }

  bool IsKnownTwoLetterElement(string symbol)
  {
    return KnownTwoLetterElements.Contains(symbol);
  }
}
