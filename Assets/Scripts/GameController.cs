using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static documentClass;

public class GameController : MonoBehaviour
{
    // lists
    [NonSerialized] public List<GameObject> documents = new List<GameObject>();
    [NonSerialized] public List<Canvas> canvasList = new List<Canvas>();
    [NonSerialized] public List<documentClass> doc = new List<documentClass>();

    [Header("Students Settings")]
    [NonSerialized] public int studentsList = 0;
    [SerializeField] private GameObject studentPrefab;
    [NonSerialized] public StudentScript student;
    [SerializeField] private Transform studentSpawnpoint;
    [Space]

    [Header("Tests Settings")]
    [SerializeField] private GameObject blankTestPrefab;
    [NonSerialized] public bool canSpawn = true;
    private GameObject correctTest;
    [NonSerialized] public bool isTestCorrect;

    [NonSerialized] public DocRandomGeneration docGenerator;

    [NonSerialized] public DataBaseGenerator database;

    [SerializeField] private TMP_Text dialogueBox;

    [NonSerialized] public bool playerChoice;
    [NonSerialized] public bool isPlayerChoosed = false;

    // Answer counts
    private int correctCount = 0;
    private int wrongCount = 0;

    private void Awake()
    {
        docGenerator = GetComponent<DocRandomGeneration>();
        studentsList = UnityEngine.Random.Range(10, 16);
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
        database = GameObject.FindAnyObjectByType<DataBaseGenerator>();
        callStudnet();
    }

    public void callStudnet()
    {
        if (studentsList != 0)
        {
            Instantiate(studentPrefab, studentSpawnpoint);
        }
        else
        {
            typeSentance.Add(TypeSentance("Wrong answers " + wrongCount.ToString() + "\nCorrect answers " + correctCount.ToString()));
        }
        studentsList--;
    }

    public void addStud(StudentScript gameObject)
    {
        student = gameObject;
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

    private void fillDocuments(Canvas canvas, ItemScript itemScript)
    {
        TMP_Text[] data = canvas.GetComponentsInChildren<TMP_Text>();

        if (doc[0] != null)
        {
            switch ((typeOfDoc)itemScript.type)
            {
                case typeOfDoc.studentId:
                    (data[0].text, data[1].text, data[2].text) = doc[0].getStudentId();
                    break;
                case typeOfDoc.diploma:
                    
                    string[] grades = null;
                    (data[0].text, data[1].text, data[2].text, grades) = doc[0].getDiploma();
                    for (int i = 3; i < data.Length; i++)
                    {
                        data[i].text = grades[i - 3];
                    }
                    break;
            }
        }
        itemScript.suitable = doc[0].suitable;
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
        if (documents.Count == 0)
        {
            student.leave();
        }
    }

    public void remuveStudent()
    {
        student.GetComponent<SpriteRenderer>().enabled = false;
        checkEverythink(doc[0]);
        database.deleteInput();
        student.enabled = false;
        Destroy(student.gameObject);
        student = null;
        callStudnet();
    }

    public void giveBlankTest()
    {
        Instantiate(blankTestPrefab);
    }

    public void getTests()
    {
        if (!student.isWrited)
        {
            student.isWrited = true;
            StartCoroutine(student.writeTest());
            docGenerator.getTests(ref correctTest, ref student.studTest);
            Instantiate(correctTest);
        }
    }
    private List<IEnumerator> typeSentance = new List<IEnumerator>();
    public void checkEverythink(documentClass document)
    {
        dialogueBox.text = "";
        if (!isPlayerChoosed)
        {
            typeSentance.Add(TypeSentance("You didn't checked \n"));
            StartCoroutine(MainCoroutine());
            wrongCount++;
        }
        else if (playerChoice)
        {
            if (!document.suitable)
            {
                typeSentance.Add(TypeSentance("grades not enough \n"));
                wrongCount++;
            }

            if (document.infoChanged)
            {
                string initials, id, faculty, name, surname;
                TMP_InputField[] text = database.inputs[0].GetComponentsInChildren<TMP_InputField>();
                initials = text[0].text;
                id = text[1].text;
                faculty = text[2].text;
                name = initials.Split(' ')[0];
                surname = initials.Split(' ')[1];

                string studInitials, studIdName, studIdsurname, studId, studFaculty;
                (studInitials, studId, studFaculty) = document.getStudentId();
                studIdName = studInitials.Split(' ')[0];
                studIdsurname = studInitials.Split(' ')[1];

                if (studIdName.ToLower() != name.ToLower())
                {
                    typeSentance.Add(TypeSentance("Name was wrong \n"));
                    wrongCount++;
                }
                    
                if (studIdsurname.ToLower() != surname.ToLower())
                {
                    typeSentance.Add(TypeSentance("Surname was wrong \n"));
                    wrongCount++;
                }
                    
                if (studId != id)
                {
                    typeSentance.Add(TypeSentance("Id was wrong \n"));
                    wrongCount++;
                }
                    
                if (studFaculty.ToLower() != faculty.ToLower())
                {
                    typeSentance.Add(TypeSentance("Faculty was wrong \n"));
                    wrongCount++;
                }
                    
            }
            if (!isTestCorrect && student.isWrited)
            {
                typeSentance.Add(TypeSentance("test was wrong \n"));
                wrongCount++;
            }
            else if(!student.isWrited)
            {
                typeSentance.Add(TypeSentance("Student didnt write the test \n"));
                wrongCount++;
            }
            StartCoroutine(MainCoroutine());
        }
        else if(!playerChoice)
        {
            bool isWrong = true;
            if (student.isWrited && isTestCorrect && document.suitable || document.suitable)
            {
                typeSentance.Clear();
                typeSentance.Add(TypeSentance("Everything was correct \n"));
                isWrong = false;
                wrongCount++;
            }
            if (!isWrong)
            {
                StartCoroutine(MainCoroutine());
            }
        }
        if (typeSentance.Count == 0) correctCount++;
        isPlayerChoosed = false;
        doc.Remove(doc[0]);
        canSpawn = true;
    }
    private IEnumerator TypeSentance(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator MainCoroutine()
    {
        foreach(IEnumerator IE in typeSentance)
        {
            yield return StartCoroutine(IE);
        }
        typeSentance.Clear();
    }
}