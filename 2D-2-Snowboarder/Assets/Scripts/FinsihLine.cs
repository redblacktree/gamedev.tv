using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinsihLine : MonoBehaviour
{    // on player enter finish line, end run
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            GameObject.Find("Timer").GetComponent<TimerController>().Stop();
            other.gameObject.GetComponent<PlayerController>().EndRun();        
        }
    }
}
