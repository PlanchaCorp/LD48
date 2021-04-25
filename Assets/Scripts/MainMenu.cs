using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
