using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour
{
    public float MoveSpeed = 1;
    public Vector3 MoveDirection;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = this.transform.position;
        currentPos += this.MoveDirection * this.MoveSpeed;
        this.transform.position = currentPos;
    }
}
