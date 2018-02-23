using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{

    public Tilemap Tilemap;

    public GameObject Decal;
    // Use this for initialization
    void Awake()
    {
        Tilemap = FindObjectOfType<Tilemap>();

    }

    // Update is called once per frame


    public void HighlightTile(Vector3 globalPos)
    {
        var cell = Tilemap.WorldToCell(globalPos);
        Decal.transform.position = Tilemap.CellToLocal(cell);
        Decal.SetActive(true);
        var tile = Tilemap.GetTile(cell);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up);
            if (hit && hit.collider != null)
            {
                HighlightTile(hit.point);
            }
        }
    }
}
