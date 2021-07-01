using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;
    [SerializeField]
    private float jumpForce;


    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;
    
    // 앉았을 때 얼마나 앉을지 결정하는 변수.
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider;
    [SerializeField]
    private float loolSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX=0;
    
    [SerializeField]
    private Camera theCamera;
    
    private Rigidbody myRigid;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        //초기화 부분
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
    }
    // 앉기 시도
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }
    private void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
       // theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrouchPosY, theCamera.transform.localPosition.z);
       StartCoroutine(CrouchCoroutine());

    }
    IEnumerator CrouchCoroutine()
    {

        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }

    // 지면 체크.
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void Jump()
    {

        // 앉은 상태에서 점프시 앉은 상태 해제.
       if (isCrouch)
          Crouch();

        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running()
    {
       if (isCrouch)
           Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }


    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        //1 -1 0 return
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
       
        myRigid.MovePosition(transform.position + _velocity  *Time.deltaTime);
    }
    private void CameraRotation()
    {
        //상하 카메라 회전
       float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * loolSensitivity;
        currentCameraRotationX -= _cameraRotationX;;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    private void CharacterRotation()
    {
        //좌우 케릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _charaterRotationY = new Vector3(0f, _yRotation, 0f) * loolSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_charaterRotationY));
        Debug.Log(myRigid.rotation);
        Debug.Log(myRigid.rotation.eulerAngles);
    }
}
