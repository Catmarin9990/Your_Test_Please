using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSaver : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D coll)
    {
        coll.gameObject.transform.position = new Vector3(0, 0);
    }
}
