using UnityEngine;

namespace DaBu.EGS.Tiles
{
    public class Tile
    {
        public Matrix4x4 Transform;
        public bool Reflected;
        public string Name;

        public Tile(Matrix4x4 transform, string name, bool reflected = false)
        {
            Transform = transform;
            Name = name;
            Reflected = reflected;
        }
    }
}



