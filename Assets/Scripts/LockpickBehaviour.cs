using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockpickBehaviour : MonoBehaviour
{
    float syringeMovement;
    float holePosition;
    float tension = 0f;
    [SerializeField]
    float syringeSpeed = 1f, holeSpeed = 0.8f, retentionSpeed = 0.4f, failRange = 1f;

    Animator animator;
    bool paused = false;
    bool shaking;

    int totalSyringes = 5;


    float targetPosition;
    [SerializeField] float margin = 0.1f;
    float MaxRotationDistance
    {
        get
        {
            return 1f - Mathf.Abs(targetPosition - SyringeMovement) + margin;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        Reset();

        targetPosition = UnityEngine.Random.value;
    }
    public void Reset()
    {
        HolePosition = 0;
        SyringeMovement = 0.5f;
        tension = 0;
        totalSyringes--;
        paused = false;
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
            holePosition = Mathf.Clamp(holePosition, 0f, MaxRotationDistance);
        }
    }

    private void Update()
    {
        if (paused == true) { return; }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            Syringe();
        }
        AggressivelyShaking();
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
        if (HolePosition > 0.98f)
        {
            Cracked();
        }
    }
    private void AggressivelyShaking()
    {
        shaking = MaxRotationDistance - HolePosition < 0.03f;
        if (shaking)
        {
            tension += Time.deltaTime * failRange;
            if (tension > 1f)
            {
                SyringeBreak();
            }
        }
    }
    private void SyringeBreak()
    {
        paused = true;
        Reset();
    }

    private void Cracked()
    {
        paused = true;
        Debug.Log("win");
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("SyringeMovement", SyringeMovement);
        animator.SetFloat("HolePosition", HolePosition);
        animator.SetBool("isShaking", shaking);
    }
    public void OpenLock(int level, Component tg)
    {

    }
}
