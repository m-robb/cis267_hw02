using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject dialogPanel;
    private bool nearNPC;
    private PlayerHubController player;
    // Update is called once per frame
    void Update()
    {
        if (nearNPC == true && Input.GetAxis("Jump") > 0)
        {
            dialogPanel.SetActive(true);
        }
        if (nearNPC == false)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = true;
            player = gameObject.GetComponent<PlayerHubController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nearNPC = false;
        }
    }

    public void appleBtn()
    {
        if (player.apple > 0)
        { 
            player.apple--;
            player.gold++;
        }
    }

    public void breadBtn()
    {
        if (player.bread > 0)
        {
            player.bread--;
            player.gold = player.gold + 5;
        }
    }
    public void woodBtn()
    {
        if (player.wood > 0)
        {
            player.wood--;
            player.gold++;
        }
    }
}
