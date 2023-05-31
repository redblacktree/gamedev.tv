using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage = false;
    [SerializeField] Color32 hasPackageColor = new Color32(0x90, 0xE8, 0xEC, 0xFF);
    [SerializeField] Color32 noPackageColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    //string hasPackageColor = "#90E8EC"

    // 2d oncollision
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Bump") {
            Debug.Log("Slow!");
            GetComponent<Driver>().UpdateMoveSpeed(Driver.slowMoveSpeed);
        }
    }

    // 2d trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Package" && !hasPackage) {
            Debug.Log("Package picked up");
            hasPackage = true;
            GetComponent<SpriteRenderer>().color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }
        else if (other.tag == "Customer" && hasPackage) {
            Debug.Log("Delivered!");
            hasPackage = false;
            GetComponent<SpriteRenderer>().color = noPackageColor;
        }
        else if (other.tag == "Boost") {
            Debug.Log("Boost!");
            GetComponent<Driver>().UpdateMoveSpeed(Driver.boostMoveSpeed);
        }
        else if (other.tag == "Road" && GetComponent<Driver>().moveSpeed == Driver.slowMoveSpeed) {
            Debug.Log("Road!");
            GetComponent<Driver>().UpdateMoveSpeed(Driver.moveSpeedOnRoad);
        }
    }

}
