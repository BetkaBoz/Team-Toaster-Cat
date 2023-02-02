using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public List<GameObject> spawnedObjects;

    public void SpawnObject(int objectIndex, Vector3 position, Quaternion rotation)
    {
        if(objectIndex > spawnedObjects.Count)
        {
            Instantiate(spawnedObjects[0], position, rotation);
        }
        else
        {
            Instantiate(spawnedObjects[objectIndex], position, rotation);
        }
    }
}
