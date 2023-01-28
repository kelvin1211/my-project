using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jason;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;
using TMPro;
using System;
using System.IO;
using Kelvin;
using UnityEngine.SceneManagement;

//public class SlotData
//{
//    bool isEmpty;
//    GameObject slotObj;
//}

namespace Kelvin
{
    public class Inventory : MonoBehaviour
    {
        SaveDataList mySaveData;

        //����� Ŭ��
        [SerializeField]
        AudioClip LockSound;
        [SerializeField]
        AudioClip UnLockSound;
        [SerializeField]
        AudioClip ItemResponSound;
        [SerializeField]
        AudioClip ItemClickSound;
        [SerializeField]
        AudioClip SurpriseSound;

        Transform parent;
        ToggleGroup toggleGroupInstance;


        [SerializeField]
        public Toggle[] toggles;

        //�� ���� ������
        private Transform J_knife;
        private Transform J_ox;
        private Transform J_key;

        public Transform knife() { return J_knife; }
        public Transform ox() { return J_ox; }
        public Transform key() { return J_key; }


        ////�� ���� Ŭ��area (�κ������� ���� ��)
        Transform J_areaA;
        Transform J_areaB;

        //�� ���� ������ ��ư
        Button Knife;
        Button Ox;
        Button Ox1;
        Button Key;

        public MyItem item;

        //�� ���� Ư�� area��ư (�κ����� ������ ���� �� Ư�� ��ġ�� Ŭ���ϱ� ����)
        Button knifeArea;
        Button OxArea;
        public Button ZoomArea;
        //public Button zoomArea() { return ZoomArea; }


        //�κ����Կ� �������ִ��� ����
        string toggleSpriteName;

        //���� �ε���
        int index;

        //�������� ���� ����
        bool isKnifeSelected = false;
        bool isOxSelected = false;
        
        bool isKeySelected = false;

        bool isToggleEmpty;

        //��� ����,���� ����
        [SerializeField]
        Color selectedColor;
        [SerializeField]
        Color UnselectedColor;


        //public List<SlotData> slots = new List<SlotData>();

        //�κ����� ���԰� �����۹�ư
        [SerializeField]
        Image[] InvenItemImg;


        public Transform Room00;
        Transform Room01;
        Transform Room02;
        Transform Room03;
        Transform Room04;


        [SerializeField]
        public Slot[] slots;

        public Slot[] GetSlots() { return slots; }

        [SerializeField]
        private MyItem[] items;


        public bool isGetKnife;

        public bool isGetOx;

        public bool isGetKey;



        //���� ��
        string CurrentScene;
        public string CurrentSceneName() { return CurrentScene; }
        
        private void Awake()
        {
            mySaveData = new SaveDataList();
            isGetKnife = mySaveData._isGetKnifeItem;
            isGetOx = mySaveData._isGetOxItem;
            isGetKey = mySaveData._isGetKeyItem;


            Debug.Log("===isKnife===" + isGetKnife);
            Debug.Log("===isox===" + isGetOx);
            Debug.Log("===isKey===" + isGetKey);

            Debug.Log("===mySaveData._isGetKnifeItem===" + mySaveData._isGetKnifeItem);
            Debug.Log("===mySaveData._isGetOxItem===" + mySaveData._isGetOxItem);
            Debug.Log("===mySaveData._isGetKeyItem===" + mySaveData._isGetKeyItem);

            /*���� �ſ����� �̱������� �����ؼ� ������ ������, �ٸ� �ſ����� ������� ���´�.
            Debug.Log("invent=====" + GameManager.instance.myName);*/
            
            LoadComponent();
            UnSelectToggle();
        }

        private void Start()
        {
            GameManager.instance.LoadComponents();
            ItemClickEvent();
            areaClickEvent();

             //= mySaveData._currentStage;


            Debug.Log("invent CurrentScene==" + CurrentScene);
        }

        /// <summary>
        /// ������ �߰� ����
        /// 1.�޸��Ҵ�
        /// 2.������ scriptable object ����
        /// 3.������ ���� ���� ����   >>>>isKeySelected
        /// 4.���ǹ� �߰�             >>>>CheckingSelected
        /// 5.�ش� area �Լ� �߰�     >>>>  areaClickEvent
        /// 6.������ Ŭ�� �̺�Ʈ �Լ� �߰�   >>>> ItemClickEvent
        /// 6.
        /// </summary>
       
        public void LoadComponent()
        {
            // ���� ���̸�
            CurrentScene = SceneManager.GetActiveScene().name;
            Debug.Log("invenscene==" + CurrentScene);

            //CurrentScene = GameManager.instance._Scene();

            parent = AssetAssist.FindObject("UICanvas");
            //��۱׷�� �ڽ��� ��� ������Ʈ �޸��Ҵ�
            toggleGroupInstance = AssetAssist.FindComponent<ToggleGroup>("Content", parent);
            toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();


            //Room Transform
            Room00 = AssetAssist.FindComponent<Transform>("Room0", parent);
            Room01 = AssetAssist.FindComponent<Transform>("Room1", parent);
            Room02 = AssetAssist.FindComponent<Transform>("Room2", parent);
            Room03 = AssetAssist.FindComponent<Transform>("Room3", parent);
            Room04 = AssetAssist.FindComponent<Transform>("Room4", parent);


            //������ �ҷ�����
            J_knife = AssetAssist.Instantiate("Prefebs/Items/Knife", Room00);
            J_ox = AssetAssist.Instantiate("Prefebs/Items/Ox", Room01);
            J_key = AssetAssist.Instantiate("Prefebs/Items/Key", Room01);
            J_areaA = AssetAssist.Instantiate("Prefebs/Area/KnifeArea", Room03);
            J_areaB = AssetAssist.Instantiate("Prefebs/Area/OxArea", Room04);

            ////KnifePrefeb = Resources.Load("Prefebs/Items/Knife")as GameObject;


            RemoveText(J_knife.gameObject);
            RemoveText(J_ox.gameObject);
            RemoveText(J_key.gameObject);
            RemoveText(J_areaA.gameObject);
            RemoveText(J_areaB.gameObject);


            //�ҷ��� ������ �ʱ���ġ ����
            J_key.localPosition = new Vector3(1, 1, 0);





            //�κ��� �����۽��� �޸��Ҵ�
            for (int i = 0; i < InvenItemImg.Length; i++)
            {
                InvenItemImg[i] = AssetAssist.FindComponent<Image>($"item" + i, parent);

                if (InvenItemImg[i] == null)
                {
                    InvenItemImg[i].enabled = false;
                    toggles[i].enabled = false;
                }
            }


            ////���� Ư��area �޸� �Ҵ�
            knifeArea = AssetAssist.FindComponent<Button>("KnifeArea", parent);
            OxArea = AssetAssist.FindComponent<Button>("OxArea", parent);
            ZoomArea = AssetAssist.FindComponent<Button>("ZoomIn", parent);
        }


        public void LoadToInvent(int _arrayNum, string _itemName, int _itemCount)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].itemName == _itemName)
                {
                    slots[_arrayNum].AddItem(items[i].itemName, items[i]);
                }
            }
        }


        //Ŭ�а�ü �ؽ�Ʈ�����
        public void RemoveText(GameObject preFab)
        {
            int index = preFab.name.IndexOf("(Clone)");
            if (index > 0)
                preFab.name = preFab.name.Substring(0, index);
        }

        //��� ���Ű ��Ȱ��ȭ
        public void UnSelectToggle()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].isOn = false;
            }
        }


        //���� �Ű������� �ش� �������� ������ additem�Լ��� ���� >>  ���޹��� additem�� �ش� �������� sprite�� ����
        public void AccquireItem(MyItem _item)
        {

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    toggles[i].enabled = true;
                    InvenItemImg[i].enabled = true;
                    slots[i].AddItem(_item.itemName, _item);
                    isToggleEmpty = true;
                    return;
                }
            }
        }


        /*������ ������ sprite ����

        //void PutItemIntoInven(string Name)
        //{
        //    for (int i = 0; i < InvenItemImg.Length; i++)
        //    {
        //        if (InvenItemImg[i].sprite == null)
        //        {
        //            //Resources ������ �ִ� sprite �̹��� ȣ��
        //            Sprite ChangeSprite = Resources.Load<Sprite>($"Items/Img/" + Name);
        //            InvenItemImg[i].sprite = ChangeSprite;
        //            break;
        //        }


        //    }
        //}

        */

        //���� ��ġ�� ������ Ŭ���� �ش� �����ۺ�Ȱ��ȭ & ���Կ� �߰��ϴ� �Լ� ���� & Ư�� area Ŭ���� ������ Ȱ��ȭ
        // �ʵ� ������ Ŭ�� �̺�Ʈ



        // �ٽû����� ������ ����ȵǴ� ����!!!!!!!!!!!!!!
        private void ItemClickEvent()
        {
            if (CurrentScene== "Stage01")
            {
                if (J_knife.gameObject != null)
                {
                    J_knife.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_knife.GetComponent<ItemPickUp>().item);
                        isGetKnife = true;
                        Destroy(J_knife.gameObject, 0.1f);
                    });
                }


                if (J_ox.gameObject != null)
                {
                    J_ox.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_ox.GetComponent<ItemPickUp>().item);

                        isGetOx = true;
                        Destroy(J_ox.gameObject, 0.1f);
                        //Ox.gameObject.SetActive(false);
                    });
                }


                if (J_key.gameObject != null)
                {
                    J_key.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(ItemClickSound);
                        AccquireItem(J_key.GetComponent<ItemPickUp>().item);
                        isGetKey = true;
                        Destroy(J_key.gameObject, 0.1f);
                        //Key.gameObject.SetActive(false);
                    });
                }
            }
        }

        //onclick ��ư �̺�Ʈ�� destory �� �� �ı� �ȴ�. Instantiate �� ���� �ؼ� �� �� �ٽ�, onClick �̺�Ʈ�� �ɾ�����Ѵ�.

        // �ʵ� area Ŭ�� �̺�Ʈ
        private void areaClickEvent()
        {
            if (CurrentScene == "Stage01")
            {
                if (J_knife.gameObject.activeSelf || J_key.gameObject.activeSelf)
                {
                    knifeArea.onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(LockSound);
                        if (isKnifeSelected || isKeySelected)
                        {
                            UnSelectToggle();
                            isKnifeSelected = false;
                            isKeySelected = false;

                            if (toggleSpriteName == "Knife")
                            {
                                slots[index].ClearSlot();

                                //������ ����
                                J_knife = AssetAssist.Instantiate("Prefebs/Items/Knife", Room03);
                                RemoveText(J_knife.gameObject);
                                J_knife.transform.localPosition = new Vector3(10, 111, 0);
                                J_knife.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    GameManager.instance.PlaySound(ItemClickSound);
                                    AccquireItem(J_knife.GetComponent<ItemPickUp>().item);
                                    isGetKnife = true;
                                    Destroy(J_knife.gameObject, 0.1f);
                                });
                                //Debug.Log("==J_knife. button===" + J_knife.GetComponent<Button>().ToString());

                                isGetKnife = true;
                                //Į�� �ڸ��� �ִϸ��̼�

                                GameManager.instance.PlaySound(ItemClickSound);

                                return;
                            }
                            else if (toggleSpriteName == "key_inven")
                            {
                                slots[index].ClearSlot();
                                GameManager.instance.PlaySound(UnLockSound);
                                isGetKey = true;
                                //Key.gameObject.SetActive(true);

                                // �� �Ǵ� ���ڿ����� �ִϸ��̼�

                                //return;
                            }
                        }


                    });
                }

                if (J_ox.gameObject.activeSelf)
                {
                    OxArea.onClick.AddListener(() =>
                    {
                        if (isOxSelected == true)
                        {
                            UnSelectToggle();
                            isOxSelected = false;

                            if (toggleSpriteName == "Ox")
                            {
                                slots[index].ClearSlot();
                                //J_ox.localPosition = new Vector3(50, 10, 0);
                                isGetOx = true;
                                return;
                            }
                        }

                    });
                }


                if (ZoomArea.gameObject.activeSelf)
                {
                    ZoomArea.onClick.AddListener(() =>
                    {
                        GameManager.instance.PlaySound(SurpriseSound);
                        GameManager.instance.Fade();
                        GameManager.instance.DownArrow.gameObject.SetActive(true);
                        GameManager.instance.LeftArrow.gameObject.SetActive(false);
                        Debug.Log("00invent====roomTypeobj===" + GameManager.instance.roomTypeObj + "==localscale:" + GameManager.instance.roomTypeObj.transform.localScale.x);
                        StartCoroutine(zoomin());
                        Debug.Log("11invent====roomType===" + GameManager.instance.roomType + "==localscale:" + GameManager.instance.roomTypeObj.transform.localScale.x);
                        Debug.Log("GMroomType===" + GameManager.instance.Room00.name + "==localscale==" + GameManager.instance.Room00.transform.localScale.x);
                    });
                }

            }

        }
        IEnumerator zoomin()
        {
            yield return new WaitForSeconds(0.3f);
            GameManager.instance.Room00.localScale = new Vector3(5, 5, 1);
            GameManager.instance.Room00.localPosition = (new Vector3(110, 2000, 1));
            //.localPosition = new Vector3(0, 0, 1);
            ZoomArea.gameObject.SetActive(false);
            Room00.GetComponent<DragSystem>().enabled = false;
            yield return null;
            //Debug.Log("GMroomType===" + GameManager.instance.Room00.name + "==localscale==" + GameManager.instance.Room00.transform.localScale.x);
        }
        

        void OpenTheBox()
        {
            Debug.Log("=======openBox======");
        }
        /// <summary>
        /// ��۱׷� ���� ����� �ϳ��� ���� ������, if���� ����.
        /// ������ ��ۿ� �ش��ϴ� �������� �˻��Ͽ� �� ��ۺ� �ٸ��� �����ؾ��ϴµ�...........
        /// �� ����� 
        /// </summary>


        //�� ���Թ�ư�� �̺�Ʈ�� �ɾ��� �Լ�
        public void ColorChange(Toggle _toggle)
        {
            ColorBlock cb = _toggle.colors;

            if (_toggle.isOn)
            {
                cb.normalColor = selectedColor;
                cb.selectedColor = selectedColor;
                _toggle.colors = cb;
            }
            else
            {
                cb.normalColor = UnselectedColor;
                cb.selectedColor = UnselectedColor;
                _toggle.colors = cb;
                GameManager.instance.myText_item.text = "";
            }
        }
        public void CheckingSelected(int _slotNum)
        {
            if (InvenItemImg[_slotNum].sprite != null)
            {
                toggleSpriteName = InvenItemImg[_slotNum].sprite.name;
                Debug.Log("toggleSpriteName ==" + toggleSpriteName);

                

                //GameManager.instance.saveDataList.getImg(1, InvenItemImg[_slotNum]);
                //Debug.Log("saveDataList._currentSlotItemName===" + GameManager.instance.saveDataList._currentSlotItems[0]);

                index = _slotNum;
                Debug.Log("Index======" + index);
                if (toggleSpriteName == "Knife")
                {
                    GameManager.instance.myText_item.text = "Į";
                    Debug.Log("toggleSpriteName====" + toggleSpriteName);
                    isKnifeSelected = true;
                    
                    Debug.Log($"knifeis True====" + isKnifeSelected);
                }
                else if (toggleSpriteName == "Ox")
                {
                    isOxSelected = true;
                    GameManager.instance.myText_item.text = "����";
                }
                else if (toggleSpriteName == "key_inven")
                {
                    isKeySelected = true;
                    GameManager.instance.myText_item.text = "����";
                }
                else
                {
                    isKnifeSelected = false;
                    isOxSelected = false;
                    isKeySelected = false;

                }
            }
        }
    }
}