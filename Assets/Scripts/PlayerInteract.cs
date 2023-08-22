using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    private PlayerUI playerUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput;

    private RaycastHit hit;

    [SerializeField]
    private AudioSource pickUpSource;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void PickUp(InputAction.CallbackContext obj)
    {
        if (hit.collider != null && inHandItem == null)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Item>())
            {
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }
        }
    }

     // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<HighlightObject>()?.ToggleHighlight(false);
            
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit,
            hitRange,
            pickableLayerMask))
        {
            hit.collider.GetComponent<HighlightObject>()?.ToggleHighlight(true);
            playerUI.UpdateText(hit.collider.GetComponent<Item>().promptMessage);

        }
    }
}
