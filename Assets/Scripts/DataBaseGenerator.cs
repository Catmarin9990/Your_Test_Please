using System;
using System.Text;
using TMPro;
using UnityEngine;

public class DataBaseGenerator : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private GameObject dataPrefab;
    [SerializeField] private GameController gameController;
    [Space]

    [Header("Chance Settings")]
    [SerializeField] private int nameChangeChance = 5;
    [SerializeField] private int surnameChangeChance = 95;
    [SerializeField] private int idChangeChance = 10;
    [SerializeField] private int facultyChangeChance = 90;

    [NonSerialized] public GameObject[] inputs;

    void Start()
    {
        for (int j = 0; j < gameController.doc.Count; j++)
        {
            Instantiate(dataPrefab, transform);
        }

        inputs = GameObject.FindGameObjectsWithTag("Finish");


        TMP_InputField[] text = inputs[0].GetComponentsInChildren<TMP_InputField>();
        int i = 1;

        foreach (documentClass doc in gameController.doc)
        {
            if (i < inputs.Length)
            {
                addToDatabase(text, doc);
                text = inputs[i].GetComponentsInChildren<TMP_InputField>();
                i++;
            }

        }
    }

    private void addToDatabase(TMP_InputField[] text, documentClass doc)
    {
        string initials, id, faculty;
        (initials, id, faculty) = doc.getStudentId();

        int chance = UnityEngine.Random.Range(1, 101);
        int armChance = UnityEngine.Random.Range(1, 101);
        int idChance = UnityEngine.Random.Range(1, 101);

        if (nameChangeChance >= chance)
        {
            if (armChance >= 50)
                initials.Split(' ')[0] = gameController.docGenerator.armNames[UnityEngine.Random.Range(0, gameController.docGenerator.armNames.Length)];
            else
                initials.Split(' ')[0] = gameController.docGenerator.engNames[UnityEngine.Random.Range(0, gameController.docGenerator.armNames.Length)];
            doc.infoChanged = true;
        }
        if (surnameChangeChance <= chance)
        {
            if (armChance > 50)
                initials.Split(' ')[1] = gameController.docGenerator.armSurnames[UnityEngine.Random.Range(0, gameController.docGenerator.armSurnames.Length)];
            else
                initials.Split(' ')[1] = gameController.docGenerator.engSurnames[UnityEngine.Random.Range(0, gameController.docGenerator.engSurnames.Length)];
            doc.infoChanged = true;

        }
        if (idChangeChance >= chance)
        {
            if (idChance <= 25)
            {
                id = UnityEngine.Random.Range(10000000, 1000000000).ToString();
            }
            else if (idChangeChance <= 50)
            {
                id = (int.Parse(id) + 1).ToString();
            }
            else if (idChangeChance <= 75)
            {
                StringBuilder str = new StringBuilder(id);
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                id = str.ToString();
            }
            else
            {
                StringBuilder str = new StringBuilder(id);
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                id = str.ToString();
            }
            doc.infoChanged = true;

        }
        if (facultyChangeChance < chance)
        {
            faculty = gameController.docGenerator.faculty[UnityEngine.Random.Range(0, faculty.Length)];
            doc.infoChanged = true;
        }
        doc.infoChanged = true;
     
        (text[0].text, text[1].text, text[2].text) = (initials, id, faculty);

    }
}