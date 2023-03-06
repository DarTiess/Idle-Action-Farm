using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrounding : MonoBehaviour
{
    [SerializeField]private float maxSize;
    [SerializeField]private float growDuration;
    // Start is called before the first frame update
    void Start()
    {
        GroundingGrass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GroundingGrass()
    {
        transform.DOScaleY(maxSize,growDuration);
    }
}
