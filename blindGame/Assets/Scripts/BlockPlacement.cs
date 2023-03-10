using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    Grid worldGrid;
    public int width;
    public int height;
    public float cellSize;
    public Transform gridOrigin;
    public bool showDebugGrid;
    public int currentBlock;
    public int oldBlock = 0;
    public List<TransparentObject> transparentBlocks;
    public GameObject currentTransparentBlock;
    public BoxCollider2D transparentBlockCollider;
    public BlockSpawner spawner;
    public Transform playerPos = null;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        worldGrid = new Grid(width, height, cellSize, gridOrigin.position, showDebugGrid);
        ChangeCurrentBlock();
        transparentBlockCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (gameManager.isGameOver) return;
        if (Input.anyKeyDown)
        {
            oldBlock = currentBlock;
            if (Input.GetKeyDown(KeyCode.Alpha1) && transparentBlocks[0].count > 0)
                currentBlock = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2) && transparentBlocks[1].count > 0)
                currentBlock = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3) && transparentBlocks[2].count > 0)
                currentBlock = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4) && transparentBlocks[3].count > 0)
                currentBlock = 3;
            if (Input.GetKeyDown(KeyCode.Alpha5) && transparentBlocks[4].count > 0)
                currentBlock = 4;
            if (Input.GetKeyDown(KeyCode.Alpha6) && transparentBlocks[5].count > 0)
                currentBlock = 5;
            ChangeCurrentBlock();
        }
            

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
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
            CheckScrollAvailability(true);
            ChangeCurrentBlock();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
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
            CheckScrollAvailability(false);
            ChangeCurrentBlock();
        }

        Vector3 transPos = worldGrid.GetCellCenter(GetMouseWorldPos(),transparentBlocks[currentBlock].size);
        transPos.x += 0.5f;
        currentTransparentBlock.transform.position = transPos;
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckIfBoxCanBePlaced())
            {
                Debug.Log("Block: " + currentBlock + " count: " + transparentBlocks[currentBlock].count);
                if (transparentBlocks[currentBlock].count <= 0) return;
                worldGrid.SetValue(GetMouseWorldPos(), 1);
                gameManager.SubstractScore(transparentBlocks[currentBlock].pointCost);
                transparentBlocks[currentBlock].decreaseAmount();
                spawner.SpawnObject(currentBlock, currentTransparentBlock.transform.position, currentTransparentBlock.transform.rotation);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            worldGrid.SetValue(GetMouseWorldPos(), 1);
        }
    }

    private void CheckScrollAvailability(bool scrollUp)
    {
        if (transparentBlocks[currentBlock].count > 0)
        {
            Debug.Log(currentBlock);
            return;
        }
        if (!scrollUp)
        {
            for(int x = currentBlock-1; x >= 0; x--)
            {
                Debug.Log(x);
                if (transparentBlocks[x].count > 0)
                {
                    Debug.Log("goin up "+x);
                    currentBlock = x;
                    return;
                }
            }
            for (int x = transparentBlocks.Count - 1; x > currentBlock; x--)
            {
                Debug.Log(x);
                if (transparentBlocks[x].count > 0)
                {
                    Debug.Log("going upp "+x);
                    currentBlock = x;
                    return;
                }
            }
        }
        else
        {
            for (int x = currentBlock + 1; x <= transparentBlocks.Count - 1; x++)
            {
                Debug.Log(x);
                if (transparentBlocks[x].count > 0)
                {
                    Debug.Log("going down "+x);
                    currentBlock = x;
                    return;
                }
            }
            for (int x = 0; x < currentBlock; x++)
            {
                Debug.Log(x);
                if (transparentBlocks[x].count > 0)
                {
                    Debug.Log("going downn "+x);
                    currentBlock = x;
                    return;
                }
            }
        }
        Debug.Log("END");
    }

    private void ChangeCurrentBlock()
    {
        transparentBlocks[oldBlock].block.SetActive(false);
        transparentBlocks[oldBlock].Selected(false);
        transparentBlocks[currentBlock].block.SetActive(true);
        transparentBlocks[currentBlock].Selected(true);
        currentTransparentBlock = transparentBlocks[currentBlock].block;
        transparentBlockCollider = currentTransparentBlock.GetComponent<BoxCollider2D>();
    }

    private bool CheckIfBoxCanBePlaced()
    {
        transparentBlockCollider.enabled = true;
        var size = transparentBlockCollider.bounds.size;
        size.x -= 1f;
        size.y -= 0.5f;
        transparentBlockCollider.enabled = false;

        if(((gridOrigin.position.x + 1f) >= worldGrid.GetCellCenter(GetMouseWorldPos(), 1).x) && (playerPos != null))
        {
            Debug.Log("Player gotya!");
            return false;
        }

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
