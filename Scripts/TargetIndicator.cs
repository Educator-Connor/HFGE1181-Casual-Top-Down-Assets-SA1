using System;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float hideDistance;
    [SerializeField] private float offset;
    
    // Update is called once per frame
    void Update()
    {
        try
        {
            var dir = target.position - transform.position;

            if (dir.magnitude < hideDistance)
            {
                SetChildrenActive(false);
            }
            else
            {
                SetChildrenActive(true);
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
               
            }
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
      
    }

    private void LateUpdate()
    {
        try
        {
            var dir = target.position - transform.position;
            HandleOffset(dir);
        }
        catch (Exception e)
        {
            Destroy(this.gameObject);
        }
      
    }

    public void HandleOffset(Vector3 dir)
    {
        Transform playerTransform = transform.parent.GetComponent<FollowTarget>().GetTarget();
        transform.position = transform.parent.transform.position + dir.normalized * offset * playerTransform.GetComponent<SizeHandler>().GetSize();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
