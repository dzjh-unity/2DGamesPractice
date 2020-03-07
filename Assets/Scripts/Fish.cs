using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameScripts/Fish")]

public class Fish : MonoBehaviour
{
    protected float m_moveSpeed = 2.0f; // 鱼的移动速度
    protected int m_life = 10; // 生命值

    public enum Target { // 移动方向
        Left = 0,
        Right = 1,
    }
    public Target m_target = Target.Right; // 当前移动目标（方向）
    public Vector3 m_targetPosition; // 目标位置

    public delegate void VoidDelegate(Fish fish);
    public VoidDelegate OnDeath; // 死亡回调

    public static Fish Create(GameObject prefab, Target target, Vector3 pos) {
        GameObject go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
        Fish fish = go.AddComponent<Fish>();
        fish.m_target = target;
        return fish;
    }

    public void SetDamage(int damage) {
        m_life -= damage;
        if (m_life <= 0) {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/explosion");
            GameObject explosion = (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation); // 创建鱼死亡时的爆炸效果
            Destroy(explosion, 1.0f);
            
            OnDeath(this); // 死亡回调
            Destroy(this.gameObject); // 删除鱼
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void SetTarget() {
        // 随机值
        float rand = Random.value;
        // 设置Sprite翻转方向
        Vector3 scale = this.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (m_target == Target.Right ? 1 : -1);
        this.transform.localScale = scale;

        float cameraz = Camera.main.transform.position.z;
        // 设置目标位置
        m_targetPosition = Camera.main.ViewportToWorldPoint(new Vector3((int)m_target, 1* rand, -cameraz));
    }

    void UpdatePosition() {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPosition, m_moveSpeed * Time.deltaTime);
        if (Vector3.Distance(pos, m_targetPosition) < 1.0f) { // 如果移动到目标位置
            m_target = m_target == Target.Left ? Target.Right : Target.Left;
            SetTarget();
        }
        this.transform.position = pos;
    }
}
