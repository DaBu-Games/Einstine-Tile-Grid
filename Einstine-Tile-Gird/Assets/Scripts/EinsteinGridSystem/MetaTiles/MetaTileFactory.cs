using UnityEngine;
using DaBu.EGS.GeometryHelper;
using DaBu.EGS.Tiles;

namespace DaBu.EGS.MetaTiles
{
    public class MetaTileFactory
    {
        private MetaTile MetaTileH;
        private MetaTile MetaTileT;
        private MetaTile MetaTileP;
        private MetaTile MetaTileF;

        public MetaTileFactory()
        {
            MetaTilesDefinitions.Initialize();
            CreateMetaTiles();
        }

        public MetaTile GetSuperTile(TileType tileType, int level)
        {
            MetaTile h = MetaTileH;
            MetaTile t = MetaTileT;
            MetaTile p = MetaTileP;
            MetaTile f = MetaTileF;
            
            for (int i = 0; i < level; i++)
            {
                MetaTilePatch patch = ConstructPatch(h, t, p, f); 

                h = CreateSuperTileH(patch);
                t = CreateSuperTileT(patch, h);
                p = CreateSuperTileP(patch);
                f = CreateSuperTileF(patch);
                
                h.Recenter();
                p.Recenter();
                f.Recenter();
                t.Recenter();
            }

            if (tileType == TileType.H)
                return h;

            if (tileType == TileType.T)
                return t;

            if (tileType == TileType.P)
                return p;

            if (tileType == TileType.F)
                return f;

            return null;
        }

        public MetaTilePatch ConstructPatch(MetaTile h, MetaTile t, MetaTile p, MetaTile f)
        {
            MetaTilePatch metaTilePatch = new MetaTilePatch();
            
            metaTilePatch.AddChild(h.Clone(Matrix4x4.identity));
            
            AddAgainstEdge(metaTilePatch, p, 0, 0, 2); 
            AddAgainstEdge(metaTilePatch, h, 1, 0, 2);
            AddAgainstEdge(metaTilePatch, p, 2, 0, 2);
            AddAgainstEdge(metaTilePatch, h, 3, 0, 2);
            AddAgainstEdge(metaTilePatch, p, 4, 4, 2);

            AddAgainstEdge(metaTilePatch, f, 0, 4, 3);
            AddAgainstEdge(metaTilePatch, f, 2, 4, 3);

            AddAgainstChildren(metaTilePatch, f, 4, 1, 3, 2, 0);

            AddAgainstEdge(metaTilePatch, h, 8, 3, 0);
            AddAgainstEdge(metaTilePatch, p, 9, 2, 0);
            AddAgainstEdge(metaTilePatch, h, 10, 2, 0);
            AddAgainstEdge(metaTilePatch, p, 11, 4, 2);
            AddAgainstEdge(metaTilePatch, h, 12, 0, 2);
            AddAgainstEdge(metaTilePatch, f, 13, 0, 3);
            AddAgainstEdge(metaTilePatch, f, 14, 2, 1);
            AddAgainstEdge(metaTilePatch, h, 15, 3, 4);
            AddAgainstEdge(metaTilePatch, f, 8, 2, 1);
            AddAgainstEdge(metaTilePatch, h, 17, 3, 0);
            AddAgainstEdge(metaTilePatch, p, 18, 2, 0);
            AddAgainstEdge(metaTilePatch, h, 19, 2, 2);
            AddAgainstEdge(metaTilePatch, f, 20, 4, 3); 
            AddAgainstEdge(metaTilePatch, p, 20, 0, 2);
            AddAgainstEdge(metaTilePatch, h, 22, 0, 2);
            AddAgainstEdge(metaTilePatch, f, 23, 4, 3);
            AddAgainstEdge(metaTilePatch, f, 23, 0, 3);
            AddAgainstEdge(metaTilePatch, p, 16, 0, 2);

            AddAgainstChildren(metaTilePatch, t, 9, 4, 0, 2, 2);

            AddAgainstEdge(metaTilePatch, f, 4, 0, 3);
            
            return metaTilePatch;
        }

        private MetaTile CreateSuperTileH(MetaTilePatch patch)
        {
            Vector2 bps1 = patch.Childeren[8].EvalPoint(2);
            Vector2 bps2 = patch.Childeren[21].EvalPoint(2);

            // Rotate bps2 around bps1 by -120 degrees.
            Matrix4x4 rotation120 = MatrixExtensions.Affine2D(
                -0.5f, Vector2Extensions.Hr3, 0f,
                -Vector2Extensions.Hr3, -0.5f, 0f
            );

            Vector2 relative = bps2 - bps1;

            Vector3 rotated = rotation120.MultiplyVector(
                new Vector3(relative.x, relative.y, 0f)
            );

            Vector2 rbps = bps1 + new Vector2(rotated.x, rotated.y);

            Vector2 p6 = patch.Childeren[6].EvalPoint(2);
            Vector2 p7 = patch.Childeren[7].EvalPoint(2);

            Vector2 llc = Vector2Extensions.Intersect(
                bps1,
                rbps,
                p6,
                p7
            );

            Vector2 w = p6 - llc;

            // trot(-PI/3)
            Matrix4x4 rotation60 = MatrixExtensions.Affine2D(
                0.5f, Vector2Extensions.Hr3, 0f,
                -Vector2Extensions.Hr3, 0.5f, 0f
            );

            Vector3 rotatedW = rotation60.MultiplyVector(
                new Vector3(w.x, w.y, 0f)
            );

            w = new Vector2(rotatedW.x, rotatedW.y);

            Vector2 p14 = patch.Childeren[14].EvalPoint(2);

            Vector2[] outline = { llc, bps1, bps1 + w, p14, p14 - w, p6 };

            MetaTile superH = new MetaTile(TileType.H, outline);

            int[] children = { 0, 9, 16, 27, 26, 6, 1, 8, 10, 15 };

            foreach (int childIndex in children)
            {
                MetaTile child = patch.Childeren[childIndex];

                foreach (Tile tile in child.Children)
                {
                    Matrix4x4 transform = child.Transform * tile.Transform;

                    superH.AddChild(transform, tile.TileType);
                }
            }

            return superH;
        }
        
        private MetaTile CreateSuperTileP(MetaTilePatch patch)
        {
            Vector2 bps1 = patch.Childeren[8].EvalPoint(2);

            Vector2 p72 = patch.Childeren[7].EvalPoint(2);
    
            Vector2 llc = patch.Childeren[6].EvalPoint(2);

            Vector2[] outline = { p72, p72 + (bps1 - llc), bps1, llc };

            MetaTile superP = new MetaTile(TileType.P, outline);

            int[] children = { 7, 2, 3, 4, 28 };

            foreach (int childIndex in children)
            {
                MetaTile child = patch.Childeren[childIndex];

                foreach (Tile tile in child.Children)
                {
                    Matrix4x4 transform = child.Transform * tile.Transform;
                    superP.AddChild(transform, tile.TileType);
                }
            }

            return superP;
        }
        
        private MetaTile CreateSuperTileF(MetaTilePatch patch)
        {
            Vector2 bps1 = patch.Childeren[8].EvalPoint(2);
            Vector2 bps2 = patch.Childeren[21].EvalPoint(2);

            Vector2 p252 = patch.Childeren[25].EvalPoint(2);

            Vector2 llc = patch.Childeren[6].EvalPoint(2);

            Vector2 p24 = patch.Childeren[24].EvalPoint(2);
            Vector2 p25 = patch.Childeren[25].EvalPoint(0);

            Vector2[] outline = { bps2, p24, p25, p252, p252 + (llc - bps1) };

            MetaTile superF = new MetaTile(TileType.F, outline);

            int[] children = { 21, 20, 22, 23, 24, 25 };

            foreach (int childIndex in children)
            {
                MetaTile child = patch.Childeren[childIndex];

                foreach (Tile tile in child.Children)
                {
                    Matrix4x4 transform = child.Transform * tile.Transform;
                    superF.AddChild(transform, tile.TileType);
                }
            }

            return superF;
        }
        
        private MetaTile CreateSuperTileT(MetaTilePatch patch, MetaTile h)
        {
            Vector2 AAA = h.GetPoint(2);
            Vector2 BBB = h.GetPoint(1) + (h.GetPoint(4) - h.GetPoint(5));
            
            Matrix4x4 rotation60 = MatrixExtensions.Affine2D(
                0.5f, Vector2Extensions.Hr3, 0f,
                -Vector2Extensions.Hr3, 0.5f, 0f
            );

            Vector2 relative = AAA - BBB;

            Vector3 rotated = rotation60.MultiplyVector(new Vector3(relative.x, relative.y, 0f));

            Vector2 CCC = BBB + new Vector2(rotated.x, rotated.y);

            Vector2[] outline = { BBB, CCC, AAA };

            MetaTile superT = new MetaTile(TileType.T, outline);

            MetaTile child = patch.Childeren[11];

            foreach (Tile tile in child.Children)
            {
                Matrix4x4 transform = child.Transform * tile.Transform;
                superT.AddChild(transform, tile.TileType);
            }

            return superT;
        }

        private void AddAgainstEdge(MetaTilePatch patch, MetaTile source, int childIndex, int childEdge, int sourceEdge)
        {
            MetaTile existing = patch.Childeren[childIndex];
            
            int nextEdge = (childEdge + 1) % existing.OutlineCount;
            
            Vector2 p = existing.EvalPoint(nextEdge);
            Vector2 q = existing.EvalPoint(childEdge);
            
            int sourceNextEdge = (sourceEdge + 1) % source.OutlineCount;

            Vector2 a = source.GetPoint(sourceEdge);
            Vector2 b = source.GetPoint(sourceNextEdge);

            Matrix4x4 transform = MatrixExtensions.MatchTwo(a, b, p, q);

            patch.AddChild(source.Clone(transform));
        }

        private void AddAgainstChildren(MetaTilePatch patch, MetaTile newMetaTile, int childPIndex, int childPEdge, int childQIndex, int childQEdge, int newEdge)
        {
            MetaTile childP = patch.Childeren[childPIndex];
            MetaTile childQ = patch.Childeren[childQIndex];

            Vector2 p = childQ.EvalPoint(childQEdge);
            Vector2 q = childP.EvalPoint(childPEdge);

            Vector2 a = newMetaTile.GetPoint(newEdge);
            Vector2 b = newMetaTile.GetPoint((newEdge + 1) %  newMetaTile.OutlineCount);

            Matrix4x4 transform = MatrixExtensions.MatchTwo(a, b, p, q);

            patch.AddChild(newMetaTile.Clone(transform));
        }
        
        private void CreateMetaTiles()
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