using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StudentScript : MonoBehaviour
{

    [Header("Document Spawn Settings")]
    private GameObject[] docSpawnpoint;
    private Transform spawnpoint;
    [SerializeField] private GameObject[] typesOfDoc;
    [NonSerialized] public GameObject studTest;
    [NonSerialized] public bool isWrited = false;
    [Space]

    private TMP_Text dialogueBox;
    [Space]

    [SerializeField] private int docNotSpawnChance = 5;

    // Animation Settings
    private Animator animator;
    private Animation animation;

    private GameController gameController;

    [NonSerialized] public bool isDocIn = false;
    [SerializeField] private int docLayer;

    void Start()
    {
        docSpawnpoint = GameObject.FindGameObjectsWithTag("Respawn");
        gameController = FindAnyObjectByType<GameController>();

        gameController.addStud(gameObject.GetComponent<StudentScript>());

        animation = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeInTrigger");
    }

    public void giveDocs()
    {
        foreach (GameObject doc in typesOfDoc)
        {
            Instantiate(doc, docSpawnpoint[UnityEngine.Random.Range(0, docSpawnpoint.Length)].transform);
        }
    }

    public void leave()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

    public void leaved()
    {
        gameController.remuveStudent();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.layer == docLayer)
        {
            isDocIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == docLayer)
        {
            isDocIn = false;
        }
    }

    public IEnumerator writeTest()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(studTest, docSpawnpoint[UnityEngine.Random.Range(0, docSpawnpoint.Length)].transform);
    }
}
