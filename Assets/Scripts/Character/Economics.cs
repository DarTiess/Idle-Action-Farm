using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economics : MonoBehaviour
{
   [Header("InGame Menu")]
    [SerializeField] private Text _money;
    [SerializeField] private Text _block;
    [SerializeField] private Text _blockMax;
  private int _blockMaxSize;
    public int MaxBlockSize
    {
        get{ return _blockMaxSize;}
        set
        {
            _blockMaxSize= value;
            _blockMax.text="/"+_blockMaxSize.ToString();
        }
    }
    public int Money
    {
        get { return PlayerPrefs.GetInt("Money"); ; }
        set { PlayerPrefs.SetInt("Money", value); }
    }
    public int Block
    {
        get { return PlayerPrefs.GetInt("Block"); ; }
        set { PlayerPrefs.SetInt("Block", value); }
    }

    private void Start()
    {
        _money.text = Money.ToString();
        _block.text = Block.ToString();
       
    }

    public void GetMoney(int count)
    {
        int result = Money + count;

        _money.DOCounter(Money, result, 0.5f)
            .OnPlay(() =>
            {
                _money.transform.DOScale(1.5f, 0.5f)
                .OnComplete(() =>
                {
                    _money.transform.DOScale(1, 0.5f);
                });
            })
            .OnComplete(() =>
            {
                Money += count;
            });
      
    }
    public void GetBlock(int count)
    {
        int result = Block + count;

        _block.DOCounter(Block, result, 0.5f)
            .OnPlay(() =>
            {
                _block.transform.DOScale(1.5f, 0.5f)
                .OnComplete(() =>
                {
                    _block.transform.DOScale(1, 0.5f);
                });
            })
            .OnComplete(() =>
            {
                Block += count;
            });

    }

    public void BuyCoins()
    {
        if (Block >= 10)
        {
            int resultBlocks = Block % 10;
            int looseBlock = Block - resultBlocks;
            int addMoney = looseBlock / 2;
            int resultMoney = Money + addMoney;

            _money.DOCounter(Money, resultMoney, 0.5f)
          .OnPlay(() =>
          {
              _money.transform.DOScale(1.5f, 0.5f)
              .OnComplete(() =>
              {
                  _money.transform.DOScale(1, 0.5f);
              });
              _block.DOCounter(Block, resultBlocks, 0.5f);
          })
          .OnComplete(() =>
          {
              Money = resultMoney;
              Block = resultBlocks;
          });

        }
    }
}
