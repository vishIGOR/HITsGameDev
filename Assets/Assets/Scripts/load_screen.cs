using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_screen : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(int level){
        SceneManager.LoadScene(level);
    }
    public void Exit(){
        Application.Quit();
    }
}
