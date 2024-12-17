using UnityEngine;

public class ThirdQuest : Quest
{
    [Header("Lever Settings")]
    public Lever lever; // Контроллер рычага

    [Header("Laser Settings")]
    public Laser laser; // Контроллер лазера
    public Transform targetPoint; // Цель для лазера

    [Header("Door Settings")]
    public GameObject door; // Объект двери

    private bool leverPulled = false; // Состояние рычага
    private bool laserHitTarget = false; // Состояние попадания лазера
    private bool doorOpened = false; // Состояние двери

    public override void StepOn()
    {
        // Шаг 1: Проверяем, потянут ли рычаг
        if (!leverPulled && lever.IsGrabbed)
        {
            leverPulled = true;
            Debug.Log("Lever has been pulled!");
        }

        // Шаг 2: Проверяем попадание лазера в цель
        if (leverPulled && !laserHitTarget && laser.IsLaserHittingTarget(targetPoint))
        {
            laserHitTarget = true;
            Debug.Log("Laser hit the target!");
        }

        // Шаг 3: Открываем дверь
        if (leverPulled && laserHitTarget && !doorOpened)
        {
            OpenDoor();
            DoneQuest();
        }
    }

    private void OpenDoor()
    {
        door.SetActive(false); // Отключаем дверь или запускаем анимацию открытия
        doorOpened = true;
        Debug.Log("Door opened!");
    }
}
