using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopScript : MonoBehaviour
{
    public GameObject shopPanel;
    public int selectedItem, Cost_of_selectedItem;
    private PlayerScripts _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<PlayerScripts>();
            if (_player != null)
            {
                UIManager._instance.OpenShop(_player.diamonds);
                SelectedItem(0);
            }
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectedItem(int itemNum)
    {
        switch (itemNum)
        {
            case 0:
                UIManager._instance.SeletionUIPos(-71);
                selectedItem = 0;
                Cost_of_selectedItem = 200;
                break;
            case 1:
                UIManager._instance.SeletionUIPos(-170);
                selectedItem = 1;
                Cost_of_selectedItem = 400;
                break;
            case 2:
                UIManager._instance.SeletionUIPos(-261);
                selectedItem = 2;
                Cost_of_selectedItem = 100;
                break;
        }
    }
    public void BuyItem()
    {
        if (_player.diamonds >= Cost_of_selectedItem)
        {
            PurchasedItems();
        }
        else
        {
            Debug.Log("You Have not enough Diamond Germs to Buy Item");
        }
    }

    bool isFireSword, isHighJump;

    void PurchasedItems()
    {

        isFireSword = UIManager._instance.Amount[0].text != "Purchased" ? true : false;
        isHighJump = UIManager._instance.Amount[1].text != "Purchased" ? true : false;

        if (selectedItem == 2)
        {
            GameManager._instance.hasKey = true;
            DecreseDiamond();
        }
        if (selectedItem == 1 && isHighJump == true)
        {
            _player.jumpSpeed += 2.0f;
            UIManager._instance.Amount[1].text = "Purchased";
            DecreseDiamond();
        }
        if (selectedItem == 0 && isFireSword == true)
        {
            PlayerScripts.isFire = true;
            UIManager._instance.Amount[0].text = "Purchased";
            DecreseDiamond();
        }
    }
    void DecreseDiamond()
    {
        _player.diamonds -= Cost_of_selectedItem;
    }
}
