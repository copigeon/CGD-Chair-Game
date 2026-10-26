using UnityEngine;

public class ChairInputController : MonoBehaviour
{
    private chairscript chairController;

    private void Awake()
    {
        // find the chairscript on this GameObject
        chairController = GetComponent<chairscript>();
    }

    private void Update()
    {
        // press Space to shove the chair forward
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (chairController != null)
            {
                // define a forward force vector
                Vector3 pushForce = transform.forward * 5f;

                // use current position as the hit point (or offset it slightly)
                Vector3 hitPoint = transform.position;

                // function on chair script
                chairController.PushChair(pushForce, hitPoint);
            }
        }
    }
}