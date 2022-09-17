using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]

public class CubeEditor : MonoBehaviour
{
    public int gridSize = 4;

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    /// <summary>
    /// Snaps the Blocks on grid based system according to the set grid size.
    /// </summary>
    private void SnapToGrid()
    {
        transform.position = new Vector3(
            GetGridPos().x * gridSize,
            0,
            GetGridPos().y * gridSize
            );
    }
    
    /// <summary>
    /// Returns Vector 2 int of the current grid position.
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    /// <summary>
    /// Update the Label names of the blocks accordinly to the coordinates
    /// </summary>
    private void UpdateLabel()
    {
        string coordText = GetGridPos().x + "," + GetGridPos().y;

        //TextMesh textMesh = GetComponentInChildren<TextMesh>();
        //textMesh.text = coordText;

        gameObject.name = coordText;
    }

}
