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
    public int oldBlock = 0;
    public List<GameObject> transparentBlocks;
    public GameObject currentTransparentBlock;
    public BoxCollider2D transparentBlockCollider;
    public BlockSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = new Grid(width, height, cellSize, new Vector3(0, 0, 0), showDebugGrid);
        ChangeCurrentBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            oldBlock = currentBlock;
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
            if (Input.GetKeyDown(KeyCode.Alpha6))
                currentBlock = 5;
            ChangeCurrentBlock();
        }
            

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            oldBlock = currentBlock;
            if (currentBlock >= transparentBlocks.Count - 1)
            {
                currentBlock = 0;
            }
            else
            {
                currentBlock++;
            }
            ChangeCurrentBlock();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            oldBlock = currentBlock;
            if (currentBlock <= 0)
            {
                currentBlock = transparentBlocks.Count - 1;
            }
            else
            {
                currentBlock--;
            }
            ChangeCurrentBlock();
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
                spawner.SpawnObject(currentBlock, currentTransparentBlock.transform.position, currentTransparentBlock.transform.rotation);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            worldGrid.SetValue(GetMouseWorldPos(), 1);
        }
    }

    private void ChangeCurrentBlock()
    {
        transparentBlocks[oldBlock].SetActive(false);
        transparentBlocks[currentBlock].SetActive(true);
        currentTransparentBlock = transparentBlocks[currentBlock];
        transparentBlockCollider = currentTransparentBlock.GetComponent<BoxCollider2D>();
        Debug.Log("Boop");
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
        if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity,LayerMask.GetMask("Platform")))
        {
            Debug.Log("Raycast gotya!");
            return false;
        }

        //this is stupid but for some reason I need to shrink the x otherwise it 
        RaycastHit2D[] hits = Physics2D.BoxCastAll(currentTransparentBlock.transform.position, size, 0f, new Vector2(0, 0), Mathf.Infinity, LayerMask.GetMask("Platform"));//, LayerMask.NameToLayer("Platform")
        if (hits.Length > 0)
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
