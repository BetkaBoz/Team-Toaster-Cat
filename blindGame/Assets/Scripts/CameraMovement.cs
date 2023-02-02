using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement playerI; 

    public Transform player; // suradnice hraca
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;
    public Vector3 positionOffset;

    [Header("limit")]
    public Vector2 xlimit;
    public Vector2 ylimit;
   

    void Update()
    {
        if (playerI.dead) return;
        Vector3 targetPos = player.position + positionOffset;
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, xlimit.x, xlimit.y), Mathf.Clamp(targetPos.y, ylimit.x, ylimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
