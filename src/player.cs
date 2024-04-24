class Player{

    public string showStatus(){
        string str = "your health is " + this.health;
        str += "\n backpack: \n" + this.backpack.showItem();
        return str;
    }

    public int Damage(int injury){
        this.health = this.health - injury;
        if(health < 0){
            this.health = 0;
        }
        return this.health;
    }
    public int Heal(int amount){
        this.health = this.health + amount;
        if(health > 100)
        {
            this.health = 100;
        }
        return this.health;
        
    }
    public bool IsAlive(){
        if(health < 1){
            return false;
        }
        else{
            return true;
        }
    }
    public Room CurrentRoom { get; set; }
    private int health;
    


    private Inventory backpack;
    // constructor
    public Player(){

        CurrentRoom = null;
        health = 100;

        // 25kg is pretty heavy to carry around all day.
        backpack = new Inventory(25);
    }

        // methods

    public void Use(string itemName){
        if(this.backpack.Get(itemName) == null)
        {
            Console.WriteLine("nuh-uhhh");
            return;
        }
        if(itemName == "medkit")
        {
            this.Heal(30);
            Console.WriteLine("you've used a medkit.");
        }
    }
    public bool TakeFromChest(string itemName)
    {
        if (CurrentRoom != null)
        {
            // Get the item from the room
            Item item = CurrentRoom.Chest.Get(itemName);
            if (item != null)
            {
                // Try to put the item in the backpack
                if (backpack.Put(itemName, item))
                {
                    Console.WriteLine("You took " + itemName + " from the chest and put it in your backpack.");
                    return true;
                }
                else
                {
                    // If the item doesn't fit in the backpack, put it back in the room
                    CurrentRoom.Chest.Put(itemName, item);
                    Console.WriteLine("You cannot carry " + itemName + " because it doesn't fit in your backpack.");
                }
            }
            else
            {
                Console.WriteLine("There is no " + itemName + " in the chest.");
            }
        }
        else
        {
            Console.WriteLine("There is no room to take items from.");
        }
        return false;
    }

    public bool DropToChest(string itemName)
    {
        if (CurrentRoom != null)
        {
            // Get the item from the backpack
            Item item = backpack.Get(itemName);
            if (item != null)
            {
                // Try to put the item in the room
                if (CurrentRoom.Chest.Put(itemName, item))
                {
                    Console.WriteLine("You dropped " + itemName + " to the chest.");
                    return true;
                }
                else
                {
                    // If the room cannot contain the item, put it back in the backpack
                    backpack.Put(itemName, item);
                    Console.WriteLine("You cannot drop " + itemName + " because the chest is full.");
                }
            }
            else
            {
                Console.WriteLine("You don't have " + itemName + " in your backpack.");
            }
        }
        else
        {
            Console.WriteLine("There is no room to drop items into.");
        }
        return false;
    }
}