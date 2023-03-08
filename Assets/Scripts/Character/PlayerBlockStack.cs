using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockStack : MonoBehaviour
{

    public event Action IsFull;
    public event Action CanStacking;

    [SerializeField] private float _cutCount;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Transform _blockPlace;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDuretion;
    [SerializeField] private float _blockHeight;
    [SerializeField] private int _limitOfBlocks;
      [SerializeField] private float _timeMagnet;
    [SerializeField] private int _countSteepMagnet;
    [SerializeField] private AnimationCurve _changeY;
    private float _steep;
    private float _timeInSteep;

    private int _blockIndex=0;
    private float _cutIndex=0;
    private float _startBlockPosition;
    private List<Block> _blocksInStack= new List<Block>();


    private void Start()
    {
        _startBlockPosition=_blockPlace.position.y;
             _steep = 1f / _countSteepMagnet;
        _timeInSteep = _timeMagnet / _countSteepMagnet;
        CreateBlocksPull();
    }

    private void CreateBlocksPull()
    {
        for(int i = 0; i < _limitOfBlocks; i++)
        {
              Block block = Instantiate(_blockPrefab,transform.position, transform.rotation);
             block.gameObject.SetActive(false);
            _blocksInStack.Add(block);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Block"))
        {
            StackBlocks(other.gameObject.GetComponent<Block>());
        }
    }

    public void CreateBlockOfGrass(Transform grass)
    {
          if (_blockIndex >= _limitOfBlocks)
        {
            return;
        }
        _cutIndex += 1;
        if (_cutIndex <= _cutCount)
        {
            return;
        }
      
      _blocksInStack[_blockIndex].gameObject.SetActive(true);
     
        int rnd= UnityEngine.Random.Range(-1,1);
        if(rnd == 0)
        {
            rnd=1;
        }
        float zPos= gameObject.transform.position.z + 3 * rnd;
        Vector3 place= new Vector3(0,0,zPos);
         _blocksInStack[_blockIndex].transform.tag="Block";
        _blocksInStack[_blockIndex].MoveToTarget(grass.position,place, _jumpForce, _jumpDuretion);
        
     // _blocksInStack[_blockIndex].PushingBlock(grass, _countSteepMagnet, _steep, _changeY,_timeInSteep);
        _cutIndex = 0;
      _blockIndex++;
      
         if (_blockIndex >= _limitOfBlocks)
        {
           IsFull?.Invoke();
        }
    }

    private void StackBlocks(Block block)
    { 
        block.tag="Untagged";
        Vector3 place = new Vector3(_blockPlace.position.x, _startBlockPosition,_blockPlace.position.z);
       
        block.MoveToTarget(block.transform.position,place, _jumpForce, _jumpDuretion);
        
     //   block.PushingBlock(_blockPlace, _countSteepMagnet, _steep, _changeY,_timeInSteep);
        block.transform.parent = _blockPlace.transform;
           _startBlockPosition+=_blockHeight;
    }
}
