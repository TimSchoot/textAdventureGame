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
        // TODO implement:
        // Check the Weight of the Item and check for enough space in the Inventory. Does the Item fit?
        if(item.Weight > FreeWeight())
        {
            // Console.WriteLine("Your item doesn't fit in the backpack.");
            return false;
        }

        // Put Item in the items Dictionary
        this.items.Add(itemName, item);

        // Return true/false for success/failure
        return true;
    }
    public Item Get(string itemName){
        // TODO implement:
        // Find Item in items Dictionary. remove Item from items Dictionary if found

        if(!this.items.ContainsKey(itemName))
        {
            Console.WriteLine("Item doesn't excist in this current world.");
            return null;
        }

        // currentItem = this.items[itemName];

        // if (currentItem != null)
        // {
        //     this.items.Remove(itemName);
        //     return currentItem;
        // }

        // return Item or null 
        return null;
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
