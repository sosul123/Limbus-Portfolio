using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // <-- 이벤트 시스템 사용을 위해 필수!

public class ButtonPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 인스펙터에서 변경할 TextMeshPro 오브젝트를 연결
    public TextMeshProUGUI targetText;
    Vector2 offset = new Vector2(0, -10);

    // 버튼을 누르는 순간 호출되는 함수
    public void OnPointerDown(PointerEventData eventData)
    {
        if (targetText != null)
        {
            Vector2 offset = new Vector2(0, -10); // 버튼을 누를 때 텍스트 위치를 아래로 이동할 오프셋
            targetText.rectTransform.anchoredPosition += offset;
        }
    }

    // 버튼에서 마우스를 떼는 순간 호출되는 함수
    public void OnPointerUp(PointerEventData eventData)
    {
        if (targetText != null)
        {
            // 저장해 둔 원래 텍스트로 복원
            targetText.rectTransform.anchoredPosition -= offset;
        }
    }
}
