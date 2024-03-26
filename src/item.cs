class Item{
	public int Weight { get; set; }
	public string Description { get; set; }
			
	// constructor
	public Item(int weight, string description){
		Weight = weight;
		Description = description;
	}
}