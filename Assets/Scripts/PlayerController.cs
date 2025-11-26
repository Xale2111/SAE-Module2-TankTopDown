using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float cannonRotateSpeed = 100f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject tankTower;
    [SerializeField] private GameObject tankBody;
    
    PlayerInput playerInput;
    InputAction moveAction;
    private InputAction lookAction;
    
    Quaternion rotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        lookAction = playerInput.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();

// Déplacement
        Vector3 moveDir = new Vector3(movement.x, 0f, movement.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

// Rotation (si il y a un mouvement)
        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            tankBody.transform.rotation = Quaternion.Slerp(tankBody.transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
        
        float mouseX = Input.GetAxis("Mouse X");
        Vector2 look = moveAction.ReadValue<Vector2>();


        if (mouseX != 0)
        {
            tankTower.transform.Rotate(0f, mouseX * cannonRotateSpeed * Time.deltaTime, 0f);
        }
        else if (look.x != 0)
        {
            tankTower.transform.Rotate(0f, look.x * cannonRotateSpeed * Time.deltaTime, 0f);
        }

    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        
        // Convert the local coordinate values into world
        Gizmos.DrawLine(tankTower.transform.position, tankTower.transform.position + new Vector3(0, 0, 2));
    }
}
