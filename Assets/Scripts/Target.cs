using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float moveSpeed;
    public float moveAmount;

    private float startXPosition;

    [HideInInspector]
    public GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        startXPosition = transform.position.x;
    }

    public void SetLevel(float speed, float amount)
    {
        moveAmount = amount;
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;

        newPosition.x = startXPosition + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        var foodItem = other.gameObject.GetComponent<FoodItem>();
        if(foodItem != null)
        {
            Destroy(foodItem.gameObject);


            Destroy(gameObject);

            game.OnTargetHit();
            game.SpawnNewFood(foodItem.startPosition, foodItem.startRotation);
        }
    }
}
