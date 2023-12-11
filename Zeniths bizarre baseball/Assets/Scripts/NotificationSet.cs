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
    List<int> notifications = new List<int>();
    bool nextTrigger;

    public void Notify(int index)
    {
        notifications.Add(index);
        gameObject.SetActive(true);
        StartCoroutine(SetNotification());
    }

    IEnumerator SetNotification()
    {

        foreach(int index in notifications)
        {
            title.text = notificationData[index].title;
            description.text = notificationData[index].description;
            notificationIcon.sprite = notificationData[index].icon;
            notifications.Remove(index);

            while(!nextTrigger){yield return null;}
            nextTrigger = false;
        }

        gameObject.SetActive(false);
    }

    public void NextNotification()
    {
        nextTrigger = true; 
    }
}
