using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int cubePosition; // (x, y, z) cube coordinates
    public string tileColor; // "White", "Black", or "Brown"
    public bool isOccupied; // Whether a piece is on this tile
    private Material originalMaterial;
    public Material highlightMaterial; // A bright material is assigned in the Inspector

    
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }


    public void Highlight()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }


    public void ClearHighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}
