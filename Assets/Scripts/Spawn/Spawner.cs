using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private float _distanceBetweenLines;

    [Header("Blocks")]
    [Range(0f, 1f)]
    [SerializeField] private float _blockfullLineSpawnChance;
    [Range(0f, 1f)]
    [SerializeField] private float _blockSpawnChance;
    [SerializeField] private Block _block;

    [Header("Walls")]
    [Range(0f, 1f)]
    [SerializeField] private float _wallfullLineSpawnChance;
    [Range(0f, 1f)]
    [SerializeField] private float _wallSpawnChance;
    [SerializeField] private Wall _wall;

    private BlockSpawnPoint[] _blockSpawnPoints;
    private WallSpawnPoint[] _wallSpawnPoints;

    private void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        for (int i = 0; i < _repeatCount; i++)
        {
            GenerateLine(_blockSpawnPoints, _block.gameObject, _blockfullLineSpawnChance, _blockSpawnChance);
            GenerateLine(_wallSpawnPoints, _wall.gameObject, _wallfullLineSpawnChance, _wallSpawnChance, _distanceBetweenLines / 2);
            MoveSpawner(_distanceBetweenLines);
        }
    }

    private void GenerateLine(SpawnPoint[] spawnPoints, GameObject generatedElement,float fullLineSpawnChance, float spawnChance, float scaleY = 1)
    {
        if (Random.Range(0f, 1f) < fullLineSpawnChance)
        {
            GenerateFullLine(spawnPoints, generatedElement, scaleY);
        }
        else
        {
            GenerateRandomLine(spawnPoints, generatedElement, spawnChance, scaleY);
        }
    }


    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement, float scaleY = 1)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement);
            element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            element.transform.Translate(Vector2.down * scaleY / 2);
        }
    }

    private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject generatedElement, float spawnChance, float scaleY = 1)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0f, 1f) < spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
                element.transform.Translate(Vector2.down * scaleY / 2);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawnPoint, GameObject generatedElement)
    {
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
    }

    private void MoveSpawner(float distanceY)
    {
        transform.Translate(Vector2.up * distanceY);
    }
}
