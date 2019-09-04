using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facing : MonoBehaviour
{
    public enum Face
    {
        front = 0,
        frontRight = 1,
        right = 2,
        backRight = 3,
        back = 4,
        backLeft = 5,
        left = 6,
        frontLeft = 7
    }
    private Face face;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 0.1)
        {
            UpdateFacing();
        }
    }
    public Face GetFace()
    {
        return face;
    }
    private void UpdateFacing()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        int octant = (int)Mathf.Round(8 * angle / (2 * Mathf.PI) + 10) % 8;
        face = (Face)octant;
    }
}
