using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    // 아이템이 잘 로드되었는지 확인하기 위한 테스트용 셀 클래스입니다.

    public class ItemInfoCell : MonoBehaviour
    {
        public Image icon;
        public Text itemName;
        public Text description;

        public void Initialize(ItemAsset itemAsset)
        {
            icon.sprite = itemAsset.icon;
            itemName.text = itemAsset.itemName;
            description.text = itemAsset.description;
        }
    }
}