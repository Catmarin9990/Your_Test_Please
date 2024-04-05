using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    private GameController gameController;
    private void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }

    private void OnMouseDown()
    {
        if (gameController.canSpawn)
        {
            gameController.canSpawn = false;
            gameController.giveBlankTest();
        }
    }
}
