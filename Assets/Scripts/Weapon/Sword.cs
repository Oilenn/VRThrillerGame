using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��� ����
public class Sword : MonoBehaviour, IWeapon
{
    //����� �� ����������� ������, � ���� ������ �� ����
    private float damageHp = 10;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        print("Collision success");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(obj.GetComponent<Enemy>());
        }
    }

    public void Damage(Enemy enemy)
    {
        enemy.Health -= damageHp;
    }
}
