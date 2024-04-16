using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spon : MonoBehaviour
{
    [HideInInspector]
    public bool activeSpon = false;
    private Vector2 offset;
    private Rigidbody2D rb;
    public LayerMask obstacleLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !activeSpon)
        {
            ActivateSpon();
        }
    }

    public void OnMouseUp()
    {
        if (activeSpon)
        {
            DeactivateSpon();
        }
    }

    private void FixedUpdate()
    {
        if (activeSpon)
        {
            Vector2 newPositin = GetMouseWorldPosition() + offset; ;
            rb.MovePosition(newPositin);
        }
    }

    private void ActivateSpon()
    {
        Cursor.visible = false;
        activeSpon = true;
        offset = (Vector2)transform.position - GetMouseWorldPosition();
        rb.gravityScale = 0f;
    }

    public void DeactivateSpon()
    {
        activeSpon = false;
        Cursor.visible = true;
        rb.gravityScale = 1f;
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, obstacleLayer);
        if (hit.collider != null)
        {
            return hit.point;
        }
        return worldPosition;
    }
}
