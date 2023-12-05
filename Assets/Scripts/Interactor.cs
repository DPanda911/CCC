using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float range;
    public bool canInteract = true;

    private Image crossImg;
    public Sprite default_crosshair;
    public Sprite notable_crosshair;

    // Start is called before the first frame update
    void Start()
    {
        crossImg = GameObject.Find("Crosshair").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitinfo, range) && canInteract) {
            if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                crossImg.sprite = notable_crosshair;
                if (Input.GetKeyDown(KeyCode.E) && !Input.GetButton("Fire1")) {
                    interactObj.Interact();
                }
            } else {
                crossImg.sprite = default_crosshair;
            }
        } else {
            crossImg.sprite = default_crosshair;
        }

        if (canInteract) {
            crossImg.color = new Color(1f, 1f, 1f, 1f);
        } else {
            crossImg.color = new Color(1f, 1f, 1f, 0.33f);
        }
        /*
        if (Input.GetKeyDown(KeyCode.E) && !Input.GetButton("Fire1") && canInteract) {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitinfo, range)) {
                if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    interactObj.Interact();
                }
            }
        }
        */
    }
}
