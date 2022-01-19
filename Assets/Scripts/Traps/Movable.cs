using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public enum DirectionMovable
    {
        X = 0,
        Y,
        Z
    }

    [SerializeField]
    private float Speed = 5.0f;
    [SerializeField]
    private DirectionMovable Direction;
    [SerializeField]
    private float Distance = 5.0f;

    private bool isReverse = false;
    private Vector3 StartPosition = Vector3.zero;

    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        switch (Direction)
        {
            case DirectionMovable.X: MoveDirectionX(); break;
            case DirectionMovable.Y: MoveDirectionY(); break;
            case DirectionMovable.Z: MoveDirectionZ(); break;
        }
    }

    private void MoveDirectionX() {
        if (transform.position.x > StartPosition.x + Distance && isReverse)
            isReverse = false;
        if (transform.position.x < StartPosition.x - Distance && !isReverse)
            isReverse = true;

        
        if (isReverse)
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        else
            transform.Translate(Vector3.right * -Speed * Time.deltaTime);
    }

    private void MoveDirectionY()
    {
        if (transform.position.y > StartPosition.y + Distance && isReverse)
            isReverse = false;
        if (transform.position.y < StartPosition.y - Distance && !isReverse)
            isReverse = true;

        
        if (isReverse)
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        else
            transform.Translate(Vector3.up * -Speed * Time.deltaTime);
    }

    private void MoveDirectionZ()
    {
        if (transform.position.z > StartPosition.z + Distance && isReverse)
            isReverse = false;
        if (transform.position.z < StartPosition.z - Distance && !isReverse)
            isReverse = true;

        
        if (isReverse)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * -Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.parent = null;
    }
}
