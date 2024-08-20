using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Explode(List<GameObject> cubes)
    {
        List<Rigidbody> explodableObjects = IdentifyExpodableObjects(cubes);

        foreach (Rigidbody explodableObject in explodableObjects)
        {
            float distance = Vector3.Distance(explodableObject.transform.position, transform.position);
            
            if (distance > 0)
            {
                float finalForce = _explosionForce / distance / gameObject.transform.localScale.x;
                float finalRadius = _explosionRadius / gameObject.transform.localScale.x;

                explodableObject.AddExplosionForce(finalForce, transform.position, finalRadius);
            }
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> IdentifyExpodableObjects(List<GameObject> cubes)
    {
        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        if (cubes.Count > 0)
        {
            explodableObjects = GetRigidbodiesCubes(cubes);
        }
        else
        {
            explodableObjects = GetExplodableObjects();
        }

        return explodableObjects;
    }

    private List<Rigidbody> GetRigidbodiesCubes(List<GameObject> cubes)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (GameObject cube in cubes)
        {
            rigidbodies.Add(cube.GetComponent<Rigidbody>());
        }

        return rigidbodies;
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                explodableObjects.Add(rigidbody);
        }

        return explodableObjects;
    }
}