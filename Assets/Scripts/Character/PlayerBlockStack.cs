using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockStack : MonoBehaviour
{
    [SerializeField] private float _cutCount;
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _blockPlace;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDuretion;
    [SerializeField] private float _blockHeight;

    private float _cutIndex=0;
    private float _startBlockPosition;

    private void Start()
    {
        _startBlockPosition=_blockPlace.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grass"))
        {
            CuttingGrass(other.gameObject.transform);
        }
        if (other.gameObject.CompareTag("Block"))
        {
            StackBlocks(other.gameObject);
        }
    }

    public void CuttingGrass(Transform grass)
    {
        _cutIndex += 1;
        if (_cutIndex <= _cutCount)
        {
            return;
        }
        GameObject block = Instantiate(_blockPrefab, grass.position, transform.rotation);
        int rnd=Random.Range(-1,1);
        if(rnd == 0)
        {
            rnd=1;
        }
        float zPos= gameObject.transform.position.z - 2 * rnd;
        Vector3 place= new Vector3(0,0,zPos);
      
        block.transform.DOJump(place, _jumpForce, 1, _jumpDuretion)
        .OnComplete(() =>
        {
            block.transform.position =place;
            block.tag="Block";
        });
        _cutIndex = 0;
      
    }

    private void StackBlocks(GameObject block)
    { 
        block.tag="Untagged";
        Vector3 place= new Vector3(_blockPlace.position.x, _startBlockPosition, _blockPlace.position.z);
        block.transform.DOJump(place, _jumpForce, 1, _jumpDuretion)
        .OnComplete(() =>
        {
            block.transform.position =place;
           
            block.transform.parent = _blockPlace.transform;
           _startBlockPosition+=_blockHeight;
        });
        _cutIndex = 0;
    }
}
