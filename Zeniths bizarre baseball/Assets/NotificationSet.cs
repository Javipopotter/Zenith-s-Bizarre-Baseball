using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NotificationSet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image notificationIcon;
    public NotificationData[] notificationData;
    [SerializeField] InputActionReference continueAction;
    List<int> notifications = new List<int>();
    bool pass;

    private void Awake() => continueAction.action.performed += OnContinueActionPerformed;
    private void OnDestroy() => continueAction.action.performed -= OnContinueActionPerformed;

    private void OnEnable() => continueAction.action.Enable();
    private void OnDisable() {
        continueAction.action.Disable();
        pass = false;
    }

    public void Notify(int index)
    {
        notifications.Add(index);
        gameObject.SetActive(true);
        StartCoroutine(SetNotification(index));
    }

    IEnumerator SetNotification(int index)
    {
        // while(notifications.Count > 1) yield return null;

        title.text = notificationData[index].title;
        description.text = notificationData[index].description;
        notificationIcon.sprite = notificationData[index].icon;

        while(!pass) yield return null;

        // notifications.Remove(index);
        gameObject.SetActive(false);
    }

    void OnContinueActionPerformed(InputAction.CallbackContext ctx)
    {
        pass = true;
    }
}
