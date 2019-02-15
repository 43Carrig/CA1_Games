using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference - https://www.youtube.com/watch?v=Mn6lUik3nyk

public class MoveCube : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Direction dir;
    
    private void OnMouseDown()
    {
        Vector3 pos = target.position;
        switch (dir)
        {
            case Direction.DOWN:
                pos += Vector3.down;
                break;
            case Direction.UP:
                pos += Vector3.up;
                break;
            case Direction.LEFT:
                pos += Vector3.left;
                break;
            case Direction.RIGHT:
                pos += Vector3.right;
                break;
            default:
                pos = Vector3.zero;
                break;
        }

        target.position = pos;
    }
}

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
