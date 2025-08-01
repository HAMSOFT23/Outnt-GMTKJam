using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class DoorAction : MonoBehaviour
{
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float maxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;

    public void OnUse()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, maxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.isOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, maxUseDistance, UseLayers) &&
            hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.isOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);

            // Corrected position and rotation
            UseText.transform.position = hit.point + hit.normal * 0.01f;
            UseText.transform.rotation = Camera.transform.rotation;
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
