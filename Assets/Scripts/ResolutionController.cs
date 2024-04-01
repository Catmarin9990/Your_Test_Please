using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{

    [SerializeField] private int widthPercent;
    [SerializeField] private int heightPercent;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2((Screen.width / 100) * widthPercent, (Screen.height / 100) * heightPercent);
    }

}
