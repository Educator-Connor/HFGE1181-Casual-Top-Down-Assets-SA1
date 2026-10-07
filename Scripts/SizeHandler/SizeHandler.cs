using System;
using UnityEngine;

public abstract class SizeHandler : MonoBehaviour
{
    [Header("Size Parameters")]
    [SerializeField] protected float size;
    [SerializeField] protected bool isSmaller = false;
    protected float satiationValue = 0f;

    protected virtual void Update()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    public virtual float GetSize()
    {
        return size;
    }

    public virtual void SetSize(float newSize)
    {
        size = newSize;
    }
    
    protected virtual void MoveTowardsCenter(GameObject target)
    {
        SizeHandler targetSize = target.GetComponent<SizeHandler>();
        float distance = Vector3.Distance(transform.position, target.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 
            ((size * targetSize.GetSize())/distance*distance) * Time.deltaTime);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.GetComponent<SizeHandler>())
        {
            return;
        }
        
        if (col.gameObject.GetComponent<SizeHandler>().GetSize() > size)
        {
            isSmaller = true;
        }
    }
    
    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<SizeHandler>().GetSize() > size)
        {
            MoveTowardsCenter(col.gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        isSmaller = false;
    }
}
