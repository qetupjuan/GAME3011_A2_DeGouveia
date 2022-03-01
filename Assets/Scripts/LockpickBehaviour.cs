using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockpickBehaviour : MonoBehaviour
{
    public GameManagerSc gameManager;
    public float syringeMovement;
    public float holePosition;
    public float tension = 0f;
    public float sweetSpot;
    [SerializeField]
    public float syringeSpeed = 5f, holeSpeed = 0.8f, 
    retentionSpeed = 0.4f, failRange = 1f, margin = 0.1f;

    public Animator animator;
    public bool isAttempting = false;
    public bool shaking;

    public int totalSyringes = 3;
    public int currentSyringes;

    private void Awake()
    {
        Init();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManagerSc>();
    }

    float RotationMargin
    {
        get
        {
            return 1f - Mathf.Abs(sweetSpot - SyringeMovement) + margin;
        }
    }

    void Init()
    {
        Reset();
        currentSyringes = totalSyringes;
        sweetSpot = UnityEngine.Random.Range(0f, 1f);
    }
    public void Reset()
    {
        HolePosition = 0;
        SyringeMovement = 0.5f;
        tension = 0;
        isAttempting = false;
    }

    private void Update()
    {
        if (isAttempting == true) { return; }
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
        SyringeMovement += Input.GetAxisRaw("Mouse X") * Time.deltaTime * syringeSpeed;
    }
    private void Hole()
    {
        HolePosition -= retentionSpeed * Time.deltaTime;
        HolePosition += Mathf.Abs(Input.GetAxisRaw("Vertical")) * Time.deltaTime * holeSpeed;
        if (HolePosition > 0.99999f)
        {
            Cracked();
        }
    }
    private void AggressivelyShaking()
    {
        shaking = RotationMargin - HolePosition < 0.03f;
        if (shaking)
        {
            tension += Time.deltaTime * failRange;
            if (tension > 1f)
            {
                Debug.Log("Syringe broke");
                totalSyringes--;
                SyringeBreak();
            }
        }
    }
    public float SyringeMovement
    {
        get
        {
            return syringeMovement;
        }
        set
        {
            syringeMovement = value;
            syringeMovement = Mathf.Clamp(syringeMovement, 0f, 1f);
        }
    }

    public float HolePosition
    {
        get
        {
            return holePosition;
        }
        set
        {
            holePosition = value;
            holePosition = Mathf.Clamp(holePosition, 0f, RotationMargin);
        }
    }
    private void SyringeBreak()
    {
        isAttempting = true;
        Reset();
    }

    private void Cracked()
    {
        isAttempting = true;
        Debug.Log("Win!");
        //gameManager.GetComponent<GameManagerSc>().winPanel = true;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("SyringeMovement", SyringeMovement);
        animator.SetFloat("HolePosition", HolePosition);
        animator.SetBool("isShaking", shaking);
    }
}
