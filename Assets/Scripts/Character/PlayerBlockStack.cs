using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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


    private int _blockIndex = 0;
    private float _cutIndex = 0;
    private float _startBlockPosition;
    private List<Block> _blocks = new List<Block>();
    private List<Block> _blocksInStack = new List<Block>();
    private bool _onSaling;
    private bool _isFullingStack;
    private PlayerMoneyStack _moneyStack;
    private Economics _economics;

    [Inject]
    private void Construct(Economics economics)
    {
        _economics = economics;
    }

    private void Start()
    {
        _moneyStack=GetComponent<PlayerMoneyStack>();
        _economics.MaxBlockSize = _limitOfBlocks;
        _moneyStack.CreateCoinsPull(_limitOfBlocks);
        CreateBlocksPull();
    }

    private void CreateBlocksPull()
    {
        for (int i = 0; i < _limitOfBlocks; i++)
        {
            Block block = Instantiate(_blockPrefab, transform.position, transform.rotation);
            block.gameObject.SetActive(false);
            _blocks.Add(block);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            StackBlocks(other.gameObject.GetComponent<Block>());
        }
      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StoreHouse"))
        {
            if (!_onSaling)
            {
                SaleBlocks(other.gameObject.transform);
            }

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
        foreach (Block block in _blocks)
        {
            if (!block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(true);

                int rnd = UnityEngine.Random.Range(-1, 1);
                if (rnd == 0)
                {
                    rnd = 1;
                }
                float zPos = gameObject.transform.position.z + 3 * rnd;

                block.transform.tag = "Block";
               block.MoveToTarget(grass.position, grass, _jumpForce, _jumpDuretion);

                _cutIndex = 0;
                return;
            }
        }
   
    }

    private void StackBlocks(Block block)
    {
        _blocksInStack.Add(block);
        block.tag = "Untagged";


        block.MoveToTarget(block.transform.position, _blockPlace, _jumpForce, _jumpDuretion);
        _economics.GetBlock(1);

        block.transform.parent = _blockPlace.transform.parent;
        _startBlockPosition += _blockHeight;
       _blockPlace.transform.position= new Vector3(_blockPlace.position.x, _startBlockPosition, _blockPlace.position.z);
       
        _blockIndex++;

        if (_blockIndex >= _limitOfBlocks)
        {
            _isFullingStack= true;
            IsFull?.Invoke();
        }
    }
    private void SaleBlocks(Transform storeHouse)
    {
        if (_blockIndex <= 0)
        {
            return;
        }
        _onSaling = true;
        Debug.Log("Storing blocks");
        Block lastBlock = _blocksInStack[_blockIndex - 1];

        lastBlock.transform.DOJump(storeHouse.position, _jumpForce, 1, _jumpDuretion)
        .OnComplete(() =>
        {
            _onSaling = false;
            _moneyStack.PushCoinsToBank(storeHouse);
            lastBlock.transform.parent = null;
            lastBlock.transform.position = storeHouse.position;
            lastBlock.gameObject.SetActive(false);
            _blocksInStack.Remove(lastBlock);

            _blockIndex--;
            _startBlockPosition -= _blockHeight;
            _blockPlace.transform.position = new Vector3(_blockPlace.position.x, _startBlockPosition, _blockPlace.position.z);
            if (_isFullingStack)
            {
                _isFullingStack = false;
                CanStacking?.Invoke();
            }
        });

    }

}
