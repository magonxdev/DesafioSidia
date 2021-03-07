using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    private AttributeType attributeType;

    //Setter para o tipo
    public void SetCollectableType(AttributeType attr)
    {
        attributeType = attr;
    }

    //Lógica que quando coleta o item
    public void ItemCollected(Player player)
    {

        if (attributeType == AttributeType.ItemAttack)
        {
            player.AttackPowerUp(1);
        } else if (attributeType == AttributeType.ItemHP)
        {
            player.HpPowerUp(2);
        }

        Destroy(this.gameObject);

    }


}
