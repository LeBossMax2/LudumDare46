using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCentered : MonoBehaviour
{
    public bool dummy;
    private void OnValidate()
    {
        Grid g = FindObjectOfType<Grid>();
        this.transform.position = g.cellSize / 2 + g.CellToWorld(g.WorldToCell(this.transform.position));
    }
}
