using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameScripts/Fire")]

public class Fire : MonoBehaviour
{
    // 移动速度
    float m_moveSpeed = 10.0f;

    // 创建子弹实例，实际项目的Update中不建议使用Resource.Load，Instantiate和Destroy
    public static Fire Create(Vector3 pos, Vector3 angle) {
        // 读取子弹Sprite Prefab
        GameObject prefab = Resources.Load<GameObject>("Prefabs/fire");
        // 创建子弹Sprite实例
        GameObject fireSprite = (GameObject)Instantiate(prefab, pos, Quaternion.Euler(angle));
        Fire f = fireSprite.AddComponent<Fire>();
        Destroy(fireSprite, 2.0f);
        return f;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 更新子弹位置
        this.transform.Translate(new Vector3(0, m_moveSpeed * Time.deltaTime, 0));
    }

    void OnTriggerEnter2D(Collider2D other) {
        Fish f = other.GetComponent<Fish>();
        if (f == null) {
            return;
        } else {
            f.SetDamage(1);
        }
        Destroy(this.gameObject);
    }
}
