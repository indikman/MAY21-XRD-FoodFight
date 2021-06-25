using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public BoxCollider spawnArea;
    public FoodItem[] foodItems;

    [Header("Target")] 
    public Target targetPrefab;
    public float minSpeed;
    public float maxSpeed;
    public float minAmount;
    public float maxAmount;

    void Start()
    {
        // Create a new target
        SpawnTarget();
    }

    public void OnTargetHit()
    {
        SpawnTarget();
    }

    public void SpawnNewFood(Vector3 pos, Quaternion rot)
    {
        var randomFood = foodItems[Random.Range(0, foodItems.Length)];

        var newFood = Instantiate(randomFood, pos, rot);
    }
    


    private void SpawnTarget()
    {
        Target newTarget = Instantiate(targetPrefab, GetRandomPosition(), targetPrefab.transform.rotation);

        newTarget.game = this;
        newTarget.SetLevel(Random.Range(minSpeed, maxSpeed), Random.Range(minAmount, maxAmount));
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

       
    }

    
}
