using System;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Rendering;
using UnityEngine;

public class StampScript : MonoBehaviour
{
    [SerializeField] private Transform defaultTransform;
    [SerializeField] private float speed;

    [SerializeField] private GameObject denySprite;
    [SerializeField] private GameObject agreeSprite;

    [SerializeField] private Transform printSpawnTransform;


    [NonSerialized] public Animator animator;
    [NonSerialized] public bool inAnim = false;

    private bool isMouseUp = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isMouseUp && !inAnim)
            transform.position = Vector2.MoveTowards(transform.position, defaultTransform.position, speed * Time.deltaTime);
        if(transform.position == defaultTransform.position)
            isMouseUp = false;
    }

    public void onStamp()
    {
        animator.SetBool("IsStamping", false);
        inAnim = false;
        
        if (denySprite != null)
            Instantiate(denySprite, printSpawnTransform.position, Quaternion.Euler(0, 0, 0));
        else if(agreeSprite != null)
            Instantiate(agreeSprite, printSpawnTransform.position, Quaternion.Euler(0, 0, 0));
        isMouseUp = true;
    }
}