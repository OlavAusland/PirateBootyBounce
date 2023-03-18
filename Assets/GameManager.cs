using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject obj;
    public Camera cam;
    public float scrollSpeed = 1f;
    public List<Transform> hookPoints;

    public Vector3 point;

    private void Start()
    {
        point = new Vector3(hookPoints[hookPoints.Count-1].transform.position.x + Random.Range(6, 10), 2.25f, 0);
    }

    public void FixedUpdate()
    {
        cam.transform.position += (Vector3.right * scrollSpeed);
        GeneratePoints();
    }

    private void GeneratePoints()
    {
        var distance = point.x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        
        Debug.Log(distance);
        Debug.Log(point);
        if (distance - 1 <= 0)
        {
            GameObject hookPoint = Instantiate(obj, point, Quaternion.identity);
            hookPoints.Add(hookPoint.transform);
            
            point = new Vector3(hookPoints[hookPoints.Count-1].transform.position.x + 
                                Random.Range(6, 10), 2.25f + Random.Range(0.5f, 1.5f), 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(point, 1f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)), 1f);
    }
}
