using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [NonSerialized] public bool isDragging = false;

    private DragController dragController;

    [Header("Position Settings")]
    [NonSerialized] public Vector3 lastPosition;
    private Vector3? mouvementDestination;
    [SerializeField] private float movementTime = 15f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Canvas orderLayer;

    [SerializeField] private int firstLayer = 5;
    [SerializeField] private int defaultLayer = 2;

    private bool isFirst = false;
    private bool isPrevious = false;
    private bool isDefault = true;

    public bool isSorting = true;

    private void Start()
    {
        dragController = FindObjectOfType<DragController>();
        
        rb = GetComponent<Rigidbody2D>();
        
        if(GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

    }

    private void Update()
    {
        if (isSorting)
        {
            if (isFirst)
            {
                spriteRenderer.sortingOrder = firstLayer;
                if (orderLayer != null)
                {
                    orderLayer.sortingOrder = firstLayer + 1;
                }
            }
            else if (isPrevious)
            {
                spriteRenderer.sortingOrder = defaultLayer + 1;
                if (orderLayer != null)
                {
                    orderLayer.sortingOrder = defaultLayer + 2;
                }
                else spriteRenderer.sortingOrder = defaultLayer + 2;
            }
            else if (isDefault)
            {
                spriteRenderer.sortingOrder = defaultLayer;
                if (orderLayer != null)
                {
                    orderLayer.sortingOrder = defaultLayer + 1;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (mouvementDestination.HasValue) 
        {
            if (isDragging)
            {
                mouvementDestination = null;
                return;
            }
            if(mouvementDestination == transform.position)
            {
                mouvementDestination = null;
                gameObject.layer = Layers.Default;
                return;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, mouvementDestination.Value, movementTime * Time.deltaTime);
            }

        }
    }

    public void TheFirstLayer()
    {
        if (GetComponentInChildren<Canvas>() != null)
        {
            orderLayer = GetComponentInChildren<Canvas>();
        }
        isFirst = true;
        isDefault = false;
        isPrevious = false;
        spriteRenderer.sortingOrder = firstLayer;
        if (orderLayer != null)
        {
            orderLayer.sortingOrder = firstLayer + 1;
        }
    }

    public void DefaultLayer()
    {
        if (GetComponentInChildren<Canvas>() != null)
        {
            orderLayer = GetComponentInChildren<Canvas>();
        }
        isFirst = false;
        isDefault = true;
        isPrevious = false;
        spriteRenderer.sortingOrder = defaultLayer;
        if (orderLayer != null)
        {
            orderLayer.sortingOrder = defaultLayer + 1;
        }
    }

    public void PreviousLayer()
    {
        if (GetComponentInChildren<Canvas>() != null)
        {
            orderLayer = GetComponentInChildren<Canvas>();
        }
        isFirst = false;
        isDefault = false;
        isPrevious = true;
        spriteRenderer.sortingOrder = defaultLayer + 1;
        if (orderLayer != null)
        {
            orderLayer.sortingOrder = defaultLayer + 2;
        }
        else spriteRenderer.sortingOrder = defaultLayer + 2;
    }

    public void setDestination()
    {
        mouvementDestination = lastPosition;
    }
}
