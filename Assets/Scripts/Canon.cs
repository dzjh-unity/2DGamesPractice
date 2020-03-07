using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameScripts/Canon")]

public class Canon : MonoBehaviour
{
    // 射击定时器
    float m_shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput() {
        m_shootTimer -= Time.deltaTime;

        // 获得鼠标位置
        Vector3 ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);

        // 大炮的位置
        Vector3 pos = this.transform.position;

        // 单击鼠标左键开火
        if (Input.GetMouseButton(0)) {
            // 计算鼠标位置鱼大炮位置之间的角度
            Vector2 targetDir = ms - pos;
            float angle = Vector2.Angle(targetDir, Vector3.up);
            if (ms.x > pos.x) {
                angle = -angle;
            }
            this.transform.eulerAngles = new Vector3(0, 0, angle);

            if (m_shootTimer <= 0) {
                m_shootTimer = 0.1f; // 每隔0.1秒可射击一次
                // 开火，创建子弹实例
                Fire.Create(this.transform.TransformPoint(0, 1, 0), new Vector3(0, 0, angle));
            }
        }
    }
}
