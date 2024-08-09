
// BELOW CODE IS WORKING AS EXPECTED
// using UnityEngine;

// public class GravityChanger : MonoBehaviour
// {
//     // Define gravity directions
//     private Vector3[] gravityDirections = {
//         Vector3.right,   // +X (0 on number pad)
//         Vector3.left,    // -X (1 on number pad)
//         Vector3.forward, // +Z (4 on number pad)
//         Vector3.back     // -Z (5 on number pad)
//     };

//     void Update()
//     {
//         // Change gravity direction based on number pad input
//         if (Input.GetKeyDown(KeyCode.Keypad0))
//         {
//             SetGravity(0); // +X
//         }
//         else if (Input.GetKeyDown(KeyCode.Keypad1))
//         {
//             SetGravity(1); // -X
//         }
//         else if (Input.GetKeyDown(KeyCode.Keypad4))
//         {
//             SetGravity(4); // +Z
//         }
//         else if (Input.GetKeyDown(KeyCode.Keypad5))
//         {
//             SetGravity(5); // -Z
//         }
//     }

//     void SetGravity(int index)
//     {
//         // Ensure index is within the bounds of gravityDirections
//         if (index >= 0 && index < gravityDirections.Length)
//         {
//             Physics.gravity = gravityDirections[index] * -9.81f; // Set gravity direction
//             Debug.Log("Gravity set to: " + Physics.gravity);
//         }
//     }
// }


//BELOW CODE IS FOR ALIGHNING THE PLAYER OPPOSITE TO THE GRAVITY APPLIED
using UnityEngine;
using System.Collections;

public class GravityManipulator : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player’s transform
    public float alignSpeed = 2.0f; // Speed of alignment

    // Define gravity directions
    private Vector3[] gravityDirections = {
        Vector3.right,   // +X (0 on number pad)
        Vector3.left,    // -X (1 on number pad)
        Vector3.forward, // +Z (4 on number pad)
        Vector3.back     // -Z (5 on number pad)
    };

    private int currentGravityIndex = 0; // Default to +X

    void Update()
    {
        // Change gravity direction based on number pad input
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ChangeGravity(0); // +X
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeGravity(1); // -X
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeGravity(2); // +Z
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ChangeGravity(3); // -Z
        }
    }

    void ChangeGravity(int index)
    {
        // Update the current gravity index
        if (index >= 0 && index < gravityDirections.Length)
        {
            currentGravityIndex = index;
            Physics.gravity = gravityDirections[currentGravityIndex] * -9.81f; // Set gravity direction
            Debug.Log("Gravity set to: " + Physics.gravity);
            
            // Align player with new gravity direction
            StartCoroutine(AlignPlayerToGravity(gravityDirections[currentGravityIndex]));
        }
    }

    IEnumerator AlignPlayerToGravity(Vector3 gravityDirection)
    {
        Vector3 targetUp = -gravityDirection; // Player's up direction should align opposite to gravity
        Quaternion targetRotation = Quaternion.FromToRotation(playerTransform.up, targetUp) * playerTransform.rotation;

        while (Quaternion.Angle(playerTransform.rotation, targetRotation) > 0.1f)
        {
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, alignSpeed * Time.deltaTime);
            yield return null;
        }

        // Final alignment to ensure it's exactly correct
        playerTransform.rotation = targetRotation;
    }
}
