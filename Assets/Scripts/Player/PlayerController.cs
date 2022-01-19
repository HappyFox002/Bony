using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Stamina))]
[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour
{
    private float ActionCoef = 100f;

    [Space(10)]
    [SerializeField]
    private float Speed = 7.0f;
    [SerializeField]
    private float RunCoefficent = 1.5f;
    [SerializeField]
    private float JumpHeight = 4.0f;
    [SerializeField]
    private float SpeedRotate = 1f;

    [Space(10)]
    [SerializeField]
    private float SpeedRotateCamera = 10f;

    private GameObject MainCameraObject;
    private GameObject CameraOriginObject;
    private const float ScrollCoef = 1000f;
    [Space(10)]
    [SerializeField]
    private float MaxDistance = 2.0f;
    [SerializeField]
    private float MinDistance = 1.0f;
    [SerializeField]
    [Range(0.5f, 3f)]
    private float ScrollSensetive = 1f;

    private CharacterController character;
    private Vector3 moveDirection = Vector3.zero;
    private float Gravity = -9.81f;
    private const float GravityCoef = 3.0f;
    private bool isRun = false;

    void Start()
    {
        CameraOriginObject = transform.Find("CameraOrigin").gameObject;
        MainCameraObject = CameraOriginObject.transform.Find("MainCamera").gameObject;
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        if (Input.GetButtonDown("Cancel"))
            UIController.UI().PauseGame();

        float moveScroll = Input.GetAxis("ScrollWheel");
        float rotateCamera = Input.GetAxis("MouseX");
        float moveInputVer = Input.GetAxis("Vertical");
        float moveInputHor = Input.GetAxis("Horizontal");

        RotateCamera(rotateCamera);
        ScrollCamera(moveScroll);

        if (Input.GetButton("LShift") && (moveInputVer != 0))
        {
            isRun = true;
        }

        if (moveDirection.y < 0 && character.isGrounded) {
            moveDirection.y = 0f;
        }

        transform.Rotate(moveInputHor * Vector3.up * SpeedRotate * ActionCoef * Time.deltaTime);
        if (character.isGrounded) {
            float moveSpeed = ((isRun) ? Speed * RunCoefficent : Speed);
            moveDirection = transform.TransformDirection(Vector3.forward * moveInputVer) * moveSpeed;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y += Mathf.Sqrt(JumpHeight * -GravityCoef * Gravity);
            }
        }

        moveDirection.y += Gravity * Time.deltaTime * GravityCoef;

        character.Move(moveDirection * Time.deltaTime);
        isRun = false;
    }

    void ScrollCamera(float moveScroll)
    {
        if (MainCameraObject && (MainCameraObject.transform.localPosition.y >= MinDistance && moveScroll > 0)
            || (MainCameraObject.transform.localPosition.y <= MaxDistance && moveScroll < 0))
            MainCameraObject.transform.Translate(Vector3.forward * moveScroll * (ScrollSensetive * ScrollCoef) * Time.deltaTime);
    }

    void RotateCamera(float rotateCamera) {
        if (Input.GetButton("Wheel")) {
            CameraOriginObject.transform.Rotate(Vector3.up * SpeedRotateCamera * ActionCoef * rotateCamera * Time.deltaTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        //Debug.Log(hit.gameObject.name);
    }
}