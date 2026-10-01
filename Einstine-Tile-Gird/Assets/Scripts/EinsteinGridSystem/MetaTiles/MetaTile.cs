using System.Collections.Generic;
using DaBu.EGS.GeometryHelper;
using DaBu.EGS.Tiles;
using UnityEngine;

namespace DaBu.EGS.MetaTiles
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
        
        public MetaTile Clone(Matrix4x4 transform)
        {
            MetaTile clone = new MetaTile(Type, (Vector2[])_outline.Clone());

            foreach (Tile child in Children)
            {
                clone.AddChild(child.Transform, child.TileType);
            }

            clone.SetTransform(transform);

            return clone;
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

        public Vector2 GetPoint(int index)
        {
            return _outline[index];
        }
        
        public Vector2 EvalPoint(int index)
        {
            Vector3 result = Transform.MultiplyPoint3x4(new Vector3(_outline[index].x, _outline[index].y, 0f));

            return new Vector2(result.x, result.y);
        }
        
        public int OutlineCount => _outline.Length;
        
        public void Recenter()
        {
            Vector2 center = Vector2.zero;

            foreach (Vector2 point in _outline)
            {
                center += point;
            }
            
            center /= _outline.Length;

            for (int i = 0; i < _outline.Length; i++)
            {
                _outline[i] -= center;
            }

            Matrix4x4 translation = Matrix4x4.Translate(new Vector3(-center.x, -center.y, 0f));

            foreach (Tile child in Children)
            {
                child.SetTransform(translation * child.Transform);
            }
        }
    }
}

