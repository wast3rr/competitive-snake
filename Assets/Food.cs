using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    private SnakeMovementScript snake;
    private double lastUpdate = 0.0;

    void Awake() 
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // this.transform.position = new Vector2(0, 0);
        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizePosition()
    {
        // Debug.Log("TestFood");
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        // Round the values to ensure it aligns with the grid
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));
        double currUpdate = Time.timeAsDouble;

        // Prevent the food from spawning on the snake
        if ((currUpdate - lastUpdate) < 0.01)
        {
            RandomizePosition();
            return;
        }

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }
}
