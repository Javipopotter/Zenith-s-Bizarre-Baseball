using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NotificationData", menuName = "Zeniths bizarre baseball/NotificationData", order = 0)]
public class NotificationData : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
}
