using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 startPoint;  // �ړ��J�n�ʒu
    public Vector3 endPoint;    // �ړ��I���ʒu
    public float moveSpeed = 3f;  // �ړ��X�s�[�h
    private bool movingToEnd = true;  // �ǂ̕����Ɉړ����邩�̃t���O

    void Start()
    {
        // �����ʒu��startPoint�ɐݒ�
        transform.position = startPoint;
    }

    void Update()
    {
        // �ړ������ɉ����Ĉʒu���X�V
        if (movingToEnd)
        {
            // endPoint�Ɍ������Ĉړ�
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);

            // �ڕW�ʒu�ɓ��B�����甽�Ε����Ɉړ�
            if (transform.position == endPoint)
            {
                movingToEnd = false;
            }
        }
        else
        {
            // startPoint�Ɍ������Ĉړ�
            transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);

            // �ڕW�ʒu�ɓ��B�����甽�Ε����Ɉړ�
            if (transform.position == startPoint)
            {
                movingToEnd = true;
            }
        }
    }
}
