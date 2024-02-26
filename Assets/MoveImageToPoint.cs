using UnityEngine;
using UnityEngine.UI;

public class MoveImageToPoint : MonoBehaviour
{
    public Image imageToMove;
    public GameObject targetObject;
    public float moveSpeed = 5f;

    private RectTransform canvasRect;

    private void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (targetObject != null)
        {
            // Get the forward vector of the target object and calculate the target position
            Vector3 forwardVector = targetObject.transform.right;
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position + forwardVector);

            MoveImage(targetPosition);
        }
    }

    private void MoveImage(Vector3 targetPosition)
    {
        Vector2 moveAmount = (targetPosition - imageToMove.rectTransform.position) * moveSpeed * Time.deltaTime;
        imageToMove.rectTransform.Translate(moveAmount);

        // Clamp the image position within the canvas boundaries
        Vector3 clampedPosition = imageToMove.rectTransform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0, canvasRect.rect.width);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0, canvasRect.rect.height);
        imageToMove.rectTransform.position = clampedPosition;
    }
}
