using System;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [ScriptedImporter(version: 1, exts: new string[] { }, overrideExts: new string[] { "xml" })]
    public sealed class ItemTableAssetImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            try
            {
                // @NOTE:
                //   ScriptableObject를 생성하고 ctx.AddObjectToAsset으로 에셋을 등록합니다.
                //   

                var tableAsset = ScriptableObject.CreateInstance<ItemTableAsset>();
                tableAsset.items = new System.Collections.Generic.List<ItemAsset>();

                XDocument document = XDocument.Load(ctx.assetPath);
                foreach (var element in document.Root.Elements("Item"))
                {
                    var itemAsset = new ItemAsset();

                    itemAsset.id = XmlHelper.GetAttributeInt(element, "ID");
                    itemAsset.type = XmlHelper.GetChildEnum<ItemType>(element, "Type");
                    itemAsset.itemName = XmlHelper.GetChildString(element, "Name");
                    itemAsset.description = XmlHelper.GetChildString(element, "Description");

                    var iconPath = XmlHelper.GetChildString(element, "Icon");
                    if (!string.IsNullOrEmpty(iconPath))
                    {
                        itemAsset.icon = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
                    }

                    tableAsset.items.Add(itemAsset);
                }

                ctx.AddObjectToAsset("table", tableAsset);
                ctx.SetMainObject(tableAsset);
            }
            catch (Exception e)
            {
                ctx.LogImportError(e.ToString());
            }
        }
    }
}