using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDisplayController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRigidbody;
    float playerSpeed = 0f;
    GameObject speedDisplay;
    TMPro.TextMeshProUGUI speedDisplayText;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Nick");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        speedDisplay = GameObject.Find("SpeedDisplay");        
        speedDisplayText = speedDisplay.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = playerRigidbody.velocity.magnitude;
        speedDisplayText.text = playerSpeed.ToString("F1");        
    }
}
