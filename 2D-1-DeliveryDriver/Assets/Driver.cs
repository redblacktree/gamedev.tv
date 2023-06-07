using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    // To clear overrides that may be set in Unity, right click on the variable name and select "Reset"
    // or delete the variable and re-add it from the Inspector.
    float steerSpeed = 150f;
    public static float moveSpeedOnRoad = 18f;
    [SerializeField] public static float regularMoveSpeed = 12f;
    [SerializeField] public static float slowMoveSpeed = 6f;
    [SerializeField] public static float boostMoveSpeed = 24f;
    public float moveSpeed = regularMoveSpeed;

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, steerAmount * steerSpeed * -1 * Time.deltaTime);
        float accelerateAmount = 0; //Input.GetAxis("Vertical");
        // if the a button on the controlller is pressed, set accelerateAmount to 1
        if (Input.GetButton("Jump"))
        {
            accelerateAmount = 1;
        }
        transform.Translate(0, moveSpeed * accelerateAmount * Time.deltaTime, 0);
    }

    public void UpdateMoveSpeed(float newMoveSpeed)
    {
        Debug.Log($"UpdateMoveSpeed to {newMoveSpeed}");
        this.moveSpeed = newMoveSpeed;
    }
}
