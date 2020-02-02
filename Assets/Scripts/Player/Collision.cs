using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]

    public LayerMask _interactionLayer;

    [Space]

    public bool onUp;
    public bool onDown;
    public bool onLeft;
    public bool onRight;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, upOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onDown = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, _interactionLayer);
        onUp = Physics2D.OverlapCircle((Vector2)transform.position + upOffset, collisionRadius, _interactionLayer);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, _interactionLayer);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, _interactionLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, upOffset, rightOffset, leftOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + upOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
