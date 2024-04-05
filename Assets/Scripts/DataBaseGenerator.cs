using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DataBaseGenerator : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private GameObject dataPrefab;
    [SerializeField] private GameController gameController;
    [Space]

    [Header("Chance Settings")]
    private int nameChangeChance = 10;
    private int surnameChangeChance = 90;
    private int idChangeChance = 30;
    private int facultyChangeChance = 75;

    [NonSerialized] public List<GameObject> inputs = new List<GameObject>();

    void Start()
    {
        for (int j = 0; j < gameController.doc.Count; j++)
        {
            Instantiate(dataPrefab, transform);
        }
        TMP_InputField[] text = inputs[0].GetComponentsInChildren<TMP_InputField>();

        int i = 0;

        foreach (documentClass doc in gameController.doc)
        {
            text = inputs[i].GetComponentsInChildren<TMP_InputField>();
            addToDatabase(text, doc);
            i++;
        }
    }

    private void addToDatabase(TMP_InputField[] text, documentClass doc)
    {
        string initials, id, faculty;
        (initials, id, faculty) = doc.getStudentId();

        int chance = UnityEngine.Random.Range(1, 101);
        int armChance = UnityEngine.Random.Range(1, 101);
        int idChance = UnityEngine.Random.Range(1, 101);

        string name, surname;
        name = initials.Split(' ')[0];
        surname = initials.Split(' ')[1];

        if (nameChangeChance >= chance)
        {
            if (armChance >= 50)
            {
                int boyChance = UnityEngine.Random.Range(1, 101);
                if(boyChance >= 50)
                    name = gameController.docGenerator.armBoyNames[UnityEngine.Random.Range(0, gameController.docGenerator.armBoyNames.Length)];
                else
                    name = gameController.docGenerator.armGrlNames[UnityEngine.Random.Range(0, gameController.docGenerator.armGrlNames.Length)];
                initials = name + ' ' + surname;
            }
            else
            {
                int boyChance = UnityEngine.Random.Range(1, 101);
                if(boyChance >= 50)
                    name = gameController.docGenerator.engBoyNames[UnityEngine.Random.Range(0, gameController.docGenerator.engBoyNames.Length)];
                else
                    name = gameController.docGenerator.engGrlNames[UnityEngine.Random.Range(0, gameController.docGenerator.engGrlNames.Length)];
                initials = name + ' ' + surname;
            }
            doc.infoChanged = true;
        }
        if (surnameChangeChance <= chance)
        { 
            if (armChance > 50)
            {
                surname = gameController.docGenerator.armSurnames[UnityEngine.Random.Range(0, gameController.docGenerator.armSurnames.Length)];
                initials = name + ' ' + surname;
            }
            else
            {
                surname = gameController.docGenerator.engSurnames[UnityEngine.Random.Range(0, gameController.docGenerator.engSurnames.Length)];
                initials = name + ' ' + surname;

            }
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
                StringBuilder str = new StringBuilder(id);
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                id = str.ToString();
            }
            else if (idChangeChance <= 75)
            {
                StringBuilder str = new StringBuilder(id);
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                id = str.ToString();
            }
            else
            {
                StringBuilder str = new StringBuilder(id);
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                str[UnityEngine.Random.Range(0, str.Length)] = char.Parse(UnityEngine.Random.Range(0, 9).ToString());
                id = str.ToString();
            }
            doc.infoChanged = true;

        }
        if (facultyChangeChance < chance)
        {
            faculty = gameController.docGenerator.faculty[UnityEngine.Random.Range(0, gameController.docGenerator.faculty.Length)];
            doc.infoChanged = true;
        }
     
        (text[0].text, text[1].text, text[2].text) = (initials, id, faculty);
    }

    public void deleteInput()
    {
        Destroy(inputs[0]);
        inputs.Remove(inputs[0]);
    }
}