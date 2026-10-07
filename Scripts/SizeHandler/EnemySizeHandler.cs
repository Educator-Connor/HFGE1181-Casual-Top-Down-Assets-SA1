using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySizeHandler : UnitSizeHandler
{
    [Header("Feedback Settings")] 
    [SerializeField] protected Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    protected override void Consume(GameObject unit)
    {
        if (unit.CompareTag("Player") && !isSmaller)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            satiationValue += 25f;
            anim.SetTrigger("Bite");
            base.Consume(unit);
        }
        
    }

    private void OnDestroy()
    {
        SpawnManager.Instance.CleanEnemyArray(this);
    }
}
