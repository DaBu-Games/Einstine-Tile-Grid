using System;
using DaBu.EGS.GeometryHelper;
using UnityEngine;

namespace DaBu.EGS.Grid
{
    public class TriangulareGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] private float cellSize = 1f;

        private Vector2[] grid = null;

        private void Start()
        {
            GenerateGrid();
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
        
        private void OnDrawGizmos()
        { 
            if (grid == null || !Application.isPlaying)
                return;
            
            Gizmos.color = Color.red;

            foreach (Vector2 point in grid)
            {
                Gizmos.DrawSphere(
                    new Vector3(point.x, 0f, point.y),
                    0.05f
                );
            }

            Gizmos.color = Color.black; 
            int width = gridSize.x;
            int height = gridSize.y;
            
            // vertical lines
            for (int x = 0; x < width; x++)
            {
                DrawLine(grid[x], grid[(height - 1) * width + x]);
                DrawLine(grid[(height - 1) * (width) + x], grid[x * width + (width - 1)]);
            }
            
            // horizontal lines
            for (int y = 0; y < height; y++)
            {
                DrawLine(grid[y * width], grid[y * width + (width - 1)]);
                DrawLine(grid[y * width], grid[y]);
            }
        }
        
        private void DrawLine(Vector2 a, Vector2 b)
        {
            Gizmos.DrawLine(
                new Vector3(a.x, 0, a.y),
                new Vector3(b.x, 0, b.y)
            );
        }
    }
}

