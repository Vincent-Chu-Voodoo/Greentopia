using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGrid : MonoBehaviour
{
    [Header("Display")]
    public List<SubGrid> subGrids;

    [Header("Param")]
    public Vector2 rowColumn;
    public Vector2 subGridSize;

    [Header("Config")]
    public GameObject gridPrefab;
    public Transform subGridRootTrans;
    
    private void Start()
    {
    }

    [ContextMenu("SpawnGrids")]
    public void SpawnGrids()
    {
        ClearSubGrids();
        var startingPosition = transform.position
            - new Vector3(subGridSize.y * rowColumn.x / 2f, 0f, subGridSize.x * rowColumn.y / 2f)
            + new Vector3(subGridSize.y / 2f, 0f, subGridSize.x / 2f);
        for (var i = 0; i < rowColumn.x; i++)
        {
            for (var j = 0; j < rowColumn.y; j++)
            {
                var newGridGO = Instantiate(gridPrefab, subGridRootTrans);
                newGridGO.name = $"SubGrid [{i},{j}]";
                var subGrid = newGridGO.GetComponent<SubGrid>();
                subGrid.id = new Vector2(i, j);
                newGridGO.transform.position = startingPosition + new Vector3(j * subGridSize.y, 0f, i * subGridSize.x);
                subGrids.Add(subGrid);
            }
        }
    }

    [ContextMenu("ClearSubGrids")]
    public void ClearSubGrids()
    {
        while (subGridRootTrans.childCount > 0)
            DestroyImmediate(subGridRootTrans.GetChild(0).gameObject);
        subGrids.Clear();
    }

    public List<SubGrid> FindNeightbourSubgrids(SubGrid subGrid)
    {
        var resultSubGridList = new List<SubGrid>();
        for (var i = (int) (subGrid.id.x - 1); i <= subGrid.id.x + 1; i++)
        {
            for (var j = (int) (subGrid.id.y - 1); j <= subGrid.id.y + 1; j++)
            {
                if (i >= 0 && j >= 0 && i < rowColumn.x && j < rowColumn.y && (i != subGrid.id.x || j != subGrid.id.y))
                {
                    resultSubGridList.Add(GetGridAt(i, j));
                }
            }
        }
        return resultSubGridList;
    }

    public SubGrid GetGridAt(int i, int j)
    {
        return subGrids[(int) (i * rowColumn.y + j)];
    }

    public SubGrid GetFreeGrid()
    {
        return subGrids.Find(i => i.isEmpty);
    }

    public SubGrid FindCloestSubGrid(Vector3 worldPosition)
    {
        int idx = (int) rowColumn.x - 1, idy = (int) rowColumn.y - 1;
        for (var i = 0; i < rowColumn.x; i++)
            if (subGrids[(int) (i * rowColumn.y)].transform.position.x > worldPosition.x - subGridSize.x / 2f)
            {
                idx = i;
                break;
            }
        for (var j = 0; j < rowColumn.y; j++)
            if (subGrids[j].transform.position.z > worldPosition.z - subGridSize.y / 2f)
            {
                idy = j;
                break;
            }
        return subGrids[(int) (idx * rowColumn.y + idy)];
    }
}
