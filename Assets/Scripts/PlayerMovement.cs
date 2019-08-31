﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private VariableJoystick InpuJoyStick;
    [SerializeField] private float m_Speed;
    [SerializeField][Range(0f,1f)] private  float m_SmoothTime=0.25f;
    [SerializeField] private string MoveAnimationXName,MoveAnimationYName;
     private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;

    private DialogueRunner dialogueRunner;


    private Vector2 velocity;
    private void Init()//初始化
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody2D.freezeRotation = true;
        dialogueRunner = FindObjectOfType<DialogueRunner>();

    }

    private void Move(Vector2 Pos)
    {
        var TargetVelocity = new Vector2((Pos.x * m_Speed*50 * Time.fixedDeltaTime), Pos.y*m_Speed*25*Time.fixedDeltaTime);
            m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity,TargetVelocity,ref velocity,m_SmoothTime );;

    }

    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (dialogueRunner.isDialogueRunning)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            if (InpuJoyStick.gameObject.activeInHierarchy)
                InpuJoyStick.gameObject.SetActive(false);;
        }
        else
        {
            if (!InpuJoyStick.gameObject.activeInHierarchy)
                InpuJoyStick.gameObject.SetActive(true);;
            Move(new Vector2(InpuJoyStick.Horizontal,InpuJoyStick.Vertical));
        }

        m_Animator.SetFloat(MoveAnimationXName,m_Rigidbody2D.velocity.x);
        m_Animator.SetFloat(MoveAnimationYName,m_Rigidbody2D.velocity.y);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
