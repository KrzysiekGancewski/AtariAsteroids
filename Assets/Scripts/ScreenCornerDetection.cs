using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScreenCornerDetection : MonoBehaviour {
    
    private const float deadZone = 0.1f;
    private Vector2 bottomLeftScreenCorner;
    private Vector2 topRightScreenCorner;
    private float halfOfTheSpriteWidth;
    private float halfOfTheSpriteHeight;

    private void Start() {
        bottomLeftScreenCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        topRightScreenCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        SpriteRenderer objectSpriteRenderer = GetComponent<SpriteRenderer>();
        halfOfTheSpriteWidth = objectSpriteRenderer.size.x / 2;
        halfOfTheSpriteHeight = objectSpriteRenderer.size.y / 2;
    }

    private void Update() {
        CheckScreenEdges();
    }

    private void CheckScreenEdges() {
        if (transform.position.x <= bottomLeftScreenCorner.x - halfOfTheSpriteWidth - deadZone) {
            transform.position = new Vector2(topRightScreenCorner.x + halfOfTheSpriteWidth, transform.position.y);
        } else if (transform.position.x >= topRightScreenCorner.x + halfOfTheSpriteWidth + deadZone) {
            transform.position = new Vector2(bottomLeftScreenCorner.x - halfOfTheSpriteWidth, transform.position.y);
        } else if (transform.position.y <= bottomLeftScreenCorner.y - halfOfTheSpriteHeight - deadZone) {
            transform.position = new Vector2(transform.position.x, topRightScreenCorner.y + halfOfTheSpriteHeight);
        } else if (transform.position.y >= topRightScreenCorner.y + halfOfTheSpriteHeight + deadZone) {
            transform.position = new Vector2(transform.position.x, bottomLeftScreenCorner.y - halfOfTheSpriteHeight);
        }
    }

}
