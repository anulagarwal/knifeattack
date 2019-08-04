using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public void ChangeScene(string name)
    {
        PlayerPrefs.SetInt("Level", -1);
        SceneManager.LoadScene(name);
    }
}
