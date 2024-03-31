using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static documentClass;

public class GameController : MonoBehaviour
{

    [NonSerialized] public List<GameObject> documents = new List<GameObject>();
    [NonSerialized] public List<Canvas> canvasList = new List<Canvas>();
    [NonSerialized] public List<documentClass> doc = new List<documentClass>();

    [Header("Students Settings")]
    private int studentsCount = 0;
    private int studentsList = 0;
    [SerializeField] private GameObject studentPrefab;
    [NonSerialized] public StudentScript student;
    [Space]

    [Header("Tests Settings")]
    [SerializeField] private GameObject blankTestPrefab;
    private bool canSpawn = true;
    private GameObject correctTest;

    [NonSerialized] public DocRandomGeneration docGenerator;

    private void Awake()
    {
        docGenerator = GetComponent<DocRandomGeneration>();
        studentsCount = UnityEngine.Random.Range(15, 26);
        studentsList = UnityEngine.Random.Range(20, 26);
        documentClass papers;

        for (int i = 0; i < studentsList; i++)
        {
            papers = new documentClass();

            papers.createDoc(docGenerator);

            doc.Add(papers);
        }
    }

    private void Start()
    {
        if (studentsList != 0)
        {
            Instantiate(studentPrefab);
            student = FindAnyObjectByType<StudentScript>();
        }
        studentsList--;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && canSpawn)
        {
            canSpawn = false;
            giveBlankTest();
        }
    }

    public void addDocument(GameObject document)
    {
        documents.Add(document);
        if(document.GetComponentInChildren<Canvas>() != null)
        {
            Canvas canvas = document.GetComponentInChildren<Canvas>();
            canvas.worldCamera = Camera.main;
            canvasList.Add(canvas);
            if (document.GetComponent<ItemScript>() != null)
                fillDocuments(canvas, document.GetComponent<ItemScript>());
        }
        
    }
    
    private int docSpawned = 0;

    private void fillDocuments(Canvas canvas, ItemScript itemScript)
    {
        TMP_Text[] data = canvas.GetComponentsInChildren<TMP_Text>();
        int chance = UnityEngine.Random.Range(0, 100);

        

        if (doc[0] != null)
        {
            switch ((typeOfDoc)itemScript.type)
            {
                case typeOfDoc.studentId:
                    (data[0].text, data[1].text, data[2].text) = doc[0].getStudentId();
                    docSpawned++;
                    break;
                case typeOfDoc.diploma:
                    
                    string[] grades = null;
                    (data[0].text, data[1].text, data[2].text, grades) = doc[0].getDiploma();
                    for (int i = 3; i < data.Length; i++)
                    {
                        data[i].text = grades[i - 3];
                    }
                    docSpawned++;
                    break;
            }
        }
        itemScript.suitable = doc[0].suitable;
        if(docSpawned == 2)
        {
            doc.Remove(doc[0]);
            docSpawned = 0;
        }
    }

    public void disable(Canvas c)
    {
        c.enabled = false;
    }

    public void removeDocument(GameObject document)
    {
        documents.Remove(document);
        document.GetComponent<ItemScript>().enabled = false;
        document.GetComponent<DragAndDrop>().enabled = false;
        document.GetComponent<ItemSwitch>().enabled = false;
        Destroy(document);
    }

    private void giveBlankTest()
    {
        Instantiate(blankTestPrefab);
    }

    public void getTests()
    {
        StartCoroutine(student.writeTest());
        docGenerator.getTests(ref correctTest, ref student.studTest);
        Instantiate(correctTest);
    }
}