using UnityEditor;
using UnityEngine;

public class ItemSwitch : MonoBehaviour
{
    [Header("Sprite Settings")]
    [SerializeField] private Sprite itemBigVersion;
    [SerializeField] private Sprite itemSmallVersion;
    private SpriteRenderer itemSprite;
    private Canvas text;
    [Space]

    // Gravitation Settings
    private Rigidbody2D rb;
    private float defaltGravity;

    //Collider Settings
    private BoxCollider2D boxColl;
    private CircleCollider2D circleColl;

    [Header("Switch Settings")]
    private bool canSwitchToBig = false;
    private bool canSwitchToSmall = false;
    [SerializeField] private float circleRadius = 1f;
    [SerializeField] private LayerMask small;
    [SerializeField] private LayerMask big;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        defaltGravity = rb.gravityScale;
        itemSprite = GetComponent<SpriteRenderer>();
        
        boxColl = GetComponent<BoxCollider2D>();
        circleColl = GetComponent<CircleCollider2D>();

        text = GetComponentInChildren<Canvas>();

    }

    private void FixedUpdate()
    {
        canSwitchToBig = Physics2D.OverlapCircle(gameObject.transform.position, circleRadius, big);
        canSwitchToSmall = Physics2D.OverlapCircle(gameObject.transform.position, circleRadius, small);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name.ToLower() == "table" && canSwitchToSmall)
        {
            circleColl.enabled = true;
            boxColl.enabled = false;
            if (text != null)
                text.enabled = false;

            rb.gravityScale = defaltGravity;
            itemSprite.sprite = itemSmallVersion;
        }
        if (coll.gameObject.name.ToLower() == "background" && canSwitchToBig && !canSwitchToSmall)
        {
            rb.velocity = new Vector2(0, 0);
            circleColl.enabled = false;
            boxColl.enabled = true;
            if (text != null)
                text.enabled = true;

            rb.gravityScale = 0;
            itemSprite.sprite = itemBigVersion;
        }
    }

}
