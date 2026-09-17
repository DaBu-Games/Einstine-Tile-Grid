using System.Collections.Generic;
using UnityEngine;

public class MetaTile
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    
    public MetaTileType Type { get; private set; }
    public int Level { get; private set; }
    public List<MetaTile> Children { get; private set; }

    public MetaTile(MetaTileType type, int level)
    {
        Type = type;
        Level = level;
        Children = new List<MetaTile>();
    }
    
    public void AddTile(MetaTile tile) => Children.Add(tile);
}
