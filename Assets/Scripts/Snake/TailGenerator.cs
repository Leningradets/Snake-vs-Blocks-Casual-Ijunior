using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private Segment _segment;

    public List<Segment> Generate(int tailSize)
    {
        List<Segment> segments = new List<Segment>();

        for(int i = 0; i < tailSize; i++)
        {
            segments.Add(Instantiate(_segment, transform));
        }

        return segments;
    }
}
