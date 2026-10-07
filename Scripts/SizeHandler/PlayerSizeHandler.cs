using UnityEngine;

public class PlayerSizeHandler : UnitSizeHandler
{
    [Header("Feedback Settings")] 
    [SerializeField] protected Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected override void HandleSize()
    {
        if (satiationValue >= 100f)
        {
            size += .25f;
            satiationValue = 0f;
            anim.SetTrigger("Grow");
        }
    }

    protected override void MoveTowardsCenter(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            base.MoveTowardsCenter(target);
        }
        else if (target.CompareTag("Food"))
        {
            
        }
        
    }

    protected override void Consume(GameObject unit)
    {
        if (!isSmaller)
        {
            satiationValue += 25f;
            anim.SetTrigger("Bite");
            Destroy(unit);
        }
    }
    
}
