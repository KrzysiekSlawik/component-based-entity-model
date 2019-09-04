using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Systems
{
    public class TilemapGrapher : MonoBehaviour
    {
        public Graph graph;
        public Tilemap[] tm;
        private int xSize;
        private int ySize;
        // Start is called before the first frame update
        void Start()
        {
            Bake();
        }
        private void OnValidate()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Bake()
        {
            Bounds b = tm[0].localBounds;
            xSize = (int)b.extents.x;
            ySize = (int)b.extents.y;

            bool[,] walkable = new bool[xSize*2, ySize*2];
            for(int x = 0; x < xSize*2; x++)
            {
                for(int y = 0; y < ySize*2; y++)
                {
                    walkable[x, y] = CheckTilemaps(x-xSize, y-ySize);
                }
            }
            graph = new Graph(walkable, xSize*2, ySize*2);
        }
        public Vector3Int GlobalToLocal(Vector2 glob)
        {
            Debug.Log(tm[0].LocalToCell(new Vector3(glob.x, glob.y, 0)) + new Vector3Int(xSize, ySize, 0));
            return tm[0].LocalToCell(new Vector3(glob.x, glob.y, 0)) + new Vector3Int(xSize, ySize, 0);
        }
        private bool CheckTilemaps(int x, int y)
        {
            foreach(Tilemap map in tm)
            {
                if(map.GetColliderType(new Vector3Int(x, y, 0)) != Tile.ColliderType.None)
                {
                    return false;
                }
            }
            return true;
        }
        public Stack<Vector2> NodeToPath(Node n)
        {
            Stack<Vector2> path = new Stack<Vector2>();
            while (n != null)
            {
                path.Push(tm[0].CellToWorld(n.pos - new Vector3Int(xSize, ySize, 0)) + new Vector3(0.5f, 0.5f, 0));
                n = n.ancestor;
            }
            return path;
        }
    }
}
