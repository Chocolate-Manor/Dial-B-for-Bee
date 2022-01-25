using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText; 
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private Animator animator;

    public Animator getAnimator()
    {
        return animator;
    }

    public TextMeshProUGUI getCountText()
    {
        return countText;
    }

    public Image getSelectedItemImage()
    {
        return selectedItemImage;
    }



}
