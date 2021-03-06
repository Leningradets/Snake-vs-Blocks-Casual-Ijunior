using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;
    [SerializeField] private float _tailSpringness;

    private SnakeInput _snakeInput;
    private TailGenerator _tailGenerator;
    private List<Segment> _tail;

    public event UnityAction<int> TailSizeUpdated;

    private void Start()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _snakeInput = GetComponent<SnakeInput>();

        _tail = _tailGenerator.Generate(_tailSize);
        TailSizeUpdated?.Invoke(_tail.Count);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.deltaTime);

        _head.transform.up = _snakeInput.GetDirectionToClick(_head.transform.position);
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void Move(Vector2 nextPosition)
    {
        Vector2 previousPosition = _head.transform.position;

        foreach (Segment segment in _tail)
        {
            Vector2 tempPosition = segment.transform.position;

            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringness * Time.deltaTime);

            previousPosition = tempPosition;
        }

        _head.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        Segment deletedSegment = _tail[_tail.Count - 1];
        _tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);

        TailSizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonus)
    {
        _tail.AddRange(_tailGenerator.Generate(bonus));

        TailSizeUpdated?.Invoke(_tail.Count);
    }
}
