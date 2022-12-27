using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public static Box lastBox;

    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private Sprite originalSprite;

    [SerializeField]
    private GameObject boxInventoryCanvas;
    
    [SerializeField]
    private int boxSize;
    [SerializeField]
    private GameObject slotPrefab;

    private List<Slot> boxInventorySlotList;

    [SerializeField]
    private Transform content;
    [SerializeField]
    private List<Item> boxContainItemList;

    public void Awake()
    {
        MakeBox();
    }

    public void MakeBox()
    {
        for (int i = 0; i < boxSize; i++)
        {
            GameObject newSlot =  Instantiate(slotPrefab, content); //슬롯을 BoxSize 크기만큼 만들어줌

            if (boxContainItemList.Count >= i + 1)
            {
                newSlot.GetComponent<Slot>().item = boxContainItemList[i]; //애초에 가지고 있는 아이템을 저장
            }
        }

        boxInventoryCanvas.SetActive(false);
    }

    public void OpenCloseBox()
    {
        if (!boxInventoryCanvas.activeSelf) //닺혀있음
        {
            OpenBox();
        }
        else
        {
            CloseBox();
        }
    }

    public void OpenBox()
    {
        lastBox = this;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        boxInventoryCanvas.SetActive(!boxInventoryCanvas.activeSelf);
        GameManager.Instance.isOpenBox = boxInventoryCanvas.activeSelf;
    }

    public void CloseBox()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        boxInventoryCanvas.SetActive(!boxInventoryCanvas.activeSelf);
        GameManager.Instance.isOpenBox = boxInventoryCanvas.activeSelf;
    }

}
