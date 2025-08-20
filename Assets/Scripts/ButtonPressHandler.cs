using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // <-- �̺�Ʈ �ý��� ����� ���� �ʼ�!

public class ButtonPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // �ν����Ϳ��� ������ TextMeshPro ������Ʈ�� ����
    public TextMeshProUGUI targetText;
    Vector2 offset = new Vector2(0, -10);

    // ��ư�� ������ ���� ȣ��Ǵ� �Լ�
    public void OnPointerDown(PointerEventData eventData)
    {
        if (targetText != null)
        {
            Vector2 offset = new Vector2(0, -10); // ��ư�� ���� �� �ؽ�Ʈ ��ġ�� �Ʒ��� �̵��� ������
            targetText.rectTransform.anchoredPosition += offset;
        }
    }

    // ��ư���� ���콺�� ���� ���� ȣ��Ǵ� �Լ�
    public void OnPointerUp(PointerEventData eventData)
    {
        if (targetText != null)
        {
            // ������ �� ���� �ؽ�Ʈ�� ����
            targetText.rectTransform.anchoredPosition -= offset;
        }
    }
}
