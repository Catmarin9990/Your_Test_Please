using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StudentScript : MonoBehaviour
{

    [Header("Document Spawn Settings")]
    private GameObject[] docSpawnpoint;
    [SerializeField] private GameObject[] typesOfDoc;
    [NonSerialized] public GameObject studTest;
    [Space]

    private TMP_Text dialogueBox;
    [Space]

    [SerializeField] private int docNotSpawnChance = 5;

    // Animation Settings
    private Animator animator;

    private GameController gameController;

    [NonSerialized] public bool isDocIn = false;
    [SerializeField] private int docLayer;

    void Start()
    {
        docSpawnpoint = GameObject.FindGameObjectsWithTag("Respawn");
        gameController = FindAnyObjectByType<GameController>();

        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeInTrigger");
    }

    public void giveDocs()
    {
        int chance = 0;
        UnityEngine.Random.Range(0, gameController.documents.Count);
        foreach (GameObject doc in typesOfDoc)
        {
            chance = UnityEngine.Random.Range(0, 100);
            if (chance > docNotSpawnChance)
            {
                Instantiate(doc, docSpawnpoint[UnityEngine.Random.Range(0, docSpawnpoint.Length)].transform);
            }
        }
    }

    public void leave()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.layer == docLayer)
        isDocIn = true;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == docLayer)
            isDocIn = false;
    }

    public IEnumerator writeTest()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(studTest, docSpawnpoint[UnityEngine.Random.Range(0, docSpawnpoint.Length)].transform);
    }
}
