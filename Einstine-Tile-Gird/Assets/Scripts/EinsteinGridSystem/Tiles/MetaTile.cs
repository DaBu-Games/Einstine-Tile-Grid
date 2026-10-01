using System.Collections.Generic;
using DaBu.EGS.GeometryHelper;
using UnityEngine;

namespace DaBu.EGS.Tiles
{
    public class MetaTile
    {
        private Vector2[] _outline;
        public TileType Type { get; private set; }
        public Matrix4x4 Transform { get; private set; }
        public List<Tile> Children { get; private set; }
    
        public MetaTile(TileType type, Vector2[] outline)
        {
            Type = type;
            _outline = outline;
            Children = new List<Tile>();
        }
        
        public void SetTransform(Matrix4x4 transform) => Transform = transform;
        
        private void AddChild(Tile tile) => Children.Add(tile);

        public void AddChild(Matrix4x4 transform)
        {
            AddChild(transform, Type);
        }

        public void AddChild(Matrix4x4 transform, TileType type)
        {
            AddChild(new Tile(transform, type));
        }
        
        public void AddChild(int hatP, int hatQ, int metaP, int metaQ)
        {
            Matrix4x4 transform = MatrixExtensions.MatchTwo(
                Vector2Extensions.hatOutline[hatP],
                Vector2Extensions.hatOutline[hatQ],
                _outline[metaP],
                _outline[metaQ]
            );
            AddChild(new Tile(transform, Type));
        }

        public Vector2 GetWorldPosition(int index)
        {
            return _outline[index].HexPt();
        }
    }
}

