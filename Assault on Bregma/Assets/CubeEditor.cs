using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    

	private Waypoint _waypoint;

	void Awake()
	{
		_waypoint = GetComponent<Waypoint>();
	}

	// Update is called once per frame
	void Update ()
	{
		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
	    int gridSize = _waypoint.GetGridSize();
		transform.position = new Vector3(
		    _waypoint.GetGridPos().x * gridSize, 
		    0f,
		    _waypoint.GetGridPos().y * gridSize);
	}

	private void UpdateLabel()
	{
	    TextMesh _textMesh = GetComponentInChildren<TextMesh>();
		string labelText = 
		    _waypoint.GetGridPos().x 
		    + "," 
		    + _waypoint.GetGridPos().y;
		_textMesh.text = labelText;
		gameObject.name = labelText;
	}
}
