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

    private GameController gameController;

    [NonSerialized] public bool isDocIn = false;
    [SerializeField] private int docLayer;

    [SerializeField] private Sprite boySprite;
    [SerializeField] private Sprite girlSprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer stickerRenderer;
    [SerializeField] private Sprite[] stickerVersions;

    [NonSerialized] public bool isWriting = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        stickerRenderer.enabled = false;
        spriteRenderer.enabled = false;

        docSpawnpoint = GameObject.FindGameObjectsWithTag("Respawn");
        gameController = FindAnyObjectByType<GameController>();

        gameController.addStud(gameObject.GetComponent<StudentScript>());

        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeInTrigger");
        spriteRenderer.sprite = (gameController.doc[0].isGirl) ? girlSprite : boySprite;
        stickerRenderer.sprite = (gameController.doc[0].isGirl) ? stickerVersions[1] : stickerVersions[0];
    }

    public void SpriteOn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
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
        isWriting = true;
        yield return new WaitForSeconds(3f);
        isWriting = false;
        Instantiate(studTest, docSpawnpoint[UnityEngine.Random.Range(0, docSpawnpoint.Length)].transform);
    }

    public void Àpproved()
    {
        stickerRenderer.enabled = true;
    }
}
