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

    // W tej klasie przechowywane sa dane kazdego poziomu, typ, wlasciwosci, kolejnosc celow oraz przerwy pomiedzy nimi.
    // O ile logicznym wydaje si� przechowywanie takich danych w oddzielnym miejscu, tak nie uda�o znale�� mi si� informacji *jak dobrze to zrobi�*.
    // W ten spos�b? Przypisa� je w edytorze? Pod��czy� pod plik .json?

    public void Level1()
    {
        // new() = new List<LevelData>
        // 0 - spinny, 1 - updown, 2 - left, 3 - right, 4 - spinny mirror, 5 - zigzag, 6 - downlow
        // level 1 speed: 1.25
        List<LevelData> table = new();
        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = 1f, delay = 1f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = 0f, delay = 3f });

        table.Add(new LevelData() { targetType = 3, locationX = -6f, locationY = 2.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -4.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = -0.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -1.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 0f, locationY = -2.75f, delay = 3f });

        table.Add(new LevelData() { targetType = 2, locationX = 6f, locationY = 2.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 4.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = -0.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 0f, locationY = -2.75f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = 0f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = 1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = -1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = -3f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 0f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 4, locationX = -1f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 0, locationX = -6f, locationY = 1.5f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = -5f, locationY = -2f, delay = 1f });
        table.Add(new LevelData() { targetType = 0, locationX = 6f, locationY = 2f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = 5f, locationY = -1.5f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = 0f, locationY = 1f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = -1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -4.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = -5f, locationY = -2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = 5f, locationY = -1.5f, delay = 3f });

        finishedTable = table; 
    }

    public void Level2()
    {
        List<LevelData> table = new();
        //table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = 1f, delay = 1f });
        finishedTable = table;
    }
}
