using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int targetType;
    public float locationX;
    public float locationY;
    public int delay;
    public List<LevelData> finishedTable;
    public void Level1()
    {
        // new() = new List<LevelData>
        List<LevelData> table = new();
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = 1f, delay = 1 });
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = 1f, delay = 1 });
        table.Add(new LevelData() { targetType = 1, locationX = 4f, locationY = 2f, delay = 1 });
        table.Add(new LevelData() { targetType = 1, locationX = 4f, locationY = 2f, delay = 1 });
        table.Add(new LevelData() { targetType = 1, locationX = 5f, locationY = 3f, delay = 1 });
        table.Add(new LevelData() { targetType = 1, locationX = 5f, locationY = 3f, delay = 1 });
        finishedTable = table; 
    }
}
