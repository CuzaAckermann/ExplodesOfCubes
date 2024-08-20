using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploder), typeof(Rigidbody))]

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
        List<GameObject> cubes = _spawner.SpawnCubes();

        _exploder.Explode(cubes);
    }
}