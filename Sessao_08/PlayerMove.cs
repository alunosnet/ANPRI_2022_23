//
// Copyright (c) 2022 IndieDevPt. All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController _characterController;

    public float VelocidadeRotacao = 3;
    public float VelocidadeAndar = 3;
    public float VelocidadeSalto = -2;
    [SerializeField]
    float _inputRodar;

    public float _inputAndar;
    public bool IsGrounded;
    public bool IsJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        _characterController=GetComponent<CharacterController>();
        if (_characterController == null)
            Debug.Log("O Player tem de ter um character controller");
    }

    // Update is called once per frame
    void Update()
    {
        //rotação
        _inputRodar = Input.GetAxis("Mouse X");
        transform.Rotate(transform.up * _inputRodar*VelocidadeRotacao * Time.deltaTime);

        //movimento
        _inputAndar = Input.GetAxis("Vertical");

        //correr
        if (Input.GetButton("Run") && _inputAndar>0)
            _inputAndar *= 2;

        Vector3 movimento = transform.forward * _inputAndar * VelocidadeAndar * Time.deltaTime;

        //saltar
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            movimento.y = Mathf.Sqrt(VelocidadeSalto * Physics.gravity.y)*Time.deltaTime;
            IsJumping = true;
        }
        else
        {
            IsJumping = false;
        }

        //aplicar gravidade
        movimento += Physics.gravity * Time.deltaTime;
        _characterController.Move(movimento);


        IsGrounded = _characterController.isGrounded;
    }
}
