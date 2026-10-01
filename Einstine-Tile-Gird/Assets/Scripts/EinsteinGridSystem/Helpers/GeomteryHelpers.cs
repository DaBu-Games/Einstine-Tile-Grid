using DaBu.EGS.Grid;
using UnityEngine;

namespace DaBu.EGS.GeometryHelper
{
    public static class Vector2Extensions
    {
        public static readonly float Hr3 = Mathf.Sqrt(3f) / 2f;
        public static readonly Vector2[] hatOutline =
        {
            new Vector2(0, 0).HexPt(),
            new Vector2(-1, -1).HexPt(),
            new Vector2(0, -2).HexPt(),
            new Vector2(2, -2).HexPt(),
            new Vector2(2, -1).HexPt(),
            new Vector2(4, -2).HexPt(),
            new Vector2(5, -1).HexPt(),
            new Vector2(4, 0).HexPt(),
            new Vector2(3, 0).HexPt(),
            new Vector2(2, 2).HexPt(),
            new Vector2(0, 3).HexPt(),
            new Vector2(0, 2).HexPt(),
            new Vector2(-1, 2).HexPt()
        };
        
        public static Vector2 HexPt(this Vector2 point)
        {
            return new Vector2(
                (point.x + 0.5f * point.y) * EinstineGrid.CellSize,
                (Hr3 * point.y) * EinstineGrid.CellSize
            );
        }

        public static Vector2 Intersect(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            float d = (q2.y - p2.y) * (q1.x - p1.x) - (q2.x - p2.x) * (q1.y - p1.y);

            float uA = ((q2.x - p2.x) * (p1.y - p2.y) - (q2.y - p2.y) * (p1.x - p2.x)) / d;

            return new Vector2(p1.x + uA * (q1.x - p1.x), p1.y + uA * (q1.y - p1.y));
        }
    }

    public static class MatrixExtensions
    {
        public static Matrix4x4 MatchSeg(Vector2 p, Vector2 q)
        {
            float dx = q.x - p.x;
            float dy = q.y - p.y;
            
            Matrix4x4 matrix = Matrix4x4.identity;

            matrix.m00 = dx;
            matrix.m01 = -dy;
            matrix.m03 = p.x;
            
            matrix.m10 = dy;
            matrix.m11 = dx;
            matrix.m13 = p.y;
            
            return matrix;
        }
        
        // constructs a 2D transformation matrix that combines two transform points in to one
        public static Matrix4x4 Affine2D(float m00, float m01, float tx, float m10, float m11, float ty)
        {
            Matrix4x4 matrix = Matrix4x4.identity;

            matrix.m00 = m00;
            matrix.m01 = m01;
            matrix.m03 = tx;

            matrix.m10 = m10;
            matrix.m11 = m11;
            matrix.m13 = ty;

            return matrix;
        }

        public static Matrix4x4 MatchTwo(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            Matrix4x4 from = MatchSeg(p1, q1);
            Matrix4x4 to = MatchSeg(p2, q2);
            
            return to * from.inverse;
        }
    }
}