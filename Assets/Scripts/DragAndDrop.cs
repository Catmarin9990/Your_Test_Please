using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousPosOffset;
    
    private GameController gameController;
    private Rigidbody2D rb;

    // Layer Settings
    private SpriteRenderer layerOrder;
    [SerializeField] private int defaultOrder = 1;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindAnyObjectByType<GameController>();

        layerOrder = spriteRenderer;

        rb = GetComponent<Rigidbody2D>();
    }

    

    private Vector3 getMousePosition()
    {
        // Capture mouse position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        // Capture mouse offset
        mousPosOffset = gameObject.transform.position - getMousePosition();

        // Layer Ordering

        foreach (Canvas c in gameController.canvasList)
        {   
            if(c != null && c != GetComponentInChildren<Canvas>())
            {
                c.sortingOrder = defaultOrder + 1;
            }
            else if(c != null)
            {
                GetComponentInChildren<Canvas>().sortingOrder += 2;
            }
            if (c != null && c == gameController.previousCanvas)
            {
                gameController.previousCanvas.sortingOrder++;
            }

        }

        layerOrder.sortingOrder += 2;
        SpriteRenderer spriteRenderer;
        foreach(GameObject doc in gameController.documents) { 
            if(doc != gameObject) {
                spriteRenderer = doc.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = defaultOrder;
            }
            if(doc == gameController.previousGameObject)
            {
                gameController.previousGameObject.GetComponent<SpriteRenderer>().sortingOrder++;
            }
        }
        gameController.previousCanvas = GetComponentInChildren<Canvas>();
        gameController.previousGameObject = gameObject;
    }
    private void OnMouseDrag()
    {
        // move object with mouse
        transform.position = getMousePosition() + mousPosOffset;
    }

    private void OnMouseUp()
    {
            
        if (gameController.student.isDocIn && gameObject.GetComponent<ItemScript>().type == ItemScript.typeOfDoc.test)
        {
            gameController.getTests();
            gameController.removeDocument(gameObject);
            gameController.canSpawn = true;
        }
        else if (gameController.student.isDocIn)
        {
            gameController.removeDocument(gameObject);
        }
    }


}
