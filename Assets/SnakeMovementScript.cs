using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovementScript : MonoBehaviour
{
    public Transform segmentPrefab;
    public float snakeSpeed = 0.5f;
    public GameObject circle;
    private float nextUpdate;
    private Vector2 initialPosition;
    public Color playerColor;

    private int resetSubtraction = 2;

    private List<Transform> segments = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private PlayerData playerData;

    public void Construct(PlayerData playerdata)
    {
        playerData = playerdata;
    }

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);

        playerColor = this.GetComponent<SpriteRenderer>().color;
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerData == null || playerData.Index >= Gamepad.all.Count)
        // {
        //     return;
        // }

        // var gamepad = Gamepad.all[playerData.Index];
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)
            // || gamepad.dpad.up.isPressed
            )
            {
                direction = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)
            // || gamepad.dpad.down.isPressed
            )
            {
                direction = Vector2Int.down;
            }
        }

        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)
            // || gamepad.dpad.right.isPressed
            )
            {
                direction = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)
            // || gamepad.dpad.left.isPressed
            )
            {
                direction = Vector2Int.left;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.time < nextUpdate)
        {
            return;
        }
        nextUpdate = Time.time + (0.5f / (snakeSpeed));

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].GetComponent<SpriteRenderer>().color = playerColor;
            segments[i].position = segments[i - 1].position;
        }
        float x = transform.position.x + direction.x;
        float y = transform.position.y + direction.y;
        transform.position = new Vector2(x, y);
    }

    private void Grow()
    {
        Debug.Log("Test");
        Transform segment = Instantiate(segmentPrefab);
        segment.GetComponent<SpriteRenderer>().color = playerColor;
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void Reset()
    {
        Debug.Log("Test");
        this.transform.position = initialPosition;
        int count = segments.Count;

        if (count - resetSubtraction > 0)
        {
            for (int i = count - 1; i > count - (resetSubtraction + 1); i--)
            {
                Debug.Log("Test" + count);
                Debug.Log(i);
                Destroy(segments[i].gameObject);
                segments.RemoveAt(i);
            }
        }
        else
        {
            for (int i = count - 1; i > 0; i--)
            {
                Destroy(segments[i].gameObject);
                segments.RemoveAt(i);
            }
        }

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Reset();
        }
        else if (other.gameObject.CompareTag("Snake"))
        {
            if (other.transform != this.transform)
            {
                int thisIndex = segments.IndexOf(this.transform);
                for (int i = 0; i < thisIndex + 3; i++)
                {
                    if (i < segments.Count && other.transform == segments[i])
                    {
                        Debug.Log("Dont delete");
                        return;
                    }
                }
                Debug.Log("deleted");
                Reset();
            }
        }
    }
}
