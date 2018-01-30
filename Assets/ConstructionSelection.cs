using UnityEngine;
using UnityEngine.Tilemaps;

public class ConstructionSelection : MonoBehaviour
{
	private Tilemap tilemap;
	public GameObject Decal;

	private UIController uiController;
	// Use this for initialization
	void Start ()
	{
		tilemap = GetComponentInChildren<Tilemap>();
		uiController = FindObjectOfType<UIController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Decal.SetActive(true);
			var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var cell = tilemap.WorldToCell(worldPosition);
			uiController.ShowInfoAboutConstruction(cell);
			var newPosition = tilemap.CellToWorld(cell);
			newPosition.z = 1;
			Decal.transform.position = newPosition;
		}
	}
}
