using UnityEngine;
using System;


public class ItemScript : MonoBehaviour
{
    public enum typeOfDoc
    {
        studentId,
        diploma,
        test
    }

    private GameController gameController;
    public typeOfDoc type;
    [NonSerialized] public bool suitable;

    private Animator animator;

    private void Awake()
    {
        gameController = FindAnyObjectByType<GameController>();
        gameController.addDocument(gameObject);

        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("FadeInTrigger");
        }
    }

    private void Start()
    {
        if(GetComponentInChildren<Canvas>() != null)
        gameController.disable(GetComponentInChildren<Canvas>());
    }

    public void animationStopper()
    {
        transform.position = transform.position;
        animator.enabled = false;
    }
}
