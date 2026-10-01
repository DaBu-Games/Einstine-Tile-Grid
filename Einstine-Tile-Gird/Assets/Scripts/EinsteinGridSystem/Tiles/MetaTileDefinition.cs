using UnityEngine;
using DaBu.EGS.GeometryHelper;

namespace DaBu.EGS.Tiles
{
    [System.Serializable]
    public class MetaTileDefinition
    {
        public MetaTileType Type;
        private Vector2[] _outline;

        public MetaTileDefinition(MetaTileType type, Vector2[] outline)
        {
            Type = type;
            _outline = outline;
        }

        public Vector2 Outline(int index)
        {
            return _outline[index];
        }

        public Vector2 GetWorldPosition(int index, float cellSize)
        {
            return _outline[index].HexPt(cellSize);
        }
    }
}