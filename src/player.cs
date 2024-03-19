class Player{

    public string showStatus(){
        string str = "your health is " + this.health;
        return str;
    }

    public int Damage(int injury){
        this.health = this.health - injury;
        return this.health;
    }
    public int Heal(int amount){
        this.health = this.health + amount;
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
    
    public Player()
        {
            CurrentRoom = null;
            health = 100;
        }

}