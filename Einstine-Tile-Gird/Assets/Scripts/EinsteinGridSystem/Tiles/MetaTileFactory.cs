using UnityEngine;
using DaBu.EGS.GeometryHelper;

namespace DaBu.EGS.Tiles
{
    public class MetaTileFactory
    {
        public MetaTile MetaTileH { get; private set; }
        private MetaTile _metaTileT;
        private MetaTile _metaTileP;
        private MetaTile _metaTileF;

        public MetaTileFactory()
        {
            MetaTilesDefinitions.Initialize();
        }

        public void CreateMetaTiles()
        {
            CreateMetaTileH();
            _metaTileT = new MetaTile(MetaTilesDefinitions.T);
            _metaTileP = new MetaTile(MetaTilesDefinitions.P);
            _metaTileF = new MetaTile(MetaTilesDefinitions.F);
        }

        private void CreateMetaTileH()
        {
            MetaTileH = new MetaTile(MetaTilesDefinitions.H);
            
            MetaTileH.AddChild(5, 7, 5, 0, "H_hat");
            MetaTileH.AddChild(9, 11, 1, 2, "H_hat");
            MetaTileH.AddChild(5, 7, 3, 4, "H_hat");
            
            Matrix4x4 transform = Matrix4x4.Translate(new Vector3(2.5f, Vector2Extensions.Hr3, 0f));

            Matrix4x4 rotation = MatrixExtensions.Affine2D(
                -0.5f, -Vector2Extensions.Hr3, 0f,
                Vector2Extensions.Hr3, -0.5f, 0f
            );

            Matrix4x4 reflection = MatrixExtensions.Affine2D(0.5f, 0f, 0f, 0f, -0.5f, 0f);

            Matrix4x4 h1Transform = transform * rotation * reflection;

            MetaTileH.AddChild(new Tile(h1Transform, "H1_hat", true));
        }
    }
}