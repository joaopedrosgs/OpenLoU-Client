using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
	public Tile[] Tiles;

	public Tilemap Tilemap;
	// Use this for initialization
	void Start ()
	{
		Tilemap = FindObjectOfType<Tilemap>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTile(int x, int y, int type)
	{
		Tilemap.SetTile(new Vector3Int(x,y,0),Tiles[type]);
	}
	public void SetTile(int x, int y,int continent, int type)
	{
		Tilemap.SetTile(new Vector3Int(x,y,0),Tiles[type]);
	}

	public void ClearTiles()
	{
		Tilemap.ClearAllTiles();
	}
	public Vector3 CellToWorld(int x, int y)
	{
		return Tilemap.CellToWorld(new Vector3Int(x, y, 0));
		
	}
	public Vector3 CellToWorld(int x, int y,int continent)
	{
		return Tilemap.CellToWorld(new Vector3Int(x, y, 0));
	}
}
