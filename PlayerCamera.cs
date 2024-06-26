using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


public class PlayerCamera : MonoBehaviour
{
    public Transform Player;
    public Transform MainCamera;
    public Transform ActualCamera;
    public float MouseSensitivity = 500f;
    float CameraVerticalRotation = 0f;
    float CameraHorizontalRotation = 0f;

    float MouseX;
    float MouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mx = MouseX * Time.fixedDeltaTime * MouseSensitivity;
        float my = MouseY * Time.fixedDeltaTime * MouseSensitivity;

        CameraVerticalRotation -= my;
        CameraVerticalRotation = Mathf.Clamp(CameraVerticalRotation, -90f, 90f);
        CameraHorizontalRotation += mx;
        MainCamera.transform.rotation = Quaternion.Euler(CameraVerticalRotation, CameraHorizontalRotation, 0f);
        Player.transform.rotation = Quaternion.Euler(0f, CameraHorizontalRotation, 0f);

        ActualCamera.position = Vector3.Lerp(ActualCamera.position, MainCamera.transform.position, 0.025f);
        ActualCamera.rotation = Quaternion.Lerp(ActualCamera.rotation, MainCamera.transform.rotation, 0.5f); 
    }

    public void UpdateMouseX(InputAction.CallbackContext context)
    {
        MouseX = context.ReadValue<float>();
    }

    public void UpdateMouseY(InputAction.CallbackContext context)
    {
        MouseY = context.ReadValue<float>();
    }
}
