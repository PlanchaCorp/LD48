using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
  public void OnclickReset(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void OnEscape() {
    SceneManager.LoadScene("MainMenu");
  }
}
