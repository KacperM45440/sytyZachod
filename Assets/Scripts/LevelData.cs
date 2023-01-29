using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int targetType;
    public float locationX;
    public float locationY;
    public float delay;
    public List<LevelData> finishedTable;
    public void Level1()
    {
        // new() = new List<LevelData>
        // 0 - spinny, 1 - updown, 2 - left, 3 - right
        List<LevelData> table = new();
        table.Add(new LevelData() { targetType = 3, locationX = 5f, locationY = 1f, delay = 1f });
        table.Add(new LevelData() { targetType = 2, locationX = 9f, locationY = 0f, delay = 3f });

        table.Add(new LevelData() { targetType = 3, locationX = 1f, locationY = 2.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 2.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 4f, locationY = -0.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 5.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 7f, locationY = -2.75f, delay = 3f });

        table.Add(new LevelData() { targetType = 2, locationX = 13f, locationY = 2.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 11.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 10f, locationY = -0.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 8.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 7f, locationY = -2.75f, delay = 3f });
        finishedTable = table; 
    }
}
