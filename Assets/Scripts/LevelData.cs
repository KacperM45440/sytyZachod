using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public List<LevelData> finishedTable;
    public int targetType;
    public float delay;
    public float locationX;
    public float locationY;

    // W tej klasie przechowywane sa dane kazdego poziomu, typ, wlasciwosci, kolejnosc celow oraz przerwy pomiedzy nimi.
    // O ile logicznym wydaje siê przechowywanie takich danych w oddzielnym miejscu, tak nie uda³o znaleŸæ mi siê informacji *jak dobrze to zrobiæ*.
    // W ten sposób? Przypisaæ je w edytorze? Pod³¹czyæ pod plik .json?

    public void Level1()
    {
        // 0 - spinny, 1 - updown, 2 - left, 3 - right, 4 - spinny mirror, 5 - zigzag, 6 - downlow
        // Predkosc pierwszego poziomu wynosi 1 i jest podwajana w rundzie drugiej

        // new() = new List<LevelData>
        List<LevelData> table = new();

        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = 1f, delay = 1f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = 0f, delay = 3f });

        table.Add(new LevelData() { targetType = 3, locationX = -6f, locationY = 3.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -4.5f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = 0.75f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = 0f, locationY = -1.75f, delay = 3f });

        table.Add(new LevelData() { targetType = 2, locationX = 6f, locationY = 3.25f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 4.5f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = 0.75f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 0f, locationY = -1.75f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = 0f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = 1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = -1.5f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = -3f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 0f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 4, locationX = -1f, locationY = -0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 0, locationX = -6f, locationY = 2f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = -5f, locationY = -1f, delay = 1f });
        table.Add(new LevelData() { targetType = 0, locationX = 6f, locationY = 2f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = 5f, locationY = -1f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = 0f, locationY = 1f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = -1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 1, locationX = 1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -4.5f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1.5f, locationY = -1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = -5f, locationY = -1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = 5f, locationY = -1f, delay = 3f });

        finishedTable = table;
    }

    public void Level2()
    {
        List<LevelData> table = new();

        table.Add(new LevelData() { targetType = 5, locationX = 0f, locationY = -1f, delay = 3f });

        table.Add(new LevelData() { targetType = 5, locationX = -2f, locationY = -1f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 5, locationX = 0f, locationY = -1f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 5, locationX = 2f, locationY = -1f, delay = 1.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = -1f, delay = 1f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = -2f, delay = 3f });

        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = -2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = -1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = 0f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = 1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = 3f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = -3f, locationY = -0.5f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = -0.5f, delay = 1f });
        table.Add(new LevelData() { targetType = 3, locationX = -3f, locationY = 2f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 2, locationX = 3f, locationY = -1f, delay = 1f });
        table.Add(new LevelData() { targetType = 4, locationX = -1f, locationY = 0.5f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 0, locationX = 1f, locationY = 0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 6, locationX = -4f, locationY = 3f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 3, locationX = -4f, locationY = 3f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 6, locationX = -2f, locationY = 2.5f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 3, locationX = -2f, locationY = 2.5f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 6, locationX = 0f, locationY = 2f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 3, locationX = 0f, locationY = 2f, delay = 3f });

        table.Add(new LevelData() { targetType = 5, locationX = 4f, locationY = -1f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 2, locationX = 4f, locationY = -1f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 5, locationX = 2f, locationY = -0.5f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 2, locationX = 2f, locationY = -0.5f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 5, locationX = 0f, locationY = 0f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 2, locationX = 0f, locationY = 0f, delay = 2f });

        finishedTable = table;
    }

    public void Level3()
    {
        List<LevelData> table = new();

        table.Add(new LevelData() { targetType = 5, locationX = 4f, locationY = -1f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 2, locationX = 4f, locationY = -1f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 6, locationX = -4f, locationY = 3f, delay = 0.75f });
        table.Add(new LevelData() { targetType = 3, locationX = -4f, locationY = 3f, delay = 1.25f });
        table.Add(new LevelData() { targetType = 4, locationX = -1f, locationY = 0.5f, delay = 0.0f });
        table.Add(new LevelData() { targetType = 0, locationX = 1f, locationY = 0.5f, delay = 3f });

        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = 1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = 0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = 1.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 3, locationX = -1f, locationY = -0.5f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 2, locationX = 1f, locationY = 0.5f, delay = 3.0f });

        table.Add(new LevelData() { targetType = 4, locationX = 6f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 0, locationX = 5f, locationY = -1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 0, locationX = 0f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = -1f, locationY = -1f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 0, locationX = -6f, locationY = 2f, delay = 0.5f });
        table.Add(new LevelData() { targetType = 4, locationX = -5f, locationY = -1f, delay = 3f });

        table.Add(new LevelData() { targetType = 1, locationX = -6f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = -5f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = -4f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = -3f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = -2f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = -1f, locationY = -0.5f, delay = 0.25f });

        table.Add(new LevelData() { targetType = 1, locationX = 6f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = 5f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = 4f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = 3f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = 2f, locationY = -0.5f, delay = 0.25f });
        table.Add(new LevelData() { targetType = 1, locationX = 1f, locationY = -0.5f, delay = 3f });
        finishedTable = table;
    }
}
