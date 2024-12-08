using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class ContinousMovement : MonoBehaviour
{
    public XRNode inputSource;
    //public InputDevice leftController;
    public InputHelpers.Button couchActivationButton;
    public LayerMask groundLayer;
    public XRController leftController;
    private Vector2 inputAxis;
    private CharacterController character;
    private XROrigin rig;
    public float gravity = -9.81f;
    private readonly float heightOffset = 0.2f;
    private float fallingSpeed;
    public float activationThreshold = 0.0000000001f;

    [SerializeField]
    private float speed = 1f;

    public bool enableCrouch { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * speed * Time.fixedDeltaTime);

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        Debug.Log(enableCrouch);
    }

    // Update is called once per frame
    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

    }

    private bool CheckIfGrounded()
    {   
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    private void CapsuleFollowHeadset()
    {
        if (!CheckIfCrouchButtonPressed())
        {   
            character.height = rig.CameraInOriginSpaceHeight + heightOffset;
        }
        else
        {
            character.height = rig.CameraInOriginSpaceHeight + heightOffset; 
        }
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    private bool CheckIfCrouchButtonPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool isPressed);
        return isPressed;
    }
}
