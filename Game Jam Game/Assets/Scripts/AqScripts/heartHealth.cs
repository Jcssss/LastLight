using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartHealth : MonoBehaviour
{       
    public Sprite fullHeart, noHeart;
    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartStatus(heartStatus status)
    {
        switch (status)
        {
            case heartStatus.Empty:
                heartImage.sprite = noHeart;
                break;
            case heartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }
}
public enum heartStatus
{
    Empty = 0,
    Full  = 1
}