using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserBeam; // ������ ��������� ���� (3D ������, ��������, �������)
    public Transform laserSpawnPoint; // �����, ������ ����������� �����
    public float laserLength = 10f; // ����� ������
    public LayerMask hitLayerMask; // ����, � �������� ��������������� �����

    private GameObject activeLaser; // ������� �������� �������� ���

    private void Update()
    {
        // ��������� �������� ���
        if (activeLaser == null)
        {
            activeLaser = Instantiate(laserBeam, laserSpawnPoint);
        }
        UpdateLaserBeam();

        // ��������� ��������� � ����
        PerformRaycast();
    }

    private void UpdateLaserBeam()
    {
        // ����������� ����� ������
        if (activeLaser != null)
        {
            activeLaser.transform.localScale = new Vector3(0.1f, 0.1f, laserLength);
            activeLaser.transform.localPosition = new Vector3(0, 0, laserLength / 2); // ���������� ���
        }
    }

    private void PerformRaycast()
    {
        Vector3 laserStart = laserSpawnPoint.position;
        Vector3 laserDirection = laserSpawnPoint.forward;

        // ��������� ���������
        if (Physics.Raycast(laserStart, laserDirection, out RaycastHit hit, laserLength, hitLayerMask))
        {
            Debug.Log($"Laser hit: {hit.collider.name}");
        }
    }

    public bool IsLaserHittingTarget(Transform targetPoint)
    {
        Vector3 laserStart = laserSpawnPoint.position;
        Vector3 laserDirection = laserSpawnPoint.forward;

        // ��������� ��������� � ����
        if (Physics.Raycast(laserStart, laserDirection, out RaycastHit hit, laserLength, hitLayerMask))
        {
            return hit.collider.transform == targetPoint;
        }

        return false;
    }
}
