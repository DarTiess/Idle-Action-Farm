using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private GameObject _tutor1;
    [SerializeField] private GameObject _tutor2; 

#pragma warning restore 0649 
    
    
    // Start is called before the first frame update
    void Start()
    {   FalseTutor();  
        
    }


    public void ShowTutor(int x)
    {   FalseTutor();
        switch (x)
        {
            case 1:
                if (PlayerPrefs.GetInt("_tutor1") == 0) {_tutor1.SetActive(true); PlayerPrefs.SetInt("_tutor1",1);}
                break;
            case 2:                               
                if (PlayerPrefs.GetInt("_tutor2") == 0) {_tutor2.SetActive(true); PlayerPrefs.SetInt("_tutor2",1);}
                break;                                 
        }

        StartCoroutine(nameof(Wait));  
    }

    private IEnumerator Wait()
    { yield return new WaitForSeconds(5f);
       FalseTutor();   
    }

    public void FalseTutor()
    {
        _tutor1.SetActive(false);
        _tutor2.SetActive(false);
      
    }
}
