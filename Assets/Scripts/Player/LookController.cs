using UnityEngine;
using UnityEngine.InputSystem;

public class LookController : MonoBehaviour
{
    public float sensitivity = 50f;
    [SerializeField] private Transform bodyTransform;
    private Vector2 lookVector;
    private float xRotation = 0;

    GameSettings settings;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        settings = GameManager.instance.GameSettings;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        float lookX = lookVector.x * settings.MouseSensitivity * Time.deltaTime;
        float lookY = lookVector.y * settings.MouseSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        bodyTransform.Rotate(Vector3.up * lookX);
    }

}
