using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementScript : MonoBehaviour
{
    public Rigidbody2D snakebody;
    public float snakeSpeed = 2;
    public GameObject circle;
    private List<Transform> segments = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        var newcircle = Instantiate(circle, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        newcircle.transform.parent = GameObject.Find("Snake").transform;
        snakebody.velocity = Vector2.right * snakeSpeed;

        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            snakebody.velocity = Vector2.up * snakeSpeed;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            snakebody.velocity = Vector2.down * snakeSpeed;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            snakebody.velocity = Vector2.right * snakeSpeed;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            snakebody.velocity = Vector2.left * snakeSpeed;
        }
    }

    private void Grow() {
        Debug.Log("Test");
        // Transform segment = Instantiate(snakebody);
        // segment.position = segments[segments.Count - 1].position;
        // segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
    }
}
