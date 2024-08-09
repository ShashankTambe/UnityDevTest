
// using UnityEngine;

// public class HologramController : MonoBehaviour
// {
//     public GameObject hologram; // Reference to the hologram GameObject
//     public Transform playerHead; // Reference to the player's head or pivot point
//     public float distanceAboveHead = 2f; // Distance in the Y direction above the player's head to position the hologram

//     void Start()
//     {
//         hologram.SetActive(false); // Start with hologram hidden
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.LeftArrow))
//         {
//             ShowHologram(Vector3.left);
//         }
//         else if (Input.GetKeyDown(KeyCode.RightArrow))
//         {
//             ShowHologram(Vector3.right);
//         }
//         else if (Input.GetKeyDown(KeyCode.UpArrow))
//         {
//             ShowHologram(Vector3.up);
//         }
//         else if (Input.GetKeyDown(KeyCode.DownArrow))
//         {
//             ShowHologram(Vector3.down);
//         }
//     }

//     void ShowHologram(Vector3 direction)
//     {
//         hologram.SetActive(true); // Show the hologram

//         // Calculate the offset position for the hologram so its head aligns with the player's head
//         Vector3 offsetPosition = playerHead.position + new Vector3(0f, distanceAboveHead, 0f);

//         // Position the hologram's head at the player's head
//         hologram.transform.position = offsetPosition;

//         // Determine the player's current forward direction
//         Vector3 playerForward = playerHead.forward;

//         // Create a rotation that aligns the hologram's top with the player's head, and the rest of the hologram oriented based on the direction
//         Quaternion targetRotation = Quaternion.LookRotation(playerForward, direction);

//         // Apply the rotation
//         hologram.transform.rotation = targetRotation;
//     }
// }
using UnityEngine;

public class HologramController : MonoBehaviour
{
    public GameObject hologram; // Reference to the hologram GameObject
    public Transform playerHead; // Reference to the player's head or pivot point
    public float distanceFromHead = 2f; // Distance to position the hologram forward or backward

    void Start()
    {
        hologram.SetActive(false); // Start with hologram hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShowHologram(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShowHologram(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShowHologram(playerHead.forward); // Forward direction relative to player
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShowHologram(-playerHead.forward); // Backward direction relative to player
        }
    }

    void ShowHologram(Vector3 direction)
    {
        hologram.SetActive(true); // Show the hologram

        // Calculate the hologram's position relative to the player's head
        // Offset the hologram in the direction specified
        Vector3 offsetPosition = playerHead.position + direction * distanceFromHead;

        // Position the hologram so its pivot is at the offset position
        hologram.transform.position = offsetPosition;

        // Ensure the hologram faces in the direction of the offset
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the rotation
        hologram.transform.rotation = targetRotation;
    }
}
