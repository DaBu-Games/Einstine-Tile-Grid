using UnityEngine;

namespace DaBu.EGS.GeometryHelper
{
    public static class Vector2Extensions
    {
        public static Vector2 HexPt(this Vector2 point, float cellSize = 1f)
        {
            return new Vector2(
                (point.x + 0.5f * point.y) * cellSize,
                (Mathf.Sqrt(3f) / 2f * point.y) * cellSize
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

        public static Matrix4x4 MatchTwo(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            Matrix4x4 from = MatchSeg(p1, q1);
            Matrix4x4 to = MatchSeg(p2, q2);
            
            return from * to;
        }
    }
}