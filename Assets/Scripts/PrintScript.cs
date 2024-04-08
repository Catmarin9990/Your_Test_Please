using System.Collections;
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
            StartCoroutine(Destroy(40f));

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            StartCoroutine(Destroy(5f));
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
        Vector2 direction = Vector2.zero;
        Vector2 origin = transform.position; 
        RaycastHit2D hit = Physics2D.Raycast(origin, direction);

        return hit.collider.gameObject;
    }

    private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
