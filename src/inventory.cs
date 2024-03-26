using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;



class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    public bool Put(string itemName, Item item)
    {
        // Preconditions
        Contract.Requires(!string.IsNullOrEmpty(itemName));
        Contract.Requires(item != null);

        // Check if the item already exists
        if (items.ContainsKey(itemName))
            return false; // Item with the same name already exists

        // Check if adding this item will exceed the maximum weight
        int totalWeight = TotalWeight() + item.Weight;
        if (totalWeight > maxWeight)
            return false; // Adding this item will exceed the maximum weight

        // Add the item to the inventory
        items[itemName] = item;

        // Postconditions
        Contract.Ensures(Contract.Result<bool>() == (items.ContainsKey(itemName)));

        return true;
    }

    public Item Get(string itemName)
    {
        // Preconditions
        Contract.Requires(!string.IsNullOrEmpty(itemName));

        // Check if the item exists in the inventory
        if (items.TryGetValue(itemName, out Item item))
        {
            // Remove the item from the inventory
            items.Remove(itemName);
            return item;
        }

        // Postconditions
        Contract.Ensures(Contract.Result<Item>() == null || !items.ContainsKey(itemName));

        return null; // Item not found
    }

    public int TotalWeight()
    {
        // Sum up the weights of all items in the inventory
        int total = items.Values.Sum(i => i.Weight);
        return total;
    }

    public int FreeWeight()
    {
        // Calculate the remaining weight that can be added to the inventory
        int freeWeight = maxWeight - TotalWeight();
        return freeWeight;
    }
}
