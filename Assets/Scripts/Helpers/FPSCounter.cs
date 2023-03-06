using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
   
    private int _accumulator = 0;
    private int _counter = 0;
    private float _timer = 0f;

    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _accumulator++;
        _timer += Time.deltaTime;
 
        if (_timer >= 1) {
            _timer = 0;
            _counter = _accumulator;    
            _accumulator = 0;
        }
        
        _text.text = "FPS: " + _counter;
    }
}
