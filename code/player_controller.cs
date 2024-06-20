using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player_ : MonoBehaviour
{
    [SerializeField] //�ν����� â���� �����ϱ� ����
    private float walkSpeed; //�ν����Ϳ��� �Ⱥ��̵��� �ϴµ�

    [SerializeField] //�ν����� â���� �����ϱ� ����
    private float jumpForce; //���� ����

    private bool isGround = true; //���� �ִ���
    public bool isPlayerDied = false;
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private float LookSensabilty; //ī�޶��� �ΰ���

    [SerializeField]
    private float CameraRotationLimit; //���� �鶧�� �Ѱ�
    private float currentCameraRotationX = 0; //������ ���� ����

    [SerializeField]
    private Camera theCamera; //ī�޶� ������Ʈ

    private BoxCollider boxcollider;
    private Rigidbody playerRigidbody; //�÷��̾��� �� (�浹�ϱ� ����)
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() //�Լ��� �̸�
    {
        IsGround();
        TryJump();
        Move();
        CameraRotation();
        CharacterRotation();
        if (isPlayerDied)
            return;
    }

    private void Move() //������
    {

        float _moveDirX = Input.GetAxisRaw("Horizontal"); //Ű���� ����Ű �Է�
        float _moveDirZ = Input.GetAxisRaw("Vertical"); //����Ƽ���� ���� �¿� �����̱� ���ؼ� X,Z ���
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed; //�����¿츦 ���ÿ� �����̱� �����ϵ���

        playerRigidbody.MovePosition(transform.position + _velocity * Time.deltaTime); //�����ϵ��� �����̵� ���ϵ��� �ϱ� ���� Ÿ�Ӱ� �Է�

    }

    private void IsGround() //���� �ִ��� Ȯ��
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump() //���� ���
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump() //���� �ӵ� �� ����
    {
        playerRigidbody.velocity = transform.up * jumpForce;
    }

    private void CharacterRotation() //�¿� ĳ���� ȸ��
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * LookSensabilty;

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() //���� ī�޶� ȸ��
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * LookSensabilty;
        currentCameraRotationX -= _cameraRotationX; //���콺 ���� ������ +�� �ƴ� -
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