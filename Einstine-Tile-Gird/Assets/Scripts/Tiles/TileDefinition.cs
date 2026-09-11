using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TileDefinition", menuName = "Einstein/Tile Definition")]
public class TileDefinition : ScriptableObject
{
    public MetaTileType Type;
    public List<Tile> Tiles;
}
