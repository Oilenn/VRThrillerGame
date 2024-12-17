using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lever : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody рычага
    private XRBaseInteractor interactor; // Объект, который схватил рычаг
    private bool isGrabbed = false;
    public bool IsGrabbed { get { return isGrabbed; } }


    [Header("Movement Limits")]
    public Vector2 xRotationLimits = new Vector2(-30f, 30f); // Ограничение по оси X
    public Vector2 yRotationLimits = new Vector2(-30f, 30f); // Ограничение по оси Y

    private Vector3 initialInteractorPosition; // Начальное положение руки
    private Quaternion initialRotation; // Начальное вращение рычага


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition; // Фиксируем положение, но позволяем вращаться
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
            // Разница между текущей и начальной позицией руки
            Vector3 handDelta = interactor.transform.position - initialInteractorPosition;

            // Рассчитываем углы на основе смещения
            float xAngle = Mathf.Clamp(handDelta.z * 100f, xRotationLimits.x, xRotationLimits.y); // Движение вперед-назад
            float yAngle = Mathf.Clamp(-handDelta.x * 100f, yRotationLimits.x, yRotationLimits.y); // Движение влево-вправо

            // Применяем поворот
            Quaternion targetRotation = initialRotation * Quaternion.Euler(xAngle, yAngle, 0);
            rb.MoveRotation(targetRotation);
        }
    }
}
