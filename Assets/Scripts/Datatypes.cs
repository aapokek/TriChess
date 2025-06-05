using UnityEngine;
using System.Collections.Generic;

public static class Datatypes
{
    // Dictionary types
    public class TileMap : Dictionary<Vector3Int, Tile> { }
    public class PieceMap : Dictionary<Vector3Int, Piece> { }
}
