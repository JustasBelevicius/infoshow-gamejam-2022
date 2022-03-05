using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileTypeToTileBase", menuName = "TileTypeToTileBase", order = 1)]
public class TileTypeToTileBase : ScriptableObject
{
    public TileType tileType;
    public TileBase tileBase;
}