using UnityEngine;

public class ThirdQuest : Quest
{
    [Header("Lever Settings")]
    public Lever lever; // ���������� ������

    [Header("Laser Settings")]
    public Laser laser; // ���������� ������
    public Transform targetPoint; // ���� ��� ������

    [Header("Door Settings")]
    public GameObject door; // ������ �����

    private bool leverPulled = false; // ��������� ������
    private bool laserHitTarget = false; // ��������� ��������� ������
    private bool doorOpened = false; // ��������� �����

    public override void StepOn()
    {
        // ��� 1: ���������, ������� �� �����
        if (!leverPulled && lever.IsGrabbed)
        {
            leverPulled = true;
            Debug.Log("Lever has been pulled!");
        }

        // ��� 2: ��������� ��������� ������ � ����
        if (leverPulled && !laserHitTarget && laser.IsLaserHittingTarget(targetPoint))
        {
            laserHitTarget = true;
            Debug.Log("Laser hit the target!");
        }

        // ��� 3: ��������� �����
        if (leverPulled && laserHitTarget && !doorOpened)
        {
            OpenDoor();
            DoneQuest();
        }
    }

    private void OpenDoor()
    {
        door.SetActive(false); // ��������� ����� ��� ��������� �������� ��������
        doorOpened = true;
        Debug.Log("Door opened!");
    }
}
