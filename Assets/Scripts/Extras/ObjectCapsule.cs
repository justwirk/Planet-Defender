using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCapsule : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int capsuleSize = 10;

    private List<GameObject> _capsule;
    private GameObject _capsuleContainer; 

    private void Awake()
    {
        _capsule = new List<GameObject>();
        _capsuleContainer = new GameObject($"Capsule - {prefab}");

        CreateCapsules();
    }

    private void CreateCapsules()
    //capsuleSize kadar nesne yaratır ve havuza ekler.
    {
        for (int i = 0; i < capsuleSize; i++)
        {
            _capsule.Add(CreateInstance());
        }
    }

    private GameObject CreateInstance()
    //Bu metod, yeni bir nesne yaratır, bunu _capsuleContainer altında bir çocuk nesne yapar ve devre dışı (inactive) olarak ayarlar.
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_capsuleContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromCapsule()
    //havuzdan kullanılabilir bir nesne bulur ve döner. Eğer havuzda aktif olmayan nesne bulunamazsa, yeni bir nesne yaratır.
    {
        for (int i = 0; i < _capsule.Count; i++)
        {
            if (!_capsule[i].activeInHierarchy)
            {
                return _capsule[i];
            }
        }
        return CreateInstance();
    }

    public static void ReturnToCapsule(GameObject instance)
    {
        instance.SetActive(false);
    }

    public static IEnumerator ReturnToCapsuleDelay(GameObject instance, float delay)
    //belirtilen süre boyunca bekler ve ardından verilen nesneyi devre dışı yapar ve havuza geri döndürür.
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}
