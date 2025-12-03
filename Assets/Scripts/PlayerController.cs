using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Space(10)]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    
    [Header("Shoot")]
    [Space(10)]
    [SerializeField] private float shootSpeed = 1f;
    [Header("Bullet")]
    [Space(5)]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    
    [Header("Rotation of the cannon")]
    [Space(10)]
    [SerializeField] private float cannonRotateSpeed = 100f;
    [SerializeField] float mouseScale = 0.03f;   // réduit la souris
    [SerializeField] float stickScale = 3f;
    
    [Header("Tank parts")]
    [Space(10)]
    [SerializeField] private GameObject tankTower;
    [SerializeField] private GameObject tankBody;
    
  
    
    private float horizontalAngle = 0f;
    private float verticalAngle = 0f;

    private float shootingDelay;

    
    private Vector2 moveInput;
    private Vector2 lookInput;

    void Update()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            tankBody.transform.rotation = Quaternion.Slerp(
                tankBody.transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        

        tankTower.transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);

        shootingDelay += Time.deltaTime;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        float scale = context.control.device is Mouse ? mouseScale : stickScale;

        Vector2 look = input * scale;

        horizontalAngle += look.x * cannonRotateSpeed * Time.deltaTime;
        verticalAngle   += -look.y * cannonRotateSpeed * Time.deltaTime;

        verticalAngle = Mathf.Clamp(verticalAngle, -15f, 7.5f);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && shootingDelay >= shootSpeed)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            shootingDelay = 0f;
        }
    }
}
