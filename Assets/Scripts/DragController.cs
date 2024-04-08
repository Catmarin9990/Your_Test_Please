using System;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public DragAndDrop LastDragged => lastDragged;

    private bool isDragActive = false;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 mousPosOffset;

    private DragAndDrop lastDragged;

    private GameController gameController;

    private Rigidbody2D rb;

    // Layer Settings
    private SpriteRenderer layerOrder;
    [SerializeField] private int defaultOrder = 2;

    //sort settings
    [NonSerialized] public GameObject previousGameObject;

    private StampScript stamp;

    private void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {

        if (isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else return;


        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    DragAndDrop drag = hit.transform.gameObject.GetComponent<DragAndDrop>();
                    TestSpawner icon = hit.transform.gameObject.GetComponent<TestSpawner>();
                    if (drag != null)
                    {
                        lastDragged = drag;
                        InItDrag();
                    }
                    else if(gameController.canSpawn && icon != null)
                    {
                        icon.spawnTest();
                    }
                }
            }

        }
    }

    private void InItDrag()
    {

        lastDragged.lastPosition = lastDragged.transform.position;
        mousPosOffset = lastDragged.transform.position - worldPosition;
        UpdateDragStatus(true);
        if (lastDragged.gameObject.GetComponent<StampScript>() != null)
        {
            stamp = lastDragged.gameObject.GetComponent<StampScript>();
        }
        if (lastDragged.isFirst) return;
        if (lastDragged.isSorting)
        {
            foreach (GameObject doc in gameController.documents)
            {
                if (doc == lastDragged.gameObject)
                {
                    doc.GetComponent<DragAndDrop>().TheFirstLayer();
                }
                else if (doc == previousGameObject)
                {
                    doc.GetComponent<DragAndDrop>().PreviousLayer();
                }
                else
                {
                    doc.GetComponent<DragAndDrop>().DefaultLayer();
                }
            }
        }
        
    }

    private void Drag()
    {
        lastDragged.transform.position = new Vector3(worldPosition.x, worldPosition.y) + mousPosOffset;
        if (stamp != null && lastDragged.gameObject == stamp.gameObject)
        {
            stamp.inAnim = true;
        }
    }

    private void Drop()
    {
        UpdateDragStatus(false);
        if (stamp != null && stamp.gameObject == lastDragged.gameObject)
        {
            stamp.inAnim = true;
            stamp.animator.SetBool("IsStamping", true);
        }
        if (lastDragged.isSorting && !gameController.student.isWriting)
        {
            if (gameController.student.isDocIn && lastDragged.gameObject.GetComponent<ItemScript>().type == ItemScript.typeOfDoc.test)
            {
                gameController.getTests();
                gameController.removeDocument(lastDragged.gameObject);
            }
            else if (gameController.student.isDocIn)    
            {
                gameController.removeDocument(lastDragged.gameObject);
            }
        }
        previousGameObject = lastDragged.gameObject;
    }

    private void UpdateDragStatus(bool isDragging)
    {
        isDragActive = lastDragged.isDragging = isDragging;
    }
}
