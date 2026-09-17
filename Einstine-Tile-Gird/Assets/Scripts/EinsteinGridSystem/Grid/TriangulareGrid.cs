using System.Collections.Generic;
using DaBu.EGS.GeometryHelper;
using UnityEngine;

namespace DaBu.EGS.Grid
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TriangulareGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] private float cellSize = 1f;

        private Vector2[] grid;
        
        private MeshFilter meshFilter;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();

            GenerateGrid();
            GenerateMesh();
        }

        private void GenerateGrid()
        {
            int width = gridSize.x;
            int height = gridSize.y;
        
            grid = new Vector2[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 gridPosition = new Vector2Int(x, y);
                    grid[y * width + x] = gridPosition.HexPt(cellSize);
                }
            }
            
           
        }

        private void GenerateMesh()
        {
            int width = gridSize.x;
            int height = gridSize.y;

            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> colors = new List<Color>();

            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int aIndex = y * width + x;
                    int bIndex = aIndex + 1;
                    int cIndex = (y + 1) * width + x;
                    int dIndex = cIndex + 1;

                    
                    Vector3 a = new Vector3(grid[aIndex].x, 0f, grid[aIndex].y);
                    Vector3 b = new Vector3(grid[bIndex].x, 0f, grid[bIndex].y);
                    Vector3 c = new Vector3(grid[cIndex].x, 0f, grid[cIndex].y);
                    Vector3 d = new Vector3(grid[dIndex].x, 0f, grid[dIndex].y);

                    // First triangle
                    AddTriangleAsKites(
                        a, c, b,
                        vertices,
                        triangles,
                        colors
                    );

                    // Second triangle
                    AddTriangleAsKites(
                        b, c, d,
                        vertices,
                        triangles,
                        colors
                    );
                }
            }

            Mesh mesh = new Mesh();
            mesh.name = "Triangular Grid";

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetColors(colors);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.mesh = mesh;
        }
        
        private void AddTriangleAsKites(Vector3 a, Vector3 b, Vector3 c, List<Vector3> vertices, List<int> triangles, List<Color> colors)
        {
            Vector3 center = (a + b + c) / 3f;

            Vector3 ab = (a + b) / 2f;
            Vector3 bc = (b + c) / 2f;
            Vector3 ca = (c + a) / 2f;

            // Kite at A
            AddKite(a, ab, center, ca, vertices, triangles, colors, Color.red);

            // Kite at B
            AddKite(b, bc, center, ab, vertices, triangles, colors, Color.green);

            // Kite at C
            AddKite(c, ca, center, bc, vertices, triangles, colors, Color.blue);
        }
        
        private void AddKite(Vector3 a, Vector3 b, Vector3 center, Vector3 c, List<Vector3> vertices, List<int> triangles, List<Color> colors, Color color)
        {
            int index = vertices.Count;

            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(center);
            vertices.Add(c);

            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);

            // First half of kite
            triangles.Add(index);
            triangles.Add(index + 1);
            triangles.Add(index + 2);

            // Second half of kite
            triangles.Add(index);
            triangles.Add(index + 2);
            triangles.Add(index + 3);
        }
    }
}

