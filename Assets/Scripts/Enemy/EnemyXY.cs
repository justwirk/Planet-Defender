using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyXY : MonoBehaviour
{

    [SerializeField] private Transform textDmgFactoryPos;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void EnemyStrike(Enemy enemy, float damage)
    //düşman hasar aldığında çağrılır. Enemy sınıfından gelen bir olay tarafından tetiklenir.
    //Belirli bir düşmanın hasar aldığını kontrol eder ve eğer hasar alan düşman bu bileşene bağlıysa,
    //hasar metnini oluşturur ve ekranda gösterir.
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Capsule.GetInstanceFromCapsule();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDmgFactoryPos);
            newInstance.transform.position = textDmgFactoryPos.position;
            newInstance.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Bullet.AtEnemyStrike += EnemyStrike;
    }

    private void OnDisable()
    {
        Bullet.AtEnemyStrike -= EnemyStrike;
    }
}
