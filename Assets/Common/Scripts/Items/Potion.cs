using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    //���������� �� ������� ����������� �������� ������
    private float healthPoints = 50;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<Player>().Health += healthPoints;

            Destroy(gameObject);
        }
    }

    
}
