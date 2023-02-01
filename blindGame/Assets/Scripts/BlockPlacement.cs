using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    Grid worldGrid;
    public int width;
    public int height;
    public float cellSize;
    public bool showDebugGrid;
    public int currentBlock;
    public List<GameObject> blockPrefabs;
    public GameObject transparentBlock;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = new Grid(width, height, cellSize, new Vector3(0, 0, 0), showDebugGrid);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transPos = worldGrid.GetCellCenter(GetMouseWorldPos());
        transPos.x += 0.5f;
        transparentBlock.transform.position = transPos;
        if (Input.GetMouseButtonDown(0))
        {
            //if (CheckIfBoxCanBePlaced())
            //{
            //    Debug.Log("poop");
            //}
        }
        if (Input.GetMouseButtonDown(1))
        {
            worldGrid.SetValue(GetMouseWorldPos(), 1);
        }
    }

    private bool CheckIfBoxCanBePlaced()
    {
        if (worldGrid.GetValue(GetMouseWorldPos()) != 0)
        {
            Debug.Log("Grid gotya!");
            return false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics2D.Raycast(ray.origin, ray.direction))
        {
            Debug.Log("Raycast gotya!");
            return false;
        }


        return true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 returnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        returnPosition.z = 0f;
        return returnPosition;
    }
}
