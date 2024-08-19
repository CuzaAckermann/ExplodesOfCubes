using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Exploder))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnMouseDown()
    {
        if (_spawner.TrySpawnCubes(out List<GameObject> cubes))
        {
            _exploder.ExplodeCubes(cubes);

            Destroy(gameObject);
        }
    }
}