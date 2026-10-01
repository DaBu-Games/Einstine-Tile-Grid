using System.Collections.Generic;
using DaBu.EGS.GeometryHelper;
using UnityEngine;

namespace DaBu.EGS.Tiles
{
    public class MetaTile
    {
        private MetaTileDefinition _definition;
        public List<Tile> Children { get; private set; }
    
        public MetaTile(MetaTileDefinition definition)
        {
            _definition = definition;
            Children = new List<Tile>();
        }
        
        public void AddChild(Tile tile) => Children.Add(tile);
        
        public void AddChild(int hatP, int hatQ, int metaP, int metaQ, string name, bool reflected = false)
        {
            Matrix4x4 transform = MatrixExtensions.MatchTwo(
                Vector2Extensions.hatOutline[hatP],
                Vector2Extensions.hatOutline[hatQ],
                _definition.Outline(metaP),
                _definition.Outline(metaQ)
            );
            AddChild(new Tile(transform, name, reflected));
        }
    }
}

