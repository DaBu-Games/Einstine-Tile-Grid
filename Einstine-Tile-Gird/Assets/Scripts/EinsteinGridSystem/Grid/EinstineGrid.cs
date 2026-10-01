using System.Collections.Generic;
using DaBu.EGS.GeometryHelper;
using DaBu.EGS.Tiles;
using UnityEngine;

namespace DaBu.EGS.Grid
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class EinstineGrid : MonoBehaviour
    {
        [SerializeField] private float cellSize = 1f;
        
        private MetaTileFactory tileFactory;
            
        private MeshFilter meshFilter;

        private void Start()
        {
            tileFactory = new MetaTileFactory();
            meshFilter = GetComponent<MeshFilter>();
            
            GenerateMesh();
        }
        
        private void GenerateMesh()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> colors = new List<Color>();
            
            tileFactory.CreateMetaTiles();

            foreach (Tile child in tileFactory.MetaTileH.Children)
            {
                AddHat(child.Transform, vertices, triangles, colors);
            }

            Mesh mesh = new Mesh();
            mesh.name = "Hat";

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetColors(colors);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.mesh = mesh;
        }

        private void AddHat(Matrix4x4 transform, List<Vector3> vertices, List<int> triangles, List<Color> colors)
        {
            int startIndex = vertices.Count;

            foreach (Vector2 point in Vector2Extensions.hatOutline)
            {
                Vector2 transformed = transform.MultiplyPoint3x4(new Vector3(point.x, point.y, 0f));
                
                Vector2 worldPoint = transformed.HexPt(cellSize);
                
                vertices.Add(new Vector3(worldPoint.x, 0, worldPoint.y));
            }
            
            TriangulateHat(startIndex, triangles);
        }
        
        private void TriangulateHat(int startIndex, List<int> triangles)
        {
            int count = Vector2Extensions.hatOutline.Length;

            List<int> remaining = new List<int>();

            for (int i = 0; i < count; i++)
            {
                remaining.Add(i);
            }

            while (remaining.Count > 3)
            {
                bool triangleFound = false;

                for (int i = 0; i < remaining.Count; i++)
                {
                    int previousIndex = remaining[(i - 1 + remaining.Count) % remaining.Count];

                    int currentIndex = remaining[i];

                    int nextIndex = remaining[(i + 1) % remaining.Count];

                    Vector2 a = Vector2Extensions.hatOutline[previousIndex];

                    Vector2 b = Vector2Extensions.hatOutline[currentIndex];

                    Vector2 c = Vector2Extensions.hatOutline[nextIndex];

                    if (!IsConvex(a, b, c))
                        continue;

                    bool containsPoint = false;

                    for (int j = 0; j < remaining.Count; j++)
                    {
                        int testIndex = remaining[j];

                        if (testIndex == previousIndex ||
                            testIndex == currentIndex ||
                            testIndex == nextIndex)
                            continue;

                        Vector2 point = Vector2Extensions.hatOutline[testIndex];

                        if (PointInTriangle(point, a, b, c))
                        {
                            containsPoint = true;
                            break;
                        }
                    }

                    if (containsPoint)
                        continue;

                    triangles.Add(startIndex + previousIndex);
                    triangles.Add(startIndex + currentIndex);
                    triangles.Add(startIndex + nextIndex);

                    remaining.RemoveAt(i);

                    triangleFound = true;
                    break;
                }

                if (!triangleFound)
                {
                    Debug.LogError("Could not triangulate hat.");
                    return;
                }
            }

            // Last triangle
            triangles.Add(startIndex + remaining[0]);
            triangles.Add(startIndex + remaining[1]);
            triangles.Add(startIndex + remaining[2]);
        }
        
        private bool IsConvex(Vector2 a, Vector2 b, Vector2 c)
        {
            float cross = (b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x);

            return cross > 0;
        }
        
        private bool PointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        {
            float cross1 = Cross(a, b, p);

            float cross2 = Cross(b, c, p);

            float cross3 = Cross(c, a, p);

            bool hasNegative = cross1 < 0 || cross2 < 0 || cross3 < 0;

            bool hasPositive = cross1 > 0 || cross2 > 0 || cross3 > 0;

            return !(hasNegative && hasPositive);
        }
        private float Cross(Vector2 a, Vector2 b, Vector2 c)
        {
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        }
    }
}