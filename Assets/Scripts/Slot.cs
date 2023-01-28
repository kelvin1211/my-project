using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jason;
using System.IO;

namespace Kelvin
{
    public class Slot : MonoBehaviour
    {
        //ItemPickUp myItemPickup = new ItemPickUp();
        Inventory myInventory;
        SaveNLoad mySaveNLoad;


        public MyItem item; // ȹ���� ������
        public int itemCount; // ȹ���� ������ ����
        public Image itemSlotImage; // ȹ���� ������ �������� �̹���



        //�ʿ��� ������Ʈ
        //[SerializeField]
        //private Text text_Count;
        //[SerializeField]
        //private GameObject go_CountImage;



        private void Awake()
        {
             myInventory = new Inventory();
             mySaveNLoad = new SaveNLoad();
        }

        //�̹��� ���� ����
        private void SetColor(float _alpha)
        {
            if(itemSlotImage.sprite!= null)
            {
                Color color = itemSlotImage.color;
                color.a = _alpha;
                itemSlotImage.color = color;
            }

            

            //ColorBlock colorBlock = ItemButton.colors;
            //colorBlock.normalColor = new Color(10f, 1f, 1f);
            //ItemButton.colors = colorBlock;
        }

        //������ ȹ��
        public void AddItem(string _itemName,MyItem _item)
        {
            item = _item;
            //itemCount = _count;
            itemSlotImage.sprite = item.itemImg;

            if (File.Exists(mySaveNLoad.SAVE_DATA_DIRECTORY + mySaveNLoad.SAVE_FILENAME))
            {
                if(mySaveNLoad.GetIsloading())
                {
                    switch (_itemName)
                    {
                        case "Knife":
                            Transform knife = myInventory.knife();
                            itemSlotImage.sprite = knife.gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren <Image>().enabled= true;
                            break;
                        case "Ox":
                            itemSlotImage.sprite = myInventory.ox().gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren<Image>().enabled = true;
                            break;
                        case "Key":
                            itemSlotImage.sprite = myInventory.key().gameObject.GetComponent<Image>().sprite;
                            gameObject.GetComponentInChildren<Image>().enabled = true;
                            break;
                    }
                }
                
            }

            

            
            Debug.Log($"=======additem======" + item.itemName);

            //GameManager.instance.saveDataList._currentSlotItems
            //�������� �ߺ����� ȹ�氡���ϴٸ� ������ ǥ�����ִ� �ؽ�Ʈ Ȱ��ȭ
            //if(item.itemtype != MyItem.ItemType.case1)
            //{
            //    text_Count.text = itemCount.ToString();
            //    go_CountImage.SetActive(true);
            //}
            //else
            //{
            //    text_Count.text = "0";
            //    go_CountImage.SetActive(false);
            //}
            SetColor(1);
        }


        //������ ���� ����
        //public void SetSlotCount(int _count)
        //{
        //    itemCount += _count;
        //    text_Count.text = itemCount.ToString();

        //    if(itemCount <= 0 )
        //    {
        //        ClearSlot();
        //    }
        //}

        Toggle myToggle;

        //���� �ʱ�ȭ
        public void ClearSlot()
        {
            item = null;
            //itemCount = 0;
            itemSlotImage.sprite = null;
            SetColor(0);
            
            //go_CountImage.SetActive(false);
            //text_Count.text = "0";
        }
    }
}