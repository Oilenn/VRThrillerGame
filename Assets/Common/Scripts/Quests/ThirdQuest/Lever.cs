using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lever : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody ������
    private XRBaseInteractor interactor; // ������, ������� ������� �����
    private bool isGrabbed = false;
    public bool IsGrabbed { get { return isGrabbed; } }


    [Header("Movement Limits")]
    public Vector2 xRotationLimits = new Vector2(-30f, 30f); // ����������� �� ��� X
    public Vector2 yRotationLimits = new Vector2(-30f, 30f); // ����������� �� ��� Y

    private Vector3 initialInteractorPosition; // ��������� ��������� ����
    private Quaternion initialRotation; // ��������� �������� ������


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition; // ��������� ���������, �� ��������� ���������
    }

    void OnEnable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        interactor = (XRBaseInteractor)args.interactorObject;
        initialInteractorPosition = interactor.transform.position;
        initialRotation = transform.rotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        interactor = null;
    }

    void Update()
    {
        if (isGrabbed && interactor != null)
        {
            // ������� ����� ������� � ��������� �������� ����
            Vector3 handDelta = interactor.transform.position - initialInteractorPosition;

            // ������������ ���� �� ������ ��������
            float xAngle = Mathf.Clamp(handDelta.z * 100f, xRotationLimits.x, xRotationLimits.y); // �������� ������-�����
            float yAngle = Mathf.Clamp(-handDelta.x * 100f, yRotationLimits.x, yRotationLimits.y); // �������� �����-������

            // ��������� �������
            Quaternion targetRotation = initialRotation * Quaternion.Euler(xAngle, yAngle, 0);
            rb.MoveRotation(targetRotation);
        }
    }
}
