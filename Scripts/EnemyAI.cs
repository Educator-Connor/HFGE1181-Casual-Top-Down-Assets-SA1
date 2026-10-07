using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float wanderDistance;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float idleTime;

    private void Start()
    {
        StartCoroutine(IdleAtLocation());
    }

    public Vector3 SelectNewLocation()
    {
        
        Vector3 newLocation = new Vector3(Random.Range(transform.position.x - wanderDistance, transform.position.x + wanderDistance),
            Random.Range(transform.position.y - wanderDistance, transform.position.y + wanderDistance),0);
        return newLocation;
    }

    public IEnumerator IdleAtLocation()
    {
        yield return new WaitForSecondsRealtime(idleTime);
        StartCoroutine(MoveTowardsLocation());
    }
    
    public IEnumerator MoveTowardsLocation()
    {
        
        Vector3 newLocation = SelectNewLocation();
        while (Vector2.Distance(transform.position, newLocation) > 0.015f)
        {
            
            yield return new WaitWhile(() => GameStateManager.Instance.GetGameState() == GameState.Paused); 
            
            transform.position = Vector3.MoveTowards(transform.position, newLocation, 
                wanderSpeed * Time.deltaTime);
            yield return null;
        }
        
        yield return StartCoroutine(IdleAtLocation());    
    }


    
}
