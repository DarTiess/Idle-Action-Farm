using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.ProceduralImage;     


public class InGameUI : MonoBehaviour
{
    public Text CoinCounter, NumLevel; 
    public GameObject RegionLineBar, RegionRadialBar;

    public RectTransform ImageLineKnob, ImageRadialKnob;

   
    
    public Image Fill;

#pragma warning disable 0649    
    [Header("StageBar")]            
    [SerializeField] private GameObject _regionStageBar;
    public float DefaultWh, CurWh;          
    [SerializeField] private RectTransform _rectStage1, _rectStage2; 
    [SerializeField] private GameObject _stageComplete1, _stageComplete2, _stageComplete3;              
    
    [Header("RadialBarArrow")]            
    [SerializeField] private GameObject _regionRadialBarArrow;
    [SerializeField] private Image _fillBarArrow;    
    [SerializeField] private GameObject _heightArrowe2, _distanArrowtage2;
    
    [Header("RadialBarSimple")]            
    [SerializeField] private GameObject _regionRadialBarSimple;
    [SerializeField] private Image _fillBarSimple;
    [SerializeField] private Image _smileySimple, _smileySimpleDefault, _smileySimpleHappy;   
    [SerializeField] private Color _colorSimpleStart, _colorSimpleDone;  
                                               
    [Header("RadialBarLosing")]            
    [SerializeField] private GameObject _regionRadialBarLosing;
    [SerializeField] private Image _fillBarLosing;                     
    [SerializeField] private Image _smileyLosing, _smileyLosingLosing, _smileyLosingBead, _smileyLosingStart, _smileyLosingGood, _smileyLosingHappy;  
    [SerializeField] private Color _colorLosingStart, _colorLosingDone, _colorLosingLost;
    [SerializeField] private ParticleSystem _particleGood, _particleBead;
    
#pragma warning restore 0649                                                         

    public void Set()
    {
        if(CoinCounter) {CoinCounter.text = PlayerPrefs.GetInt("CoinCount").ToString();} 
        if(NumLevel) {NumLevel.text = PlayerPrefs.GetInt("NumLevel").ToString();}  
    }       

    #region Bars
    
    public void SetLinePBar(float all, float cur)   
    {
        RegionLineBar.SetActive(true);
        float fill = 1 - (cur/all);  
        Fill.fillAmount = fill;
        
        if (!ImageLineKnob) return;  
         Vector3 temp = ImageLineKnob.localPosition;
         temp.x = Mathf.Lerp(-250, 250, fill);    
         ImageLineKnob.localPosition = temp;  
    }

    public void SetStageBar(int stage, bool isDone)
    {
        _regionStageBar.SetActive(true); 
//        _rectStage1.sizeDelta = _rectStage2.sizeDelta = new Vector2(DefaultWh,DefaultWh);
        _stageComplete1.SetActive(false); _stageComplete2.SetActive(false);_stageComplete3.SetActive(false);
        switch (stage)
        {
            case 1:// _rectStage1.sizeDelta = new Vector2(CurWh,CurWh);      
                if (isDone){ _stageComplete1.SetActive(true);}

                break;
            case 2:// _rectStage2.sizeDelta = new Vector2(CurWh,CurWh);   
                _stageComplete1.SetActive(true);
                if (isDone){ _stageComplete2.SetActive(true);}
                break;
            case 3: //_rectStage3.sizeDelta = new Vector2(CurWh,CurWh);   
                _stageComplete1.SetActive(true); _stageComplete2.SetActive(true);
                if (isDone){ _stageComplete3.SetActive(true);}
                break;
        }
    }
    
    public void SetRadialBarArrow(float all, float cur)    
    {
        RegionRadialBar.SetActive(true);
        float fill = cur/all;

        Vector3 temp = ImageRadialKnob.GetComponent<RectTransform>().localEulerAngles;
        temp.z = Mathf.Lerp(500, 220, fill);
        ImageRadialKnob.GetComponent<RectTransform>().localEulerAngles = temp;
    }

    public void SetRadialBarSimple(float all, float cur)
    {
        FalseRadialBars("simple");
        _fillBarSimple.color = _colorSimpleStart;
        _smileySimple.sprite = _smileySimpleDefault.sprite;
        if(!_regionRadialBarSimple.activeInHierarchy) {_regionRadialBarSimple.SetActive(true);}  
        _regionRadialBarSimple.GetComponent<Animator>().SetTrigger("anim");
        float fill = cur/all;
        _fillBarSimple.fillAmount = fill;                         
        if (fill > .99f){_smileySimple.sprite = _smileySimpleHappy.sprite; _fillBarSimple.color = _colorSimpleDone;}  
    }

    public void SetRadialBarLosing(float fill)  
    {
        FalseRadialBars("losing");
        if (!_regionRadialBarLosing.activeInHierarchy) {_regionRadialBarLosing.SetActive(true);} 
      //  _regionRadialBarLosing.GetComponent<Animator>().SetTrigger("anim");
        
       //  0.3 - 1
       // if (fill > .3f) {_fillBarLosing.color = Color.Lerp(_colorLosingStart, _colorLosingDone, fill);}
       // if (fill < .3f) {_fillBarLosing.color = Color.Lerp(_colorLosingStart, _colorLosingLost, fill);}
                                       

        StartCoroutine(nameof(SetFill), fill);    
         
        
        
         _smileyLosing.sprite = _smileyLosingLosing.sprite; _fillBarLosing.color = _colorLosingLost;
        if (fill >0){_smileyLosing.sprite = _smileyLosingBead.sprite; }     
        if (fill >.2f){_smileyLosing.sprite = _smileyLosingStart.sprite;_fillBarLosing.color = _colorLosingStart;}   
        if (fill >.8f) {_smileyLosing.sprite = _smileyLosingGood.sprite;_fillBarLosing.color = _colorLosingDone;}
        if (fill >=1) {_smileyLosing.sprite = _smileyLosingHappy.sprite;}        
    }
    private IEnumerator SetFill(float fill)
    {
        _fillBarLosing.fillAmount = fill;
        yield return null;
        float lastFill = _fillBarLosing.fillAmount;
        if(lastFill<fill){_particleGood.Play();}
        if(lastFill>fill){_particleBead.Play();}

        for (int i = 0; i <= 10; i++)
        {
            _fillBarLosing.fillAmount = Mathf.Lerp(lastFill, fill, i * .1f);
            yield return new WaitForSeconds(.001f);     
        }

    }
     
    public void FalseRadialBars(string type)      
    {
        switch (type)
        {
            case "simple": _regionRadialBarArrow.SetActive(false);
                           _regionRadialBarLosing.SetActive(false); break;
            case "losing": _regionRadialBarArrow.SetActive(false);
                           _regionRadialBarSimple.SetActive(false); break;
            case "all":    _regionRadialBarArrow.SetActive(false);
                           _regionRadialBarSimple.SetActive(false);
                           _regionRadialBarLosing.SetActive(false);break;
        }
    }


    #endregion

}
