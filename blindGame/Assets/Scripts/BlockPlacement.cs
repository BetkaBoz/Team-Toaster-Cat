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
    public List<GameObject> transparentBlocks;
    public GameObject currentTransparentBlock;
    public BoxCollider2D transparentBlockCollider;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = new Grid(width, height, cellSize, new Vector3(0, 0, 0), showDebugGrid);
        transparentBlocks[currentBlock].SetActive(true);
        currentTransparentBlock = transparentBlocks[currentBlock];
        transparentBlockCollider = currentTransparentBlock.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            transparentBlocks[currentBlock].SetActive(false);
            if (Input.GetKeyDown(KeyCode.Alpha1))
                currentBlock = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                currentBlock = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3))
                currentBlock = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4))
                currentBlock = 3;
            if (Input.GetKeyDown(KeyCode.Alpha5))
                currentBlock = 4;
            transparentBlocks[currentBlock].SetActive(true);
            currentTransparentBlock = transparentBlocks[currentBlock];
        }
            

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            transparentBlocks[currentBlock].SetActive(false);
            if (currentBlock >= transparentBlocks.Count - 1)
            {
                currentBlock = 0;
            }
            else
            {
                currentBlock++;
            }
            transparentBlocks[currentBlock].SetActive(true);
            currentTransparentBlock = transparentBlocks[currentBlock];
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            transparentBlocks[currentBlock].SetActive(false);
            if (currentBlock <= 0)
            {
                currentBlock = transparentBlocks.Count - 1;
            }
            else
            {
                currentBlock--;
            }
            transparentBlocks[currentBlock].SetActive(true);
            currentTransparentBlock = transparentBlocks[currentBlock];
        }

        transparentBlockCollider.enabled = true;
        Vector3 transPos = worldGrid.GetCellCenter(GetMouseWorldPos(),Mathf.FloorToInt(transparentBlockCollider.bounds.size.x));
        transPos.x += 0.5f;
        currentTransparentBlock.transform.position = transPos;
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckIfBoxCanBePlaced())
            {
                Debug.Log("poop");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            worldGrid.SetValue(GetMouseWorldPos(), 1);
        }
    }

    private bool CheckIfBoxCanBePlaced()
    {
        var size = transparentBlockCollider.bounds.size;
        size.x -= 1f;
        size.y -= 0.5f;
        transparentBlockCollider.enabled = false;

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

        //this is stupid but for some reason I need to shrink the x otherwise it 
        RaycastHit2D[] hits = Physics2D.BoxCastAll(currentTransparentBlock.transform.position, size, 0f, new Vector2(0, 0));
        if(hits.Length > 0)
        {
            Debug.Log("Colliders gotya!");
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
