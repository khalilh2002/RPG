using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilMapVisulaizer : MonoBehaviour
{
   [SerializeField]private Tilemap floorTilmap ,wallTilemap;
   [SerializeField]private TileBase floorTile , wallTile;

   public void paintFloorTiles(IEnumerable<Vector2Int> floorPosition){
        paintTiles(floorPosition,floorTilmap,floorTile);
   }

    private void paintTiles(IEnumerable<Vector2Int> Positions, Tilemap Tilmap, TileBase Tile)
    {
        
        foreach (var item in Positions)
        {
            paintSingleTile(Tilmap , Tile , item);
        }
    }

    private void paintSingleTile(Tilemap tilmap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilmap.WorldToCell((Vector3Int)position);
        tilmap.SetTile(tilePosition,tile);
    }

    public void paintWallTiles(Vector2Int position)
    {
        paintSingleTile(wallTilemap,wallTile,position);
    }
    public void clear(){
        floorTilmap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

}

