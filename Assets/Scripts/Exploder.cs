using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void ExplodeCubes(List<GameObject> cubes)
    {
        List<Rigidbody> explodableObjects = GetRigidbodiesCubes(cubes);

        foreach (Rigidbody explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
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
}