using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameScripts/FishSpawn")]

public class FishSpawn : MonoBehaviour
{
    // 生成计时器
    public float timer = 0;
    // 最大生成数量
    public int max_count = 30;
    // 当前鱼的数量
    public int cur_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            // 重新计时
            timer = 2.0f;
            // 如果鱼的数量达到最大数量则返回
            if (cur_count >= max_count) {
                return;
            }
            // 随机1、2、3产生不同的鱼
            int index = 1 + (int)(Random.value *3.0f);
            if (index > 3) {
                index = 3;
            }
            // 更新鱼的数量
            cur_count ++;
            // 读取鱼的prefab
            GameObject fishprefab = (GameObject)Resources.Load("Prefabs/fish"+index);

            float cameraz = Camera.main.transform.position.z;
            // 鱼的初始随机位置
            Vector3 randpos = new Vector3(Random.value, Random.value, -cameraz);
            randpos = Camera.main.ViewportToWorldPoint(randpos);
            // 鱼的随机初始方向
            Fish.Target target = Random.value > 0.5f ? Fish.Target.Right : Fish.Target.Left;
            Fish f = Fish.Create(fishprefab, target, randpos);
            // 注册鱼的死亡回调
            f.OnDeath += OnDeath;
        }
    }

    void OnDeath(Fish fish) {
        cur_count --; // 更新鱼的数量
    }
}
