using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private CuttingBoard cuttingBoard;

    //Knife moving logic things
    public Vector2 offset;
    private Rigidbody2D thisRB;
    public LayerMask obstacleLayer;
    private bool canMove = false;
    private bool canCut = false;

    public GameObject spot;

    //Animation
    private Animator animator;
    //Sound
    private SoundEffects soundEffects;
    private void Awake()
    {
        cuttingBoard = CuttingBoard.Instance;
        thisRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundEffects = SoundEffects.Instance;
    }
    public void ActivateKnife()
    {
        Cursor.visible = false;
        canMove = true;
    }

    public void DeactivateKnife()
    {
        canMove = false;
        canCut = false;
        if (cuttingBoard.GetCuttingBoardState())
        {
            cuttingBoard.Fail();
        }
        Cursor.visible = true;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cuttingBoard.GetCuttingBoardState())
        {
            DeactivateKnife();
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, obstacleLayer);
        if (hit)
        {
            if(hit.collider.gameObject.CompareTag(spot.tag))
            {
                if(Input.GetMouseButtonUp(0) && cuttingBoard.GetCuttingBoardState())
                {
                    if (canCut)
                    {
                        cuttingBoard.PlayCutAnimation();
                        StartCoroutine(PlayAnimation());
                        cuttingBoard.IncrementSpriteIndex();
                        soundEffects.CutSound();
                    }
                    else
                    {
                        canCut = true;
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (cuttingBoard.GetCuttingBoardState() && canMove)
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
        if (hit.collider != null)
        {
            return hit.point;
        }
        return worldPosition;
    }

    private IEnumerator PlayAnimation()
    {
        canMove = false;
        canCut = false;
        animator.Play("KnifeAnimation", 0, 0f);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        canMove = true;
    }

    private float GetAnimationLength()
    {
        float length = 0f;
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            length = Mathf.Max(length, clip.length);
        }
        Debug.Log(length);
        return length;
    }
}
