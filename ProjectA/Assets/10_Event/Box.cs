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
    
    private List<Slot> boxInventorySlotList = new List<Slot>();

    [SerializeField]
    private Transform content;
    [SerializeField]
    private List<Item> boxContainItemList;

    public void Awake()
    {
        MakeBox();
    }

    public void FillBoxFirst()
    {
        for (int i = 0; i < boxContainItemList.Count; i++)
        {
            SetItmeInBox(boxContainItemList[i]);
        }
    }

    public void SetItmeInBox(Item itme)
    {
        for (int i = 0; i < boxInventorySlotList.Count; i++)
        {
            if (boxInventorySlotList[i].item == null)
            {
                boxInventorySlotList[i].SetItem(itme);
                break;
            }
        }
    }

    public void MakeBox()
    {
        for (int i = 0; i < boxSize; i++)
        {
            GameObject newSlot =  Instantiate(slotPrefab, content); //ΩΩ∑‘¿ª BoxSize ≈©±‚∏∏≈≠ ∏∏µÈæÓ¡‹
            boxInventorySlotList.Add(newSlot.GetComponent<Slot>());
        }

        boxInventoryCanvas.SetActive(false);
        FillBoxFirst();
    }

    public void OpenCloseBox()
    {
        if (!boxInventoryCanvas.activeSelf) //¥Ë«Ù¿÷¿Ω
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
