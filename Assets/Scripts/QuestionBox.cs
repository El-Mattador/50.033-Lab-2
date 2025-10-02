using System.Collections;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;       // coin SFX
    [SerializeField] private LayerMask playerMask;  // layer for Mario

    [SerializeField] private Sprite usedSprite;     // sprite for after box is used
    [SerializeField] private GameObject coinPrefab; // prefab to spawn
    [SerializeField] private Transform coinSpawn;   // child transform above box

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool used = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (used) return;

        // Must be the player (by layer)
        if (((1 << col.collider.gameObject.layer) & playerMask) == 0) return;

        // Accept hit only from below.
        bool fromBelow = false;

        // Primary: contact normal on THIS box tends to be ~Vector2.down when hit on its bottom.
        for (int i = 0; i < col.contactCount; i++)
        {
            var n = col.GetContact(i).normal;
            if (n.y < -0.5f) { fromBelow = true; break; }
        }

        // Fallback: player moving upward, but only if below the box center
        if (!fromBelow && col.relativeVelocity.y > 0.1f)
        {
            if (col.transform.position.y < transform.position.y)
                fromBelow = true;
        }
        

        if (!fromBelow) return;

        // Trigger coin + disable future bounces
        StartCoroutine(TriggerBox());
    }

    private IEnumerator TriggerBox()
    {
        used = true;

        if (sfx) sfx.Play();

        // Change to "used" sprite
        if (usedSprite != null)
            sr.sprite = usedSprite;

        // Spawn coin prefab
        if (coinPrefab != null && coinSpawn != null)
        {
            GameObject coin = Instantiate(coinPrefab, coinSpawn.position, Quaternion.identity);
            Destroy(coin, 1f); // coin disappears after 1s
        }
        
        // After a short moment, disable the joint by making this RB static (as per checkoff tip)
        yield return new WaitForSeconds(0.3f);
        // Disable spring by freezing
        rb.bodyType = RigidbodyType2D.Static;
    }
}
