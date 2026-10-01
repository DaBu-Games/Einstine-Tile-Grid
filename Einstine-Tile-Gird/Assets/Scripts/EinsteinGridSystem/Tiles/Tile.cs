using UnityEngine;

namespace DaBu.EGS.Tiles
{
    public class Tile
    {
        public Matrix4x4 Transform;
        public TileType TileType;

        public Tile(Matrix4x4 transform, TileType type)
        {
            Transform = transform;
            TileType = type;
        }
        
        public void SetTransform(Matrix4x4 transform) => Transform = transform;
    }
}



