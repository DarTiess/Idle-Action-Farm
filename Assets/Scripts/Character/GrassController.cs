using System.Collections.Generic;
using UnityEngine;
public class GrassController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _grassPrefabs;
    [SerializeField] private int _grassNumber = 64;
    [SerializeField] private float _width;
    [SerializeField] private float _depth;

    private Transform _ground;
    private List<GameObject> _grass = new List<GameObject>();

    void Start()
    {
        _ground = transform;
        float groundWidthHalf = _width / 2;
        float groundDepthHalf = _depth / 2;

        for (int grassIndex = 0; grassIndex < _grassNumber; grassIndex++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-groundWidthHalf, groundWidthHalf), 0, Random.Range(-groundDepthHalf, groundDepthHalf));

            GameObject newGrass = Instantiate(_grassPrefabs[Random.Range(0, _grassPrefabs.Count)], position, Quaternion.Euler(0, Random.Range(0, 360), 0), _ground.transform);

            _grass.Add(newGrass);
        }
        gameObject.tag = "GrassPlace";
    }

}
