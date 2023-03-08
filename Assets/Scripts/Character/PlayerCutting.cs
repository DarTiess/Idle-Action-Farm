using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutting : MonoBehaviour
{
    [SerializeField]private GameObject _melee;
 
    private PlayerAnimator _animator;
    private PlayerBlockStack _blockStack;
    private bool _isFull;

    private void Start()
    {
        _blockStack = GetComponent<PlayerBlockStack>();
        _melee.SetActive(false);
        _animator = GetComponent<PlayerAnimator>();
        _blockStack.IsFull += IsFull;
        _blockStack.CanStacking+=ContinueCutting;
    }

    private void OnDisable()
    {
        _blockStack.IsFull -= IsFull;
        _blockStack.CanStacking -= ContinueCutting;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GrassPlace"))
        {
            if (!_isFull)
            {
                _animator.CuttingAnimation();
                _melee.SetActive(true);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GrassPlace"))
        {
            StopCutting();
        }
    }
    private void IsFull()
    {
        _isFull = true;
        StopCutting();
    }
     private void ContinueCutting()
    {
      _isFull= false;
    }

    private void StopCutting()
    {
        _melee.SetActive(false);
        _animator.StopCuttingAnimation();
    }

}
