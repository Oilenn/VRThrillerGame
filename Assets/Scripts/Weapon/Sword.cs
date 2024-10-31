using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для меча
public class Sword : MonoBehaviour, IWeapon
{
    //дамаг от конкретного оружия, в этом случае от меча
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
