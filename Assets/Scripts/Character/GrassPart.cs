using System.Collections;
using UnityEngine;

public class GrassPart : MonoBehaviour
{
    private float _timer;
    private GrassGrounding _grassParent;
    private bool _isDead;
    private void Update()
    {
        if (_isDead) return;
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }
        StartCoroutine(DestroyPart());
    }
    public void Dissapearing(float timer, GrassGrounding grassParent)
    {
        _timer = timer;
        _grassParent = grassParent;
    }

    IEnumerator DestroyPart()
    {
        _isDead = true;
        _grassParent.gameObject.SetActive(true);
        _grassParent.StartGroundAgain();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
