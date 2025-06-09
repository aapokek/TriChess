using UnityEngine;

public class GameConstants
{
    // Unit vectors
    public Vector3Int UNITVECTORA = new (1, 0, 0);
    public Vector3Int UNITVECTORB = new (0, 1, 0);
    public Vector3Int UNITVECTORC = new (0, 0, 1);

    public enum PLAYERS
    {
        WhitePlayer,
        BrownPlayer,
        BlackPlayer
    }
}
