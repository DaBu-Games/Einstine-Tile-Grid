using UnityEngine;
using DaBu.EGS.GeometryHelper;

namespace DaBu.EGS.Tiles
{
    public static class MetaTilesDefinitions
    {
        public static MetaTileDefinition H;
        public static MetaTileDefinition T;
        public static MetaTileDefinition P;
        public static MetaTileDefinition F;

        public static void Initialize()
        {
            H = new MetaTileDefinition(MetaTileType.H, GetHOutline());

            T = new MetaTileDefinition(MetaTileType.T, GetTOutline());

            P = new MetaTileDefinition(MetaTileType.P, GetPOutline());

            F = new MetaTileDefinition(MetaTileType.F, GetFOutline());
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