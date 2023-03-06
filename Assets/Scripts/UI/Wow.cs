using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wow : MonoBehaviour
{
    public GameObject CurWow;
    public float TimeWaitWow;

    public List<GameObject> ListSmallWow, ListBigWow;  
    public GameObject ObjWow;      

    public void ShoveWow(string type)
    {
        if (CurWow != null) {StopCoroutine(nameof(Shove)); CurWow.SetActive(false);}

        switch (type)
        {
            case "big" : CurWow = ListBigWow[Random.Range(0, ListBigWow.Count)]; break;
            case "small" : CurWow = ListSmallWow[Random.Range(0, ListSmallWow.Count)]; break;
            case "obj" : CurWow = ObjWow; break;
            default: break;
        }

        StartCoroutine(nameof(Shove));
    }
                                             
    private IEnumerator Shove()  
    {
        CurWow.SetActive(true);
        yield return new WaitForSeconds(TimeWaitWow);   
        CurWow.SetActive(false); 
        
    }

}
