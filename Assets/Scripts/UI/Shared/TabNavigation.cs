using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabNavigation : MonoBehaviour
{
    [SerializeField]
    private Selectable firstSelectableObject;
    private EventSystem system;

    private void Start()
    {
        system = EventSystem.current;
        if (firstSelectableObject != null)
        {
            firstSelectableObject.Select();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)
                ? system.currentSelectedGameObject?.GetComponent<InputField>().FindSelectableOnUp()
                : system.currentSelectedGameObject?.GetComponent<InputField>().FindSelectableOnDown();

            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
}
