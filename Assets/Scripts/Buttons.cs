using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
     public GameObject NextLearn;
    public GameObject Learn;
    
    // Start is called before the first frame update
   public void Next()
    {
        Learn.SetActive(false);
        NextLearn.SetActive(true);
    }
    public void NextClose(){
        NextLearn.SetActive(false);
    }
    
}
