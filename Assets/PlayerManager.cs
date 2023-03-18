using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float swingForce = 2f;
    public float speed = 5f;
    public bool isHooked;

    public bool IsHooked
    {
        get => isHooked;
        set
        {
            distanceJoint.enabled = value;
            if (!value)
            {
                
            }else
            {
                distanceJoint.connectedAnchor = target;
            }

            isHooked = value;
        }
    }

    private Vector3 target;
    public GameManager gm;
    public Rigidbody2D rb;
    public LineRenderer lr;
    public DistanceJoint2D distanceJoint;
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Hook();
        if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
            IsHooked = false;
        }

        if (IsHooked)
            OnHook();

        if (Input.GetKey(KeyCode.W) && isHooked)
            transform.position += (target - transform.position).normalized * speed;
        else if (Input.GetKey(KeyCode.S) && isHooked)
            transform.position -= (target - transform.position).normalized * speed;
        else if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector2.left * swingForce, ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector2.right * swingForce, ForceMode2D.Impulse);

    }

    private void OnHook()
    {
        lr.SetPosition(0, transform.position);
    }

    private void Hook()
    {
        lr.positionCount = 2;
        var closest = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

        foreach (var point in gm.hookPoints)
        {
            if (Vector3.Distance(transform.position, point.position) < Vector3.Distance(transform.position, closest))
                closest = point.position;
        }

        target = closest;
        distanceJoint.distance = Vector3.Distance(transform.position, closest);
        
        IsHooked = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, closest);
    }
}
