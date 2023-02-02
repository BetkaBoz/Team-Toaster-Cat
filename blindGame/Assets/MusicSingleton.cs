using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            for (int x = 1; x< objs.Length;x++)
            {
                Destroy(objs[x]);
            }
        }
        DontDestroyOnLoad(this.gameObject);

    }
}
