using UnityEngine;

namespace Assets.Scripts.UI
{
    // 아이템이 잘 로드되었는지 확인하기 위한 테스트용 UI 목록 클래스입니다.

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