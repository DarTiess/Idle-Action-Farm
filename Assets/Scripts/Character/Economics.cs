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
 [SerializeField]private float _speedDuration;

    [Header("Blocks salle's settings")]
    [SerializeField]private int _blockPrice;
    public int MaxBlockSize
    {
        get{ return _blockMaxSize;}
        set
        {
            _blockMaxSize= value;
            _blockMax.text="/"+_blockMaxSize.ToString();
        }
    }
    public Text MoneyText
    {
        get { return _money; }
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
 private int _blockMaxSize;
    private void Start()
    {
        _money.text = Money.ToString();
        _block.text = Block.ToString();
       
    }
     
    public void GetBlock(int count)
    {
        int result = Block + count;

        _block.DOCounter(Block, result, _speedDuration)
            .OnPlay(() =>
            {
                _block.transform.DOScale(1.5f, _speedDuration)
                .OnComplete(() =>
                {
                    _block.transform.DOScale(1, _speedDuration);
                });
            })
            .OnComplete(() =>
            {
                Block += count;
            });
    }

    public void BuyCoins(int blockNum)
    {
        if(Block<=0) return;
        int restBlock = Block - blockNum;
        int resultMoney = Money + _blockPrice;

        _money.DOCounter(Money, resultMoney, _speedDuration)
      .OnPlay(() =>
      {
          _money.transform.DOScale(1.5f, _speedDuration)
          .OnComplete(() =>
          {
              _money.transform.DOScale(1, _speedDuration);
          });
          _block.DOCounter(Block, restBlock, _speedDuration);
      })
      .OnComplete(() =>
      {
          Money = resultMoney;
          Block = restBlock;
      });

    }
}
