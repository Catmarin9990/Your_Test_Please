using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    private GameController gameController;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 mousPosOffset;

    private void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }
    
    public void spawnTest()
    {
        if (gameController.canSpawn)
        {
            gameController.canSpawn = false;
            gameController.giveBlankTest();
        }
    }
        
}
