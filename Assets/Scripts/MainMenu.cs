using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  private Transform tracker;
  [SerializeField]
  private RectTransform mainPanel;
  [SerializeField]
  private RectTransform levelsPanel;
  [SerializeField]
  private RectTransform creditsPanel;

  public void EnableLevelPanel(bool activate) {
    if (activate)
      tracker.position = levelsPanel.transform.position;
    else
      tracker.position = mainPanel.transform.position;
  }
  public void EnableCreditsPanel(bool activate) {
    if (activate)
      tracker.position = creditsPanel.transform.position;
    else
      tracker.position = mainPanel.transform.position;
  }
  public void onStart(){
      SceneManager.LoadScene(1);
  }
    public void onLevel1(){
SceneManager.LoadScene(1);
  }
      public void onLevel2(){
SceneManager.LoadScene(2);
  }
      public void onLevel3(){
SceneManager.LoadScene(3);
  }
      public void onLevel4(){
SceneManager.LoadScene(4);
  }


}
