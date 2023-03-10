using DG.Tweening;
using System;
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
    private bool _stacking;
    private PlayerMoneyStack _moneyStack;
    private Rigidbody _rb;
    private Economics _economics;

    [Inject]
    private void Construct(Economics economics)
    {
        _economics = economics;
    }

    private void Start()
    {
        _moneyStack = GetComponent<PlayerMoneyStack>();
        _economics.MaxBlockSize = _limitOfBlocks;
        _moneyStack.CreateCoinsPull(_limitOfBlocks);
        _rb = GetComponent<Rigidbody>();
        CreateBlocksPull();
        CreateBlocksInStackPull();
    }

    private void CreateBlocksPull()
    {
        for (int i = 0; i < _limitOfBlocks; i++)
        {
            Block block = Instantiate(_blockPrefab, transform.position, transform.rotation);
            block.InitBlockInScene();

            block.gameObject.SetActive(false);
            _blocks.Add(block);
        }
    }

    private void CreateBlocksInStackPull()
    {
        _startBlockPosition += _blockHeight;

        Block firstBlock = Instantiate(_blockPrefab, _blockPlace.transform.position, _blockPlace.rotation);

        firstBlock.InitFirstBlockInStack(_blockPlace, _rb);
        _blocksInStack.Add(firstBlock);

        for (int i = 1; i < _limitOfBlocks; i++)
        {
            _startBlockPosition += _blockHeight;

            Block block = Instantiate(_blockPrefab, new Vector3(_blockPlace.position.x, _startBlockPosition, _blockPlace.position.z), _blockPlace.rotation);
            block.InitBlockInStack(_blockPlace, _blocksInStack[i - 1].Rigidbody);
            _blocksInStack.Add(block);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            if (!_stacking)
            {
                StackBlocks(other.gameObject.GetComponent<Block>());
            }
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

                Transform newPosition = grass;
                newPosition.position = new Vector3(grass.position.x, grass.position.y + 1f, grass.position.z);

                block.MoveToTarget(transform.position, newPosition, _jumpForce, _jumpDuretion);
                block.transform.tag = "Block";

                _cutIndex = 0;
                return;
            }
        }
    }

    private void StackBlocks(Block block)
    {
        _stacking = true;
        block.tag = "Untagged";

        _economics.GetBlock(1);

        block.transform.DOJump(_blocksInStack[_blockIndex].transform.position, _jumpForce, 1, _jumpDuretion)
       .OnComplete(() =>
       {
           block.gameObject.SetActive(false);

           _blocksInStack[_blockIndex].GetComponent<MeshRenderer>().enabled = true;
           _blockIndex++;

           if (_blockIndex >= _limitOfBlocks)
           {
               _isFullingStack = true;
               IsFull?.Invoke();
           }
           _stacking = false;
       });

    }
    private void SaleBlocks(Transform storeHouse)
    {
        if (_blockIndex <= 0)
        {
            return;
        }
        _onSaling = true;
        Block lastBlock = null;
        foreach (Block bl in _blocks)
        {
            if (!bl.gameObject.activeInHierarchy)
            {
                lastBlock = bl;
                lastBlock.gameObject.SetActive(true);
                lastBlock.transform.position = gameObject.transform.position;
                break;
            }
        }

        lastBlock.transform.DOJump(storeHouse.position, _jumpForce, 1, _jumpDuretion)
        .OnComplete(() =>
        {
            _blockIndex--;
            _onSaling = false;
            _moneyStack.PushCoinsToBank(storeHouse);
            lastBlock.transform.parent = null;
            lastBlock.transform.position = storeHouse.position;
            lastBlock.gameObject.SetActive(false);
            _blocksInStack[_blockIndex].SaleBlockFromStack();

            if (_isFullingStack)
            {
                _isFullingStack = false;
                CanStacking?.Invoke();
            }
        });
    }
}
