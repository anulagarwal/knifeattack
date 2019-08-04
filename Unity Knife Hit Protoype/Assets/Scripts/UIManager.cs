using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    //UI class to handle UI events
    public TextMeshProUGUI Level;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject NextLevelPanel;
    public GameObject PausePanel;
    public Sprite BonusInstructions;
    public Sprite DefaultInstructions;
    public TextMeshProUGUI NextLevel;
    public bool IsUIActive;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateLevel(int value)
    {
        Level.text = "" + value + "\n Stage 1";
        NextLevel.text = "Level " + value;
    }
    
    public void OpenPausePanel()
    {
        PausePanel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }
    public void OpenWinPanel()
    {
        WinPanel.SetActive(true);
    }
    public void OpenNextLevelPopup()
    {    
        NextLevelPanel.SetActive(true);
    }
    public void OpenLosePanel()
    {
        LosePanel.SetActive(true);
    }
    public void SetBonusInstructions()
    {
        PausePanel.transform.GetChild(0).GetComponent<Image>().sprite = BonusInstructions;
    }
    public void SetDefaultInstructions()
    {
        PausePanel.transform.GetChild(0).GetComponent<Image>().sprite = DefaultInstructions;

    }
}
