using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
  public void OnclickReset(){
    Debug.Log("reset");
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
