using UnityEngine;

public abstract class UnitSizeHandler : SizeHandler
{
    protected override void Update()
    {
        transform.localScale = new Vector3(size, size, size);
        HandleSize();
    }

    protected virtual void HandleSize()
    {
        if (satiationValue >= 100f)
        {
            size += .25f;
            satiationValue = 0f;
        }
    }
    
    protected virtual void Consume(GameObject unit)
    {
        if(!isSmaller)
        {
            satiationValue += 25f;
            Destroy(unit);
        }
    }
    
    protected override void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<SizeHandler>().GetSize() > size)
        {
            MoveTowardsCenter(col.gameObject);
        }
        
        if (Vector2.Distance(transform.position, col.transform.position) <= 0.015f)
        {
            Consume(col.gameObject);
        }
    }
    
}
