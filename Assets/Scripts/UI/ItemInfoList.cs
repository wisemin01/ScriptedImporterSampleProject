using UnityEngine;

namespace Assets.Scripts.UI
{
    // �������� �� �ε�Ǿ����� Ȯ���ϱ� ���� �׽�Ʈ�� UI ��� Ŭ�����Դϴ�.

    public class ItemInfoList : MonoBehaviour
    {
        public GameObject cellPrefab;
        public RectTransform content;

        void Start()
        {
            // Loading Sample

            var armors = Resources.Load<ItemTableAsset>("Tables/Item.Armors");
            foreach (var itemAsset in armors.items)
            {
                var go = Instantiate(cellPrefab, content, false);

                go.GetComponent<ItemInfoCell>().Initialize(itemAsset);
                go.SetActive(true);
            }

            var weapons = Resources.Load<ItemTableAsset>("Tables/Item.Weapons");
            foreach (var itemAsset in weapons.items)
            {
                var go = Instantiate(cellPrefab, content, false);

                go.GetComponent<ItemInfoCell>().Initialize(itemAsset);
                go.SetActive(true);
            }

            var potions = Resources.Load<ItemTableAsset>("Tables/Item.Potions");
            foreach (var itemAsset in potions.items)
            {
                var go = Instantiate(cellPrefab, content, false);

                go.GetComponent<ItemInfoCell>().Initialize(itemAsset);
                go.SetActive(true);
            }
        }
    }
}