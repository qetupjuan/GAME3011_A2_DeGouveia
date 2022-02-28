using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockpickBehaviour : MonoBehaviour
{
    float syringeMovement;
    float holePosition;
    [SerializeField]
    float syringeSpeed = 1f, holeSpeed = 0.8f, retentionSpeed = 0.4f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public float SyringeMovement
    {
        get { return syringeMovement; }
        set
        {
            syringeMovement = value;
            syringeMovement = Mathf.Clamp(syringeMovement, 0f, 1f);
        }
    }

    public float HolePosition
    {
        get { return holePosition; }
        set
        {
            holePosition = value;
            holePosition = Mathf.Clamp(holePosition, 0f, 1f);
        }
    }

    private void Update()
    {
        Syringe();
        Hole();
        UpdateAnimator();

    }

    private void Syringe()
    {
        SyringeMovement += Input.GetAxisRaw("Horizontal") * Time.deltaTime * syringeSpeed;
    }
    private void Hole()
    {
        HolePosition -= retentionSpeed * Time.deltaTime;
        HolePosition += Mathf.Abs(Input.GetAxisRaw("Vertical")) * Time.deltaTime * holeSpeed;
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("SyringeMovement", SyringeMovement);
        animator.SetFloat("HolePosition", HolePosition);
    }
    public void OpenLock(int level, Component tg)
    {

    }
}
