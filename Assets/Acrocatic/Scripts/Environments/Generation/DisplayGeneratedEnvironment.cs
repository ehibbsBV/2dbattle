using UnityEngine;
using System.Collections;
using Acrocatic.Scripts.Environments.Generation;

public class DisplayGeneratedEnvironment : MonoBehaviour
{

	public GameObject emptyCell, wallCell;
	public int gridSize = 50;
	public int percentWalls = 50;
	
	private float cellSize;
	private bool hasGridChanged = false;
	private Conway conwayGrid;
	private GameObject[,] cellGrid;
	private Vector3 topLeftDisplayCoords;
	
	// Use this for initialization
	void Start ()
	{
		SetUp ();
	}
	
	private void SetUp ()
	{
		Renderer wallRenderer = wallCell.GetComponent<Renderer> ();
		cellSize = wallRenderer.bounds.size.x;
		InstantiateLevelGrid ();
		hasGridChanged = true;
		cellGrid = new GameObject[gridSize, gridSize];
		topLeftDisplayCoords = new Vector3 (
								-cellSize * (gridSize / 2),
								-cellSize * (gridSize / 2)
		);
	}
	
	private void InstantiateLevelGrid ()
	{
		conwayGrid = new Conway (gridSize, 6, 8, 3, 8, percentWalls);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (hasGridChanged) {
			DrawGrid ();
			hasGridChanged = false;
		}
		
		if (Input.GetButtonDown ("NextStep")) {
			Debug.Log ("Simulating next iteration");
			conwayGrid.NextIteration ();
			hasGridChanged = true;
		}
		
		if (Input.GetButtonDown ("ResetGeneration")) {
			Debug.Log ("Resetting generation");
			InstantiateLevelGrid ();
			hasGridChanged = true;
		}
	}
	
	void PrintGridToConsole ()
	{
		for (int row = 0; row < gridSize; row++) {
			string lineOutput = "";
			for (int col = 0; col < gridSize; col++) {
				if (conwayGrid.CellGrid [col, row] == CellStates.ALIVE) {
					lineOutput += "X";
				} else {
					lineOutput += "O";
				}
				if (col < gridSize - 1) {
					lineOutput += " ";
				}
			}
			Debug.Log (lineOutput);
		}
	}
	
	// draw the grid
	void DrawGrid ()
	{
		for (int col = 0; col < gridSize; col++) {
			for (int row = 0; row < gridSize; row++) {
				
				if (cellGrid [col, row] != null) {
//					Debug.Log ("Item isn't null, destroying it.");
					Destroy (cellGrid [col, row]);
					
				} else {
//					Debug.Log ("Item is null, leaving it alone.");
				}
				
				if (conwayGrid.CellGrid [col, row] == CellStates.ALIVE) {
					cellGrid [col, row] = (GameObject)Instantiate (wallCell, new Vector3 (
											topLeftDisplayCoords.x + (col * cellSize),
											topLeftDisplayCoords.y + (row * cellSize)), 
											Quaternion.identity);
					cellGrid [col, row].transform.parent = gameObject.transform;
				}
			}
		}
		
		PrintGridToConsole ();
	}
	
}
