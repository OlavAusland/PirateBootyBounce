using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5f;
    public bool isHooked;

    public bool IsHooked
    {
        get => isHooked;
        set
        {
            springJoint.enabled = value;
            if (!value)
            {
                
            }else
            {
                springJoint.anchor = target;
            }

            isHooked = value;
        }
    }

    public Vector3 target;
    public GameManager gm;
    public LineRenderer lr;
    public SpringJoint2D springJoint;
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
            springJoint.distance -= Time.deltaTime * speed;
        else if (Input.GetKey(KeyCode.S) && isHooked)
            springJoint.distance += Time.deltaTime * speed;

    }

    private void OnHook()
    {
        lr.SetPosition(0, transform.position);
    }

    public void Hook()
    {
        lr.positionCount = 2;
        var closest = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

        foreach (var point in gm.hookers)
        {
            if (Vector3.Distance(transform.position, point.position) < Vector3.Distance(transform.position, closest))
                closest = point.position;
        }

        target = closest;
        springJoint.distance = Vector3.Distance(transform.position, closest) - 1;
        
        IsHooked = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, closest);
    }
}
