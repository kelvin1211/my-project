using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kelvin
{
    public class PlayerData4Saving : MonoBehaviour
    {
        //���� ����� ������� ����

        //������ �� ����
        GameObject currentRoom;

        //����� �̼�


        //������ ������
        Image[] currentSlotItems;

        public void currentRoomData(GameObject _room)
        {
            currentRoom = _room;
        }

        public void currentSlotItemData(Image[] _item)
        {
            for (int i = 0; i < currentSlotItems.Length; i++)
            {
                currentSlotItems[i] = _item[i];
            }
        }
    }
}