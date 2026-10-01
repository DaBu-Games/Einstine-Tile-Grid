using UnityEngine;

namespace DaBu.EGS.Tiles
{
    public class Tile
    {
        public Matrix4x4 Transform;
        public float Rotation;
        public bool Reflected;
        public string Name;

        public Tile(Matrix4x4 transform, string name)
        {
            Transform = transform;
            Name = name;
        }
    }
}



