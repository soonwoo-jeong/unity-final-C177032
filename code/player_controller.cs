using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player_ : MonoBehaviour
{
    [SerializeField] //인스펙터 창에서 수정하기 위함
    private float walkSpeed; //인스펙터에서 안보이도록 하는데

    [SerializeField] //인스펙터 창에서 수정하기 위함
    private float jumpForce; //점프 높이

    private bool isGround = true; //땅에 있는지
    public bool isPlayerDied = false;
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private float LookSensabilty; //카메라의 민감도

    [SerializeField]
    private float CameraRotationLimit; //고개를 들때의 한계
    private float currentCameraRotationX = 0; //정면을 보기 위함

    [SerializeField]
    private Camera theCamera; //카메라 컴포넌트

    private BoxCollider boxcollider;
    private Rigidbody playerRigidbody; //플레이어의 몸 (충돌하기 위함)
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() //함수들 이름
    {
        IsGround();
        TryJump();
        Move();
        CameraRotation();
        CharacterRotation();
        if (isPlayerDied)
            return;
    }

    private void Move() //움직임
    {

        float _moveDirX = Input.GetAxisRaw("Horizontal"); //키보드 방향키 입력
        float _moveDirZ = Input.GetAxisRaw("Vertical"); //유니티에서 상하 좌우 움직이기 위해서 X,Z 사용
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed; //상하좌우를 동시에 움직이기 가능하도록

        playerRigidbody.MovePosition(transform.position + _velocity * Time.deltaTime); //움직일동안 순간이동 안하도록 하기 위해 타임값 입력

    }

    private void IsGround() //땅에 있는지 확인
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump() //점프 방법
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump() //점프 속도 및 높이
    {
        playerRigidbody.velocity = transform.up * jumpForce;
    }

    private void CharacterRotation() //좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * LookSensabilty;

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() //상하 카메라 회전
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * LookSensabilty;
        currentCameraRotationX -= _cameraRotationX; //마우스 반전 없도록 +가 아닌 -
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

    public void OnTriggerEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDied = true;
            SceneManager.LoadScene("exit");
        }
    }
}