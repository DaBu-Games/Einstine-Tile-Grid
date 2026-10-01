using UnityEngine;
using DaBu.EGS.GeometryHelper;
using DaBu.EGS.Tiles;

namespace DaBu.EGS.MetaTiles
{
    public static class MetaTilesDefinitions
    {
        public static MetaTile H;
        public static MetaTile T;
        public static MetaTile P;
        public static MetaTile F;

        public static void Initialize()
        {
            H = new MetaTile(TileType.H, GetHOutline());

            T = new MetaTile(TileType.T, GetTOutline());

            P = new MetaTile(TileType.P, GetPOutline());

            F = new MetaTile(TileType.F, GetFOutline());
        }

        private static Vector2[] GetHOutline()
        {
            return new[]
            {
                new Vector2(0, 0),
                new Vector2(4, 0),
                new Vector2(4.5f, Vector2Extensions.Hr3),
                new Vector2(2.5f, 5 * Vector2Extensions.Hr3),
                new Vector2(1.5f, 5 * Vector2Extensions.Hr3),
                new Vector2(-0.5f, Vector2Extensions.Hr3),
            };
        }

        private static Vector2[] GetTOutline()
        {
            return new[]
            {
                new Vector2(0, 0),
                new Vector2(3, 0),
                new Vector2(1.5f, 3 * Vector2Extensions.Hr3),
            };
        }

        private static Vector2[] GetPOutline()
        {
            return new []
            {
                new Vector2(0, 0),
                new Vector2(4, 0),
                new Vector2(3, 2 * Vector2Extensions.Hr3),
                new Vector2(-1, 2 * Vector2Extensions.Hr3),
            };
        }

        private static Vector2[] GetFOutline()
        {
            return new []
            {
                new Vector2(0, 0),
                new Vector2(3, 0),
                new Vector2(3.5f, Vector2Extensions.Hr3),
                new Vector2(3, 2 * Vector2Extensions.Hr3),
                new Vector2(-1, 2 * Vector2Extensions.Hr3),
            };
        }
    }
}