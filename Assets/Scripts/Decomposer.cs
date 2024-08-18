using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Decomposer : MonoBehaviour
{
    [SerializeField] private float _probabilitySeparation = 100;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private static int s_minAmountCubes = 2;
    private static int s_maxAmountCubes = 6;
    private static int s_minPercent = 0;
    private static int s_maxPercent = 100;
    private static int s_reductionMultiplierForScale = 2;
    private static int s_reductionMultiplierForProbability = 2;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void OnMouseDown()
    {
        if (_probabilitySeparation >= Random.Range(s_minPercent, s_maxPercent + 1))
        {
            _probabilitySeparation /= s_reductionMultiplierForProbability;
            Split();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Split()
    {
        List<GameObject> cubes = CreateCubes();
        List<Rigidbody> rigidbodiesOfCubes = GetRigidbodiesCubes(cubes);
        InstatiateCubes(cubes);
        ExplodeCube(rigidbodiesOfCubes);

        Destroy(gameObject);
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
            cubes.Add(newCube);
        }

        return cubes;
    }

    private List<Rigidbody> GetRigidbodiesCubes(List<GameObject> cubes)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        for (int i = 0; i < cubes.Count; i++)
        {
            rigidbodies.Add(cubes[i].GetComponent<Rigidbody>());
        }

        return rigidbodies;
    }

    private void InstatiateCubes(List<GameObject> cubes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            Instantiate(cubes[i], transform.position, Quaternion.identity);
        }
    }

    private void ExplodeCube(List<Rigidbody> explodableObjects)
    {
        foreach (Rigidbody explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}