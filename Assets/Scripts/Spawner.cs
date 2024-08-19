using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _probabilitySeparation = 100;

    private static int s_minAmountCubes = 2;
    private static int s_maxAmountCubes = 6;
    private static int s_minPercent = 0;
    private static int s_maxPercent = 100;
    private static int s_reductionMultiplierForScale = 2;
    private static int s_reductionMultiplierForProbability = 2;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    public bool TrySpawnCubes(out List<GameObject> cubes)
    {
        cubes = new List<GameObject>();

        if (_probabilitySeparation >= Random.Range(s_minPercent, s_maxPercent + 1))
        {
            _probabilitySeparation /= s_reductionMultiplierForProbability;

            cubes = CreateCubes();
            InstatiateCubes(cubes);

            return true;
        }
        else
        {
            Destroy(gameObject);

            return false;
        }
    }

    private List<GameObject> CreateCubes()
    {
        List<GameObject> cubes = new List<GameObject>();
        int amountNewCubes = Random.Range(s_minAmountCubes, s_maxAmountCubes + 1);
        Vector3 scale = transform.localScale / s_reductionMultiplierForScale;

        for (int i = 0; i < amountNewCubes; i++)
        {
            var newCube = gameObject;
            newCube.transform.localScale = scale;
            gameObject.GetComponent<Renderer>().material.color =
            new Color(Random.value, Random.value, Random.value);
            cubes.Add(newCube);
        }

        return cubes;
    }

    private void InstatiateCubes(List<GameObject> cubes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            Instantiate(cubes[i], transform.position, Quaternion.identity);
        }
    }
}