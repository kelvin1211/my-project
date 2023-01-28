using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item/item")]
public class MyItem : ScriptableObject
{
    public string itemName ;// ������ �̸�
    public ItemType itemtype;  //����������
    public Sprite itemImg; // ������ �̹���
    public GameObject itemPrefab; //������ ������
    public int itemCount; // ȹ���� ������ ����

    public enum ItemType
    {
        Item, Area, 
    }
}
