using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Simple level class - We can add as many levels using the editor 
[System.Serializable]
public class Level 
{
    public int KnifeCount;
    public int LevelID;
    public GameManager.GameMode mode;
    public int Speed;
    public List<LevelSpeedCurve> curve;
    [System.Serializable]
    public class LevelSpeedCurve
    {
        public float speed;
        public float duration;
    }
}
