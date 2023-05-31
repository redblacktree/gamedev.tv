using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float lowerScreenLimit = 0.33f;
    [SerializeField] float upperScreenLimit = 0.66f;
    [SerializeField] float speedFactor = 0.5f;
    [SerializeField] float centerScreen = 0.5f;
    [SerializeField] float velocityThreshold = 2.0f; // threshold for "slow" velocity
    [SerializeField] float zoomFactor = 0.1f; // how much to zoom out as velocity increases
    [SerializeField] float zoomSpeed = 0.5f; // how fast to zoom in and out
    [SerializeField] float zoomExtent = 20.0f; // how far to zoom out at max velocity
    [SerializeField] float zoomMin = 10.0f; // minimum zoom level

    GameObject trackedObject;
    void Start() {
        trackedObject = GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow.gameObject;
    }
    void Update()
    {
        float objectVelocityY = trackedObject.GetComponent<Rigidbody2D>().velocity.y;

        if (Mathf.Abs(objectVelocityY) < velocityThreshold) {
            // Move towards the center if velocity is slow
            GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY, centerScreen, Time.deltaTime * speedFactor);
        } 
        else if (objectVelocityY < 0) {
            // Move towards the bottom if velocity is negative
            GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY, lowerScreenLimit, -objectVelocityY * Time.deltaTime * speedFactor);
        } 
        else {
            // Move towards the top if velocity is positive
            GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY, upperScreenLimit, objectVelocityY * Time.deltaTime * speedFactor);
        }   

        // zoom out as total velocity increases, zoom back in as it slows down
        float totalVelocity = Mathf.Abs(trackedObject.GetComponent<Rigidbody2D>().velocity.x) + Mathf.Abs(trackedObject.GetComponent<Rigidbody2D>().velocity.y);
        
        // calculate target zoom level based on total velocity and zoom extent
        float targetZoom = zoomMin + Mathf.Min(totalVelocity * zoomFactor, zoomExtent);

        // update camera zoom level
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Lerp(GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.OrthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

}
