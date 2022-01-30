using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class toheaven : MonoBehaviour
{
    public void loadscene(string sceneName)
    {
            SceneManager.LoadScene(sceneName); 
    }

}
