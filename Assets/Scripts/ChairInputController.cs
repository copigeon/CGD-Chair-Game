using UnityEngine;

public class ChairInputController : MonoBehaviour
{
    private chairscript chairController;

    [Header("Camera Reference")]
    // main camera/player camera reference from inspector
    [SerializeField] private Transform cameraTransform;

    // chair input actions reference
    private ChairInputActions inputActions;

    // for me - this is the init
    private void Awake()
    {
        // find the chairscript on this GameObject
        chairController = GetComponent<chairscript>();

        // init the input actions asset class
        inputActions = new ChairInputActions();

        // find camera if not assigned - keep forgetting to assign!
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        // turn on when is active
        inputActions.Enable();
    }

    private void OnDisable()
    {
        // turn off when is inactive
        inputActions.Disable();
    }

    private void Update()
    {
        // listen for the push input (from input mapping)
        if (inputActions.ChairCamera.Push.triggered)
        {
            if (chairController != null)
            {
                // Get the camera's forward direction, but flatten it on the Y-axis 
                // so we only push horizontally across the floor (no pushing into or up from the ground)
                // create a holder for the direction "push" direction
                Vector3 pushDirection = Vector3.forward;
                // check camera exists (debug issues)
                if (cameraTransform != null)
                {
                    // get camera forward vector and assign
                    pushDirection = cameraTransform.forward;
                    // remove any tilt (maybe tilt on second stick? for look but not movement? would this need second camera?)
                    pushDirection.y = 0f;
                    // NORMALISE! :D
                    pushDirection.Normalize();
                }
                else
                {
                    // get chairs forward if camera is missing (debug issues)
                    pushDirection = transform.forward;
                }

                // define a force vector using the camera's direction
                Vector3 pushForce = pushDirection * 5f;

                // use current position as the hit point
                Vector3 hitPoint = transform.position;

                // function on chair script
                chairController.PushChair(pushForce, hitPoint);
            }
        }
    }
}