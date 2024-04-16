using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    private bool activePestle = false;

    private Vector2 offset;
    public LayerMask obstacleLayer;
    public LayerMask foodLayer;
    public LayerMask cauldronAndSpoonLayers;

    private Rigidbody2D thisRB;

    public MortarCover mortarCover;
    public GameObject pointDirection;

    private SoundEffects soundEffects;
    private float speed = 10f;

    private void Start()
    {
        thisRB = GetComponent<Rigidbody2D>();

        soundEffects = SoundEffects.Instance;
    }
    public bool GetActivePestle () { return activePestle; }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !activePestle)
        {
            ActivatePestle();
        }
    }

    public void ActivatePestle()
    {
        mortarCover.GetComponent<MortarCover>().SetActivePestle(true);
        gameObject.GetComponentInChildren<PolygonCollider2D>().includeLayers = foodLayer;
        gameObject.GetComponentInChildren<PolygonCollider2D>().excludeLayers = LayerMask.GetMask("Nothing");
        gameObject.GetComponentInChildren<PolygonCollider2D>().excludeLayers += cauldronAndSpoonLayers;
        gameObject.GetComponentInChildren<Rigidbody2D>().excludeLayers = LayerMask.GetMask("Nothing");

        Cursor.visible = false;
        activePestle = true;
        offset = (Vector2)transform.position - GetMouseWorldPosition();
        thisRB.gravityScale = 0f;
        
    }
    public void DeactivatePestle()
    {
        mortarCover.GetComponent<MortarCover>().SetActivePestle(false);
        gameObject.GetComponentInChildren<PolygonCollider2D>().includeLayers = LayerMask.GetMask("Nothing");
        gameObject.GetComponentInChildren<PolygonCollider2D>().excludeLayers = foodLayer;
        gameObject.GetComponentInChildren<PolygonCollider2D>().excludeLayers += cauldronAndSpoonLayers;
        gameObject.GetComponentInChildren<Rigidbody2D>().excludeLayers = foodLayer;
        gameObject.GetComponentInChildren<Rigidbody2D>().excludeLayers += cauldronAndSpoonLayers;
        activePestle = false;
        Cursor.visible = true;
        thisRB.gravityScale = 1f;

        Mortar mortar = gameObject.GetComponentInParent<Mortar>();
        if (mortar.GetItemObject() != null && !mortar.GetActiveState())
            mortar.GetItemObject().GetComponent<CircleCollider2D>().enabled = true;
    }
    private void Update()
    {
        if(pointDirection != null && activePestle)
        {
            Vector3 vectorToTarget = pointDirection.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }
        if (Input.GetMouseButtonUp(0) && activePestle)
        {
            DeactivatePestle();
        }
    }
    private void FixedUpdate()
    {
        if (activePestle)
        {
            Vector2 newPosition = GetMouseWorldPosition() + offset;
            thisRB.MovePosition(newPosition);
        }
    }
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, obstacleLayer);
        if(hit.collider != null)
        {
            return hit.point;
        }
        return worldPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FoodObj"))
        {
            gameObject.GetComponentInParent<Mortar>().StartMash();
        }
        if (collision.gameObject.name == "Hit")
        {
            gameObject.GetComponentInParent<Mortar>().ReportHit();
        }
    }
}
