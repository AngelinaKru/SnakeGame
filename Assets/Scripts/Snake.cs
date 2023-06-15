using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> segmentsList = new List<Transform>();
    public Transform segmentPrefab;

    public int initialSize = 4;


    void Start()
    {
        ResetState();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = segmentsList.Count - 1; i > 0; i--)
        {
            segmentsList[i].position = segmentsList[i - 1].position;
        }


        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segmentsList[segmentsList.Count - 1].position;
        segmentsList.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < segmentsList.Count; i++)
        {
            Destroy(segmentsList[i].gameObject);
        }

        segmentsList.Clear();
        segmentsList.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            segmentsList.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }

    }
}
