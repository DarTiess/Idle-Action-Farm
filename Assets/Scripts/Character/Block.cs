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
    public void MoveToTarget(Vector3 startPlace, Vector3 target, float jumpForce, float jumpDuretion)
    {
        transform.position=startPlace;
          transform.DOJump(target, jumpForce, 1, jumpDuretion)
        .OnComplete(() =>
        {
            transform.position =target;
             transform.tag="Block";
        });
    }

    public void PushingBlock(Transform destin, int _countSteepMagnet, float _steep, AnimationCurve _changeY, float _timeInSteep)
    {
        StartCoroutine(Pushing(destin, _countSteepMagnet,  _steep, _changeY,  _timeInSteep));
    }
      private IEnumerator Pushing(Transform destin, int _countSteepMagnet, float _steep, AnimationCurve _changeY, float _timeInSteep)
    {

        for (int i = 0; i <= _countSteepMagnet; i++)
        {

            Vector3 pos = Vector3.Lerp(transform.position, destin.transform.position, i * _steep);
            pos.y += _changeY.Evaluate(i * _steep);
            transform.position = pos;

            transform.rotation = Quaternion.Lerp(transform.rotation, destin.transform.rotation, i * _steep);


            yield return new WaitForSeconds(_timeInSteep);

        }
        transform.parent = destin.transform;
       transform.position = destin.transform.position;
         transform.rotation = destin.transform.rotation;
    }
}
