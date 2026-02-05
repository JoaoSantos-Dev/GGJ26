 using System;
 using UnityEngine;
 using UnityEngine.Serialization;
 using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace LevelSystem
{
    [Serializable]
    public abstract class BaseTiles
    {
        protected  Vector2Int size;
        [SerializeField] protected Tilemap tilemap;
        [SerializeField] protected TileBase[] tiles;
        [SerializeField, Range(0,0.3f)] protected float randomMagnitude = 0.05f;
        public void Initialize(Vector2Int size)
        {
            this.size = size;
            GenerateTiles();
            
        }
        protected abstract void GenerateTiles();

        protected void SetRandomTile(int x, int y)
        {
            TileBase randomTile = tiles[Random.Range(0, tiles.Length)];
            Vector3Int position = new Vector3Int(x, y, 0);
            var randomOffset = Random.insideUnitCircle*randomMagnitude; 
            var randomOffsetTransform = Matrix4x4.Translate(randomOffset);
            var tileChangeData = new TileChangeData()
            {
                position = position, 
                transform = randomOffsetTransform,
                tile =  randomTile


            };
            
            tilemap.SetTile(tileChangeData,false);
            
        }
        
    }

    [Serializable]
    public class FloorTiles : BaseTiles
    {
        
        protected override void GenerateTiles()
        {
            GenerateFloorTiles();
        }

        public void GenerateFloorTiles()
        {
            tilemap.ClearAllTiles();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    SetRandomTile(i,j);
                  
                }
            }
        }
        
    }
    
      
     [Serializable]
     public class WallTiles : BaseTiles
     {
         [SerializeField, Range(1,4)] private int width = 2;
        protected override void GenerateTiles()
        {
            randomMagnitude = 0;
            GenerateWalls();
        }
        
    
        public void GenerateWalls()
        {
            tilemap.ClearAllTiles();
            //DOWN
            for (int i = -1-width; i <= size.x+width; i++)
            {
                for(int w = 1; w <= width; w++)
                {
                    SetRandomTile(i,-w);
                }
    
            }
            //TOP
            for (int i = -1-width; i <= size.x+width; i++)
            {
                for(int w = 0; w < width; w++)
                {
                    SetRandomTile(i,size.y+w);
                }
            }
            //LEFT
            for (int i = -1-width; i <= size.y+width; i++)
            {
                for(int h = 1; h <= width; h++)
                {
                    SetRandomTile(-h,i);
                }
    
            }
            //RIGHT
            for (int i = -1-width; i <= size.y+width; i++)
            {
                for(int h = 0; h < width; h++)
                {
                    SetRandomTile(size.x+h,i);
                }
    
            }
           
        }
    
    
    
    }


    public class TilemapGenerator : MonoBehaviour
    {
        
        [SerializeField] private FloorTiles floorTiles;
        [SerializeField] private WallTiles wallTiles;
        [SerializeField] private Vector2Int size = Vector2Int.one * 20;
        [SerializeField] private bool updateOnValidate = false;
        private void OnValidate()
        {
            if (!updateOnValidate) return;
            RegenerateTiles();
        }


        
        void Start()
        {
           RegenerateTiles();
        }
        
        [ContextMenu("Regenerate Tiles")]
        private void RegenerateTiles()
        {
            floorTiles.Initialize(size);
            wallTiles.Initialize(size);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position + (Vector3Int)size/2, (Vector2)size );
        }
        
    }
}
