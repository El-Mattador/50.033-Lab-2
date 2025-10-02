// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(SpringJoint2D))]
// public class BrickMovement : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     private float originalY;   // the rest Y position of the block 
//     public float bounceForce = 5f; // Force applied when hit from below

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         originalY = transform.position.y;  // save starting Y
//     }

//     void FixedUpdate()
//     {
//         // Only prevent the block from going below its original position
//         if (transform.position.y - bounceOffset < originalY)
//         {
//             // Reset position to original Y
//             transform.position = new Vector3(transform.position.x, originalY, transform.position.z);

//             // Stop any downward velocity
//             if (rb != null)
//                 rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(0, rb.linearVelocity.y));
//         }
//     }
// }
