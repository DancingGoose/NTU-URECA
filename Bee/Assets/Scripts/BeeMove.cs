using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMove : MonoBehaviour
{
    public float _moveSpeed = 5f; 
    public float _rotationSpeed = 30f; 
    protected Rigidbody mRigidbody;

    protected string mVerticalAxisInputName = "Vertical";
    protected string mHorizontalAxisInputName = "Horizontal";
    protected float mVerticalInputValue = 0f;
    protected float mHorizontalInputValue = 0f;

    public bool _inputIsEnabled = true;


    // Awake is called right at the beginning if the object is active. Only run once in a lifetime
    private void Awake()
    {
        // Reference the component from the object. The component must be in the object for it to be referenced, else it will return null
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Capture movement input
        MovementInput();
    }

    // Physics update. Update regardless of FPS
    void FixedUpdate()
    {
        Move();
        Spin();
    }

    // Record the value from the input dictionary
    protected void MovementInput()
    {
        mVerticalInputValue = -Input.GetAxis(mVerticalAxisInputName );
        mHorizontalInputValue = Input.GetAxis(mHorizontalAxisInputName);
    }


    // Move the ball based on speed
    public void Move()
    {
        Vector3 moveVect = transform.forward * _moveSpeed * Time.deltaTime * mVerticalInputValue;
        mRigidbody.MovePosition(mRigidbody.position + moveVect);
    }

    // Spin the ball without changing its location. During game play mode, see inspector view-->Transform--> Rotation field to see the changes in angle.
    public void Spin()
    {
        float rotationDegree = _rotationSpeed * Time.deltaTime * mHorizontalInputValue;
        Quaternion rotQuat = Quaternion.Euler(0f, rotationDegree, 0f);
        mRigidbody.MoveRotation(mRigidbody.rotation * rotQuat);
    }

    public void Restart(Vector3 pos, Quaternion rot)
    {
        // Reset position, rotation
        transform.position = pos;
        transform.rotation = rot;


        // Activate the gameobject and input
        gameObject.SetActive(true);
        if (!_inputIsEnabled)
            _inputIsEnabled = true;

    }
}

