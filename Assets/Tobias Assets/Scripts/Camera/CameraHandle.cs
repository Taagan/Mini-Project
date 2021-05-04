using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    [SerializeField, Range(5.0f, 30.0f)] private float m_Speed = 10.0f;
    [SerializeField, Range(1.0f,  3.0f)] private float m_BoostSpeed = 2.25f;
    [SerializeField, Range(0.75f, 3.0f)] private float m_Sensitivity = 1.7f;

    private Vector3 cameraRotation = Vector3.zero;
    private float m_DefaultSpeed;

    private void Start()
    {
        m_DefaultSpeed = m_Speed;
        m_BoostSpeed *= m_Speed;
    }

    private void Update()
    {
        // -- MOVEMENT --

        m_Speed = Input.GetKey(KeyCode.LeftShift) ?
            m_BoostSpeed : m_DefaultSpeed;
        
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
            transform.position += transform.up * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl))
            transform.position -= transform.up * m_Speed * Time.deltaTime;

        // -- ROTATION --

        float mouseX = Input.GetAxis("Mouse X") * m_Sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * m_Sensitivity;

        cameraRotation.x -= mouseY; // pitch
        cameraRotation.y += mouseX; // yaw

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
