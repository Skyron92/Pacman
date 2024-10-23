using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionReference moveActionReference;
    InputAction MoveAction => moveActionReference.action;

    [SerializeField, Range(0,10)] float speed = 1.0f;
    [SerializeField, Range(5,10)] float rotationSpeed = 10.0f;
    
    CharacterController _characterController;
    
    bool _moveActionStarted;

    void Awake() {
        _characterController = GetComponent<CharacterController>();
        
        MoveAction.started += OnMoveActionStarted;
        MoveAction.canceled += OnMoveActionCanceled;
    }

    void OnMoveActionCanceled(InputAction.CallbackContext obj) {
        _moveActionStarted = false;
    }

    void Update() {
        if(_moveActionStarted) ApplyMove();
    }

    void OnMoveActionStarted(InputAction.CallbackContext obj) { 
        _moveActionStarted = true;
        Debug.Log("Move");
    }

    void ApplyMove() {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        _characterController.Move(movement * Time.deltaTime * speed);

        Rotate(movement);
    }

    void Rotate(Vector3 movement) {
        // On récupère l'angle entre notre direction et l'avant de notre objet
        float angle = Vector3.Angle(movement, -Vector3.forward);
        // On change le signe de l'angle en tenant compte de si on va à droite ou à gauche
        if (movement.x > 0) angle = -angle;
        // Je calcule la rotation qu'il faut appliquer à notre Quaternion pour pivoter où on veut sur l'axe Y
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        // On applique cette rotation à notre transform
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }

    void OnEnable() {
        MoveAction.Enable();
    }

    void OnDisable() {
        MoveAction.Disable();
    }
}
