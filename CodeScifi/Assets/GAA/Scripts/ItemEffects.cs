using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Switch of effects or just a selection of effects
*/

public class ItemEffects : MonoBehaviour {

    PlayerAttributesScript targetAttributes;
    Transform parent;


    void Awake() {
        parent = transform.parent;
        targetAttributes = parent.GetComponentInChildren<PlayerAttributesScript>();
        //targetAttributes = GetComponent<PlayerAttributesScript>();
    }

    public bool RequestEffect(Item item) {
        switch (item.Effect) {//item.Effect can be Heal, Buff, etc
            case "Heal":
                return (Heal(item.Vitality));
            case "Charge":
                return (Charge(item.Vitality));
            case "Flux":
                return (Flux(item.Vitality));
            default:
                return false;
        }


    }

    bool Heal(int amount) {
        if (targetAttributes.GetCurrentHealth() == targetAttributes.GetMaxHealth())//if already maxed health
        {
            return false;
        }
        else {
            targetAttributes.PlayerRecoverHealth(amount);
            return true;
        }
    }

    bool Charge(int amount) {
        if (targetAttributes.GetCurrentShield() == targetAttributes.GetMaxShield())//if already maxed health
        {
            return false;
        }
        else {
            targetAttributes.PlayerRecoverShield(amount);
            return true;
        }
    }

    bool Flux(int amount) {
        if (targetAttributes.GetCurrentEnergy() == targetAttributes.GetMaxEnergy())//if already maxed health
        {
            return false;
        }
        else {
            targetAttributes.PlayerRecoverEnergy(amount);
            return true;
        }
    }


//
	
}
