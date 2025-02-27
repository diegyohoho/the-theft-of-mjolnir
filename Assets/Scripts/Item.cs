﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Item : MonoBehaviour {
    public ItemType type;
    public int sortingOrder;
    public ItemData currentItemData;

    [HideInInspector]
    public SpriteRenderer spr;

    void Awake() {
        spr = GetComponent<SpriteRenderer>();
    }

    public void SetCurrentData(ItemData itemData) {
        currentItemData = itemData;
        if(currentItemData != null) {
            transform.localPosition = currentItemData.spritePosition;
            spr.sprite = currentItemData.sprite;
            spr.sortingOrder = sortingOrder +
                                    currentItemData.sortingOrder; 
        } else {
            transform.localPosition = Vector2.zero;
            spr.sprite = null;
            spr.sortingOrder = sortingOrder; 
        }
    }

#if UNITY_EDITOR

    public void UpdateCurrentData() {
        if(!spr.sprite) {
            currentItemData = null;
        } else {
            SetCurrentData(GameData.GetItemData(spr.sprite));
        }
    }

    public void SaveChanges() {
        if(!spr)
            spr = GetComponent<SpriteRenderer>();

        if(!spr.sprite) {
            Debug.LogWarning("Assign a sprite!");
            return;
        }

        ItemData currentChange = new ItemData(
            spr.sprite.name,
            type,
            spr.sprite,
            transform.localPosition
        );

        currentItemData = GameData.SetItemData(currentChange);
    }
#endif

}
