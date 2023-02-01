using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    Grid worldGrid;
    public int width;
    public int height;
    public float cellSize;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = new Grid(width, height, cellSize, new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(worldGrid.GetValue(GetMouseWorldPos(Input.mousePosition, Camera.main)));
        }
    }

    private Vector3 GetMouseWorldPos(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 returnPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        returnPosition.z = 0f;
        return returnPosition;
    }
}
