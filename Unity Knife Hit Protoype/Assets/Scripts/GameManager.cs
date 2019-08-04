using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion
    public int CurrentLevelObjective = 5;
    public int CurrentKnifeHitCount = 0;
    public bool IsPaused = false;
    public List<Level> levels;
    public Circle circle;
    private int LevelID;
    private int LevelSpeed;
    private int KnifeCount;
    private int CurrentLevel=0;
    public bool IsBonusLevelOn=false;
    public bool IsDoubleBonusLevelOn = false;
    public KnifeLive kl;
    public enum GameMode { Simple, Emoji, Blub, Knife, Color};
    public GameMode gameMode;
    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.Find("Circle").GetComponent<Circle>();
        if (PlayerPrefs.GetInt("Level", -1) != -1)
        {
            SetLevel(PlayerPrefs.GetInt("Level", -1));
        }
        else
        {
            UpdateLevel();
        }
        CurrentKnifeHitCount = CurrentLevelObjective;
        circle.curve = levels[0].curve;
    }

    //Updating knife count
    public void UpdateKnifeHit()
    {
        CurrentKnifeHitCount -= 1;
        kl.RemoveKnife();
        if ( gameMode == GameMode.Emoji)
        {
            int temp = CurrentLevelObjective - CurrentKnifeHitCount;
            if (temp <= CurrentLevelObjective / 3)
            {
                circle.UpdateFace(1);
            }
            else if (temp <= CurrentLevelObjective / 3 * 2 && temp > CurrentLevelObjective / 3)
            {
                circle.UpdateFace(2);
            }
            else if (temp <= CurrentLevelObjective && temp > CurrentLevelObjective / 3 * 2)
            {
                circle.UpdateFace(3);
            }
        }
        if(CurrentKnifeHitCount == 0)
        {
            if (CurrentLevel < levels.Count)
            {
                OpenNextLevelPopup();
            }
            else
            {
                WinGame();
            }
        }
    }
    //Making emoji face level
    void MakeEmojiLevel()
    {
        UIManager.instance.SetDefaultInstructions();
        gameMode = GameMode.Emoji;
        circle.UpdateFace(1);
    }
    //Making the bonus blub level
    void MakeBlubLevel()
    {
        gameMode = GameMode.Blub;
        UIManager.instance.SetBonusInstructions();
        circle.UpdateFace(6);
    }
    //Making color changing circle level
    void MakeColorLevel(){
        gameMode = GameMode.Color;
        UIManager.instance.SetDefaultInstructions();
        circle.UpdateFace(7);
        }
    //To set the level after restarting the scene
    public void SetLevel(int levelNumber)
    {
        CurrentLevel = levelNumber;
        PlayerPrefs.SetInt("Level", CurrentLevel);
        if (CurrentLevel <= levels.Count)
        {
            if (levels[CurrentLevel - 1].mode == GameMode.Emoji)
            {
                MakeEmojiLevel();
            }
            else if (levels[CurrentLevel - 1].mode == GameMode.Blub)
            {
                MakeBlubLevel();
            }
            else if (levels[CurrentLevel - 1].mode == GameMode.Knife)
            {
                MakeKnifeLevel();
            }
            else if(levels[CurrentLevel-1].mode == GameMode.Color)
            {
                MakeColorLevel();
            }
            else
            {
                gameMode = GameMode.Simple;
                circle.UpdateFace(5);
            }
            LevelID = levels[CurrentLevel - 1].LevelID;
            LevelSpeed = levels[CurrentLevel - 1].Speed;
            KnifeCount = levels[CurrentLevel - 1].KnifeCount;
            circle.Speed = LevelSpeed;
            CurrentKnifeHitCount = CurrentLevelObjective = KnifeCount;
            circle.curve = levels[CurrentLevel - 1].curve;
            circle.currentCount = 0;
            UIManager.instance.UpdateLevel(LevelID);
            if (gameMode == GameMode.Blub)
            {
                circle.SpawnBlub(CurrentLevelObjective);
            }
            else if(gameMode== GameMode.Knife)
            {
                circle.SpawnKnifeInCircle(CurrentLevelObjective);
            }
            int x = 0;
            while (x < CurrentLevelObjective)
            {
                kl.SpawnKnifeLive();
                x++;
            }
        }
        else
        {
            WinGame();
        }
    }
    //To update the level after one other
    public void UpdateLevel()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("Level", CurrentLevel);   
        if (CurrentLevel <= levels.Count)
        {
            if (levels[CurrentLevel - 1].mode == GameMode.Emoji)
            {
                MakeEmojiLevel();
            }
            else if (levels[CurrentLevel - 1].mode == GameMode.Blub)
            {
                MakeBlubLevel();
            }
            else if (levels[CurrentLevel - 1].mode == GameMode.Knife)
            {
                MakeKnifeLevel();
            }
            else if (levels[CurrentLevel - 1].mode == GameMode.Color)
            {
                MakeColorLevel();
            }
            else
            {
                gameMode = GameMode.Simple;
                circle.UpdateFace(5);
            }
            LevelID = levels[CurrentLevel - 1].LevelID;
            LevelSpeed = levels[CurrentLevel - 1].Speed;
            KnifeCount = levels[CurrentLevel - 1].KnifeCount;
            circle.Speed = LevelSpeed;
            CurrentKnifeHitCount =CurrentLevelObjective = KnifeCount;
            circle.curve = levels[CurrentLevel-1].curve;
            circle.currentCount = 0;
            UIManager.instance.UpdateLevel(LevelID);
            if (gameMode == GameMode.Blub)
            {
                circle.SpawnBlub(CurrentLevelObjective);
            }
            else if (gameMode == GameMode.Knife)
            {
                circle.SpawnKnifeInCircle(CurrentLevelObjective);
            }
            int x = 0;
            while (x < CurrentLevelObjective)
            {
                kl.SpawnKnifeLive();
                x++;
            }
        }
        else
        {
            WinGame();
        }
    }
    //Making knife based level
    public void MakeKnifeLevel()
    {
        gameMode = GameMode.Knife;
    }
    public void WinGame()
    {
        UIManager.instance.OpenWinPanel();
    }
    public void LoseGame()
    {
        if(gameMode == GameMode.Emoji)
        circle.UpdateFace(4);
        UIManager.instance.OpenLosePanel();
    }
    public void NextLevel()
    {
        circle.UpdateFace(1);
        ClearScene();
        UpdateLevel();
    }
    public void OpenNextLevelPopup()
    {
        UIManager.instance.OpenNextLevelPopup();
    }
    public void TogglePause()
    {
        if (IsPaused)
        {
            IsPaused = false;
            UnPauseGame();
        }
        else
        {
            IsPaused = true;
            PauseGame();
        }
    }
    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        UIManager.instance.OpenPausePanel();
    }

    public void UnPauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        UIManager.instance.ClosePausePanel();
    }
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Play");
    }

    public void ClearScene()
    {
        circle.Clear();
    }
    public void LoadScene(string name)
    {
        UnPauseGame();
        SceneManager.LoadScene(name);
    }
    private void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
