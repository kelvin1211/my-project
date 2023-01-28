using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jason;
using System;


namespace Kelvin
{
    public class CheckRoomRay : MonoBehaviour
    {
        public static Action checkRoomRay;

        //�浹ü ���� ����
        RaycastHit hitInfo;

        //�ش� ���̾�� �����ϵ��� ���̾��ũ ����
        [SerializeField]
        private LayerMask layerMask;

        //�����ɽ�Ʈ �ִ� �Ÿ�
        [SerializeField]
        private float range = 500f;

        //���� ���� ���� �� �� true
        private bool pickUpActivated = false;


        public string roomName_Raycast;
        public GameObject roomObj_Raycast;

        private void Awake()
        {
            //�������� �Ѱ��� �Լ�
            checkRoomRay = () =>
            {
                GetRayInfoRoom();
                //roomObj_Raycast;

            };
        }

        private void Update()
        {
            GetRayInfoRoom();
            GameManager.instance.CheckRayToGM(roomName_Raycast, roomObj_Raycast);
        }

        //Ray�� ��� �ش� ��ü�� �̸�(RoomŸ��)�� roomName�� ����
        public void GetRayInfoRoom()
        {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask);


            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);

            roomName_Raycast = hitInfo.transform.GetComponent<RoomData>().myRoomData.RoomName;
            roomObj_Raycast = hitInfo.transform.GetComponent<RoomData>().myRoomData.RoomObject;
            //Debug.Log("name==" + hitInfo.collider.name);
        }

        void RoomRayInfoAppear()
        {
            pickUpActivated = true;
        }

        void infoDisappear()
        {
            pickUpActivated = false;
        }
    }
}