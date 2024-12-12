using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    List<IEnemySub> enemies = new List<IEnemySub>();

    void Awake()
    {
        //���� ��� ������� ������ �� �����
        enemies = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IEnemySub>().ToList();
    }

    //�������� ���� ����������� ������ � ����� �����
    private void Update()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.OnDeactivated())
            { 
                enemies.Remove(enemy);
            }
            try
            {
                enemy.OnTriggered();
            }
            catch(MissingReferenceException ex)
            {
                continue;
            }
        }
    }
}
