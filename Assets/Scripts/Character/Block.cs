using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveToTarget(Vector3 startPlace, Transform target, float jumpForce, float jumpDuretion)
    {
        transform.position=startPlace;
          transform.DOJump(target.position, jumpForce, 1, jumpDuretion)
        .OnComplete(() =>
        {
            transform.position =target.position;
             transform.tag="Block";
        });
    }

}
