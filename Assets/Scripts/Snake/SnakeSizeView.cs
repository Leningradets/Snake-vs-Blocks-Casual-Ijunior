using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Snake))]
public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] TMP_Text _view;

    private Snake _snake;

    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.TailSizeUpdated += OnTailSizeUpdated;
    }

    private void OnDisable()
    {
        _snake.TailSizeUpdated -= OnTailSizeUpdated;
    }

    public void OnTailSizeUpdated(int tailLenght)
    {
        _view.text = tailLenght.ToString();
    }
}