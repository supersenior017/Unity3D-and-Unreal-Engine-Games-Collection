using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Upgrade_Pool : MonoBehaviour
{
    //get Sprites for the different Upgrades
    public Sprite Sprite_Upgrade_0;
    public Sprite Sprite_Upgrade_1;
    public Sprite Sprite_Upgrade_2;
    public Sprite Sprite_Upgrade_3;

    //get count of Souls
    public Text Souls;


    public void Upgrade()
    {
        Text txt = transform.Find("Ability(Text)").GetComponent<Text>(); //what text is written on the button?
        Image img = GetComponent<Image>(); //what is the image of the button?
        int Count_Souls = Int32.Parse(Souls.text); //Count of Souls from Text to int

        //check if any upgrade has been made and if enough Souls are available
        if (txt.text == "Pool (0)" && Count_Souls >= 1000)
        {
            Upgrade_1(txt, img, Count_Souls);
        }
        //check if Cloud Wall already has been upgraded once and if enough Souls are available
        else if (txt.text == "Pool (1)" && Count_Souls >= 2500)
        {
            Upgrade_2(txt, img, Count_Souls);
        }
        else if (txt.text == "Pool (2)" && Count_Souls >= 5000)
        {
            Upgrade_3(txt, img, Count_Souls);
        }
    }

    void Upgrade_1(Text txt, Image img, int Count_Souls)
    {
        int Souls_after_Upgrade;
        txt.text = "Pool (1)"; //set Text of Button to ...
        img.sprite = Sprite_Upgrade_1; //change image

        Souls_after_Upgrade = Count_Souls - 1000; //subtract 1000 from Count Souls
        Souls.text = Souls_after_Upgrade.ToString(); //and write it into the Textbox "Seelen"
    }

    void Upgrade_2(Text txt, Image img, int Count_Souls)
    {
        int Souls_after_Upgrade;
        txt.text = "Pool (2)"; //set Text of Button to ...
        img.sprite = Sprite_Upgrade_2; //change image

        Souls_after_Upgrade = Count_Souls - 2500;
        Souls.text = Souls_after_Upgrade.ToString();
    }

    void Upgrade_3(Text txt, Image img, int Count_Souls)
    {
        int Souls_after_Upgrade;
        txt.text = "Pool (3)"; //set Text of Button to ...
        img.sprite = Sprite_Upgrade_3; //change image

        Souls_after_Upgrade = Count_Souls - 5000;
        Souls.text = Souls_after_Upgrade.ToString();
    }

    public void Mouse_Enter_Button()
    {
        //If mouse is over the Button, find and enable the Button with the Info Text
        Image img = transform.Find("Info_Ability(Button)").GetComponent<Image>();
        Button btn = transform.Find("Info_Ability(Button)").GetComponent<Button>();
        Text txt = transform.Find("Info_Ability(Button)").Find("Info_Ability(Text)").GetComponent<Text>();

        img.enabled = true;
        btn.enabled = true;
        txt.enabled = true;
    }

    public void Mouse_Exit_Button()
    {
        //if mouse is not over the Button, find and disable the Button with the Info Text
        Image img = transform.Find("Info_Ability(Button)").GetComponent<Image>();
        Button btn = transform.Find("Info_Ability(Button)").GetComponent<Button>();
        Text txt = transform.Find("Info_Ability(Button)").Find("Info_Ability(Text)").GetComponent<Text>();

        img.enabled = false;
        btn.enabled = false;
        txt.enabled = false;
    }
}
