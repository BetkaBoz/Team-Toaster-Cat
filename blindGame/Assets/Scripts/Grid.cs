using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPoint;

    public Grid(int width, int height, float cellSize, Vector3 originPoint, bool showDebugGrid)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new int[width, height];
        this.originPoint = originPoint;

        //only here for debugging
        if (showDebugGrid)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPoint;
    }

    private void GetXY(Vector3 position, out int x, out int y)
    {
        x = Mathf.FloorToInt((position - originPoint).x / cellSize);
        y = Mathf.FloorToInt((position - originPoint).y / cellSize);
    }

    public Vector3 GetCellCenter(Vector3 position)
    {
        int x, y;
        GetXY(position, out x, out y);
        Vector3 returnVector = GetWorldPosition(x, y);
        returnVector.x += cellSize / 2;
        returnVector.y += cellSize / 2;
        return returnVector;
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 wordlPosition, int value)
    {
        int x, y;
        GetXY(wordlPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(Vector3 wordlPosition)
    {
        int x, y;
        GetXY(wordlPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }
}
