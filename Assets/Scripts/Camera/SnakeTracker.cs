using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTracker : MonoBehaviour
{
    [SerializeField] SnakeHead _snakeHead;
    [SerializeField] float _speed;
    [SerializeField] float _offsetY;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.deltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(transform.position.x, _snakeHead.transform.position.y + _offsetY, transform.position.z);
    }
}
