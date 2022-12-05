using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image skillImage = null;
    public Skill skill = null;

    private void Awake()
    {
        skillImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetItem(Skill _skill)
    {
        //아이템을 슬롯에 추가해주는함수
        skill = Instantiate(_skill, transform);
        skillImage.gameObject.SetActive(true);
        skillImage.sprite = _skill.skillSprite;
    }

    public virtual void OnClick()
    {
        Inventory.Instance.SkillExplanation(skill);
    }
}
