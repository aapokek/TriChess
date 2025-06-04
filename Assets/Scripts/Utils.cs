using UnityEngine;

/// <summary>
/// Provides static utility methods for converting between world positions and cube coordinates in 
/// a hexagonal grid system.
/// </summary>
/// <remarks>
/// The conversion between world positions and cube coordinates is based on the mathematical 
/// foundation of hexagonal grids using cube coordinates. 
/// 
/// The conversion MATH is as followed:
/// 
///     The basis vectors for the grid are:
///         a0 = (0, 0.5)
///         b0 = (cos(pi / 6) / 2, -sin(pi / 6) / 2)
///         c0 = (-cos(pi / 6) / 2, -sin(pi / 6) / 2)
///
///     Coordinate (x, y) is a linear combination of these unit vectors:
///         (x, y) = a * a0 + b * b0 + c * c0
///     
///     Given the condition a + b + c = 0 we get c = -a - b, hence
///         (x, y) = a * a0 + b * b0 + (-a - b) * c0
///     
///     Computing this with unit vector values and replacing y with z (for Unity) gives
///         x = sqrt(3) / 2 * (a / 2 + b)
///         z = 3 / 4 * a
///         
///     Furthermore, solving for a, b and c gives
///         a = 4 / 3 * y
///         b = 2 / sqrt(3) * x - 2 / 3 * y
///         c = -a - b
///
/// The methods include adjustments for the grid's origin to ensure accurate conversions.
/// </remarks>

public class Utils
{
    /// <summary>
    /// Converts a 3D world position to a cube coordinate in a hexagonal grid system. See the MATH 
    /// above.
    /// </summary>
    /// <param name="worldPos">The world position to convert.</param>
    /// <returns>
    /// A Vector3Int representing the corresponding cube coordinates (a, b, c).
    /// </returns>
    /// <remarks>
    /// Assumes a specific layout and origin correction for accurate conversion.
    /// </remarks>
    public static Vector3Int WorldToCubePosition(Vector3 worldPos)
    {
        float x = worldPos.x;
        float z = worldPos.z + 0.5f; // Correct the origin position

        int a = Mathf.RoundToInt((4.0f / 3.0f) * z);
        int b = Mathf.RoundToInt((2.0f / Mathf.Sqrt(3f)) * x - (2.0f / 3.0f) * z);
        int c = -a - b;

        return new Vector3Int(a, b, c);
    }

    /// <summary>
    /// Converts cube coordinates from a hexagonal grid to a 3D world position. See the MATH above.
    /// </summary>
    /// <param name="cubePos">The cube coordinates (a, b, c) to convert.
    /// </param>
    /// <returns>
    /// A Vector3 representing the corresponding world position, with y set to 0 for a flat board.
    /// </returns>
    /// <remarks>
    /// Applies layout-specific scaling and origin correction during conversion.
    /// </remarks>
    public static Vector3 CubeToWorldPosition(Vector3Int cubePos)
    {
        int a = cubePos.x;
        int b = cubePos.y;
        int c = cubePos.z;

        float x = Mathf.Sqrt(3) / 2.0f * (a / 2.0f + b);
        float z = 3.0f / 4.0f * a - 0.5f; // Correct the origin position

        return new Vector3(x, 0, z); // y = 0 assuming flat board
    }
}
