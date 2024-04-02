using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DataBaseGenerator : MonoBehaviour
{
    [SerializeField] private GameObject dataPrefab;
    [SerializeField] private GameController gameController;

    private GameObject[] inputs;

    [NonSerialized] public bool changed;

    void Start()
    {
        for (int j = 0; j < gameController.doc.Count; j++)
        {
            Instantiate(dataPrefab, transform);
        }

        inputs = GameObject.FindGameObjectsWithTag("Finish");


        TMP_Text[] text = inputs[0].GetComponentsInChildren<TMP_Text>();
        int i = 1;

        foreach (documentClass doc in gameController.doc)
        {
            (text[0].text, text[1].text, text[2].text) = doc.getStudentId(); 
            if(i < inputs.Length)
            text = inputs[i].GetComponentsInChildren<TMP_Text>();
            i++;
        }
    }


}