using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshTimer : MonoBehaviour
{
    private float time = 0.0f;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 300.0f)//5�� ������� ���˸�
        {
            time = 0.0f;
            count++;
            warning();
        }
    }
    void warning()
    {
        int useTime = count * 5;
        string message = "����Ͻ��� " + useTime.ToString() + "���� �������ϴ�.\n�������ų� ���� ���� �����ôٸ� ��⸦ ���� �޽��� �����ֽñ� �ٶ��ϴ�.";
        Debug.Log(message);

        // GameManager�� ShowWarningTextForDuration �Լ� ȣ��
        GameManager.Instance.ShowWarningTextForDuration(10f, message);
    }
}
