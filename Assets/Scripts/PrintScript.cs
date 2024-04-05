using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class PrintScript : MonoBehaviour
{
    private GameObject perent;
    private SpriteRenderer sprite;
    private SpriteRenderer gameObjectSprite;
    [SerializeField] bool isAgree;

    void Start()
    {
        perent = CreateRay();
        transform.SetParent(perent.transform);
        if (perent.GetComponent<SpriteRenderer>() != null)
        {
            sprite = perent.GetComponent<SpriteRenderer>();
            gameObjectSprite = gameObject.GetComponent<SpriteRenderer>();
            gameObjectSprite.sortingOrder = sprite.sortingOrder;
            GameController gameController = FindAnyObjectByType<GameController>();
            gameController.playerChoice = isAgree;
            gameController.isPlayerChoosed = true;
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void Update()
    {
        if(sprite != null)
        {
            gameObjectSprite.sortingOrder = sprite.sortingOrder + 1;
        }
    }

    private GameObject CreateRay()
    {
        Vector2 direction = transform.right;
        Vector2 origin = transform.position; 
        float distance = 1f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);

        return hit.collider.gameObject;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
