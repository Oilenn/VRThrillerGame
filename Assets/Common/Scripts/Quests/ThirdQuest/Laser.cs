using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserBeam; // Префаб лазерного луча (3D объект, например, цилиндр)
    public Transform laserSpawnPoint; // Точка, откуда выпускается лазер
    public float laserLength = 10f; // Длина лазера
    public LayerMask hitLayerMask; // Слои, с которыми взаимодействует лазер

    private GameObject activeLaser; // Текущий активный лазерный луч

    private void Update()
    {
        // Обновляем лазерный луч
        if (activeLaser == null)
        {
            activeLaser = Instantiate(laserBeam, laserSpawnPoint);
        }
        UpdateLaserBeam();

        // Проверяем попадание в цель
        PerformRaycast();
    }

    private void UpdateLaserBeam()
    {
        // Настраиваем длину лазера
        if (activeLaser != null)
        {
            activeLaser.transform.localScale = new Vector3(0.1f, 0.1f, laserLength);
            activeLaser.transform.localPosition = new Vector3(0, 0, laserLength / 2); // Центрируем луч
        }
    }

    private void PerformRaycast()
    {
        Vector3 laserStart = laserSpawnPoint.position;
        Vector3 laserDirection = laserSpawnPoint.forward;

        // Проверяем попадание
        if (Physics.Raycast(laserStart, laserDirection, out RaycastHit hit, laserLength, hitLayerMask))
        {
            Debug.Log($"Laser hit: {hit.collider.name}");
        }
    }

    public bool IsLaserHittingTarget(Transform targetPoint)
    {
        Vector3 laserStart = laserSpawnPoint.position;
        Vector3 laserDirection = laserSpawnPoint.forward;

        // Проверяем попадание в цель
        if (Physics.Raycast(laserStart, laserDirection, out RaycastHit hit, laserLength, hitLayerMask))
        {
            return hit.collider.transform == targetPoint;
        }

        return false;
    }
}
