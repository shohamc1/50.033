using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    GREENMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory inventory;
    public Powerup redMush;
    public Powerup greenMush;
    public List<GameObject> powerupIcons;

    void Start()
    {
        if (!inventory.gameStarted)
        {
            inventory.gameStarted = true;
            inventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                Powerup p = inventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        inventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    public void AttemptConsumePowerup(KeyCode k)
    {
        Debug.Log(gameObject.activeInHierarchy);
        if (k == KeyCode.Z)
        {
            if (inventory.Get(0) != null)
            {
                marioMaxSpeed.SetValue(marioMaxSpeed.Value + 100);
                StartCoroutine(removeSpeedEffect());
                inventory.Remove(0);
                powerupIcons[0].SetActive(false);

            }
        }
        if (k == KeyCode.X)
        {
            if (inventory.Get(1) != null)
            {
                marioJumpSpeed.SetValue(marioJumpSpeed.Value + redMush.absoluteJumpBooster);
                StartCoroutine(removeJumpEffect());
                inventory.Remove(1);
                powerupIcons[1].SetActive(false);
            }
        }
    }


    IEnumerator removeJumpEffect()
    {
        yield return new WaitForSeconds(5.0f);
        // player.GetComponent<PlayerController>().maxSpeed  /=  2;
        marioJumpSpeed.SetValue(marioJumpSpeed.Value - redMush.absoluteJumpBooster);
    }
    IEnumerator removeSpeedEffect()
    {
        yield return new WaitForSeconds(5.0f);
        // player.GetComponent<PlayerController>().maxSpeed  /=  2;
        marioMaxSpeed.SetValue(marioMaxSpeed.Value - greenMush.aboluteSpeedBooster);
    }



    public void OnApplicationQuit()
    {
        resetPowerup();
    }

}