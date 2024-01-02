using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    // 아이템 Type
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
    }

    // 아이템 Asset
    [Serializable]
    public class ItemAsset
    {
        public int id;
        public ItemType type;
        public string itemName;
        public string description;
        public Sprite icon;
    }

    // 아이템 Table (데이터 파일 1개와 1:1 매칭)
    [Serializable]
    [PreferBinarySerialization]
    public sealed class ItemTableAsset : ScriptableObject
    {
        public List<ItemAsset> items;
    }
}