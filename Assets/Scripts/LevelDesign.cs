using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : Singleton<LevelDesign>
{
    public LevelData[] LevelDatas;
}

[System.Serializable]
public class LevelData
{
    public string LevelName;
    public int Level;
    public int ColAmount;
    public int SpeedBall;
    public int BrickAmount;
    public int Time;
}
