using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListElem : MonoBehaviour
{
    private DataBaseGenerator generator;

    private void Awake()
    {
        generator = GetComponentInParent<DataBaseGenerator>();
        generator.inputs.Add(gameObject);
    }
}
