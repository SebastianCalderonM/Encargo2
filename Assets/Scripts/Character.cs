using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
   
    private Vector3 _direction;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).sqrMagnitude == 0) return;

        
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        var targetAngle = Mathf.Atan2(move.x, move.z)* Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        
        characterController.Move(move*Time.deltaTime*Speed);
    }
}
