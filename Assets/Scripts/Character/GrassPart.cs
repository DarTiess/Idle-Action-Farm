using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPart : MonoBehaviour
{
    private float _timer;
    private GrassGrounding _grassParent;
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }
      StartCoroutine(DestroyPart());
    }
    public void Dissapearing(float timer, GrassGrounding grassParent)
    {
        _timer= timer;
        _grassParent= grassParent;
    }

    IEnumerator DestroyPart()
    {
     _grassParent.StartGroundAgain();   
        yield return new WaitForSeconds(0.3f);
          Destroy(gameObject);
    }
}
