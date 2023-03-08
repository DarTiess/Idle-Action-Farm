using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrounding : MonoBehaviour
{
    [SerializeField]private float _maxSize;
    [SerializeField]private float _growDuration;
    private GrassSlicer _slicer;
    [SerializeField] private GrassPlane _plane;
    private CapsuleCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        
        _collider= GetComponent<CapsuleCollider>();
        _collider.enabled = false;
        _plane.gameObject.SetActive(false);
        _slicer=GetComponent<GrassSlicer>();
           _slicer.enabled = false;
        GroundingGrass();
    }

  
    private void GroundingGrass()
    {
        transform.DOScaleY(_maxSize, _growDuration)
            .OnComplete(() =>
            {
               _collider.enabled = true;
              
                _slicer.enabled = true;
               _plane.gameObject.SetActive(true);
            });
    }
}
