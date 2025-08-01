using UnityEngine;
using System.Collections;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportZone;
    
    private static bool canTeleport = true;
    [SerializeField] private float tPCoolDown = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        // Only teleport if the player is allowed to and has the correct tag
        if (other.CompareTag("Player") && canTeleport)
        {
            // Start the teleport and cooldown process
            StartCoroutine(Teleport(other));
        }
    }

    private IEnumerator Teleport(Collider player)
    {
        // Prevent any teleporter from running again immediately
        canTeleport = false;

        //Teleport Logic
        Vector3 localOffset = transform.InverseTransformPoint(player.transform.position);
        Quaternion relativeRotation = teleportZone.rotation * Quaternion.Inverse(transform.rotation);
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = teleportZone.TransformPoint(localOffset);
            player.transform.rotation = relativeRotation * player.transform.rotation;
            cc.enabled = true;
        }

        // Wait for a short duration before allowing teleporting again
        yield return new WaitForSeconds(tPCoolDown);
        canTeleport = true;
    }
}