using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;

    public int healthModifier;
    public int damageModifier; 
}

public enum EquipmentSlot {hands, head}
