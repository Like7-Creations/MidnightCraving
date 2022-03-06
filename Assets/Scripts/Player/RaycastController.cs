using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    Item item;
    [SerializeField] Transform slot;
    [SerializeField] Image inventorySlot;
    Ray ray;
    //[SerializeField] float minDoorDistance;

    private void Start()
    {
        cam = Camera.main;
        item = null;
        inventorySlot.enabled = false;
    }
    private void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        ItemController();
        if (Physics.Raycast(ray,out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "Door")
                {
                    hit.collider.gameObject.GetComponent<DoorController>().PlayAnimation();
                }
            }
        }
    }

    private void ItemController()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (item != null)
            {
                Drop(item);
            }
            else
            {
                if (Physics.Raycast(ray, out hit, 2f))
                {
                    item = hit.transform.GetComponent<Item>();
                    if (item != null)
                        PickUp(item);
                }
            }
        }
    }

    void Drop(Item pickedItem)
    {
        pickedItem = item;
        inventorySlot.enabled = false;
        pickedItem.transform.SetParent(null);
        pickedItem.gameObject.SetActive(true);
        pickedItem.rbd.AddForce(pickedItem.transform.forward *2, ForceMode.VelocityChange);
        item = null;
        inventorySlot.sprite = null;
    }

    void PickUp(Item pickedItem)
    {
        inventorySlot.enabled = true;
        pickedItem = item;
        pickedItem.transform.SetParent(slot);
        inventorySlot.sprite = pickedItem.inventoryImage;
        pickedItem.gameObject.SetActive(false);
    }
}
