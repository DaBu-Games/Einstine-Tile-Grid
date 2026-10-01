using UnityEngine;
using DaBu.EGS.GeometryHelper;

namespace DaBu.EGS.Tiles
{
    public class MetaTileFactory
    {
        public MetaTile MetaTileH { get; private set; }
        public MetaTile MetaTileT { get; private set; }
        public MetaTile MetaTileP { get; private set; }
        public MetaTile MetaTileF { get; private set; }

        public MetaTileFactory()
        {
            MetaTilesDefinitions.Initialize();
        }

        public void CreateMetaTiles()
        {
            CreateMetaTileH();
            CreateMetaTileT();
            CreateMetaTileP();
            CreateMetaTileF();
        }

        private void CreateMetaTileH()
        {
            MetaTileH = MetaTilesDefinitions.H;
            
            MetaTileH.AddChild(5, 7, 5, 0);
            MetaTileH.AddChild(9, 11, 1, 2);
            MetaTileH.AddChild(5, 7, 3, 4);
            
            Matrix4x4 transform = Matrix4x4.Translate(new Vector3(2.5f, Vector2Extensions.Hr3, 0f));

            Matrix4x4 rotation = MatrixExtensions.Affine2D(
                -0.5f, -Vector2Extensions.Hr3, 0f,
                Vector2Extensions.Hr3, -0.5f, 0f
            );

            Matrix4x4 reflection = MatrixExtensions.Affine2D(0.5f, 0f, 0f, 0f, -0.5f, 0f);

            Matrix4x4 h1Transform = transform * rotation * reflection;

            MetaTileH.AddChild(h1Transform, TileType.H1);
        }

        private void CreateMetaTileT()
        {
            MetaTileT = MetaTilesDefinitions.T;

            Matrix4x4 transform = MatrixExtensions.Affine2D(
                0.5f, 0f, 0.5f,
                0f, 0.5f, Vector2Extensions.Hr3
            );

            MetaTileT.AddChild(transform);
        }

        private void CreateMetaTileP()
        {
            MetaTileP = MetaTilesDefinitions.P;
            
            Matrix4x4 transform = MatrixExtensions.Affine2D(
                0.5f, 0f, 1.5f,
                0f, 0.5f, Vector2Extensions.Hr3
            );

            MetaTileP.AddChild(transform);
            
            Matrix4x4 transform2 = Matrix4x4.Translate(new Vector3(0f, 2f * Vector2Extensions.Hr3, 0f));
            
            Matrix4x4 rotation = MatrixExtensions.Affine2D(
                0.5f, Vector2Extensions.Hr3, 0f,
                -Vector2Extensions.Hr3, 0.5f, 0f
            );

            Matrix4x4 reflection = MatrixExtensions.Affine2D(0.5f, 0f, 0f, 0f, 0.5f, 0f);

            Matrix4x4 endTransform = transform2 * rotation * reflection;

            MetaTileP.AddChild(endTransform);
        }

        private void CreateMetaTileF()
        {
            MetaTileF = MetaTilesDefinitions.F;
            
            Matrix4x4 transform = MatrixExtensions.Affine2D(
                0.5f, 0f, 1.5f,
                0f, 0.5f, Vector2Extensions.Hr3
            );

            MetaTileF.AddChild(transform);
            
            Matrix4x4 transform2 = Matrix4x4.Translate(new Vector3(0f, 2f * Vector2Extensions.Hr3, 0f));
            
            Matrix4x4 rotation = MatrixExtensions.Affine2D(
                0.5f, Vector2Extensions.Hr3, 0f,
                -Vector2Extensions.Hr3, 0.5f, 0f
            );

            Matrix4x4 reflection = MatrixExtensions.Affine2D(0.5f, 0f, 0f, 0f, 0.5f, 0f);

            Matrix4x4 endTransform = transform2 * rotation * reflection;

            MetaTileF.AddChild(endTransform);
        }
    }
}