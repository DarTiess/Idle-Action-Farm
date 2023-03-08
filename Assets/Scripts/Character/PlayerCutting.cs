using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutting : MonoBehaviour
{
    [SerializeField]private GameObject _melee;
 
    private PlayerAnimator _animator;
    private PlayerBlockStack _blockStack;

    private void Start()
    {
        _blockStack=GetComponent<PlayerBlockStack>();
         _melee.SetActive(false);
      _animator = GetComponent<PlayerAnimator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GrassPlace"))
        {  
            _animator.CuttingAnimation();
            _melee.SetActive(true);
         // _blockStack.CuttingGrass(_melee.transform);
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("GrassPlace"))
        {
            _melee.SetActive(false);
            _animator.StopCuttingAnimation();
        }
    }

 
}
