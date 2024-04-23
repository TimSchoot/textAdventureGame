using System;
using System.Security;
using System.Security.AccessControl;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room theatreAttic = new Room("in the attic of this theatre");
		Room pub = new Room("in the campus pub");
		Room bedroom = new Room("in your bedroom");
		Room closet = new Room("looking in the closet");
		Room lab = new Room("in a computing lab");
		Room garden = new Room("you are in the school garden");
		Room office = new Room("in the computing admin office");
		Room kitchen = new Room("you are in the kitchen");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddExit("up", theatreAttic);

		pub.AddExit("east", outside);
		pub.AddExit("up", bedroom);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);
		lab.AddExit("west", garden);

		office.AddExit("west", lab);
		office.AddExit("north", kitchen);

		bedroom.AddExit("down", pub);
		bedroom.AddExit("north", closet);

		closet.AddExit("back", bedroom);

		kitchen.AddExit("south", office);

		garden.AddExit("east", lab);

		theatreAttic.AddExit("down", theatre);

		// Create your Items here
		
		Item sword = new Item(6, "a weapon in the form of a rusty sword");
		Item bible = new Item(2, "holy protection in the form of a bible");
		Item key = new Item(1, "a key");
		Item beer = new Item(3, "a beverage in the form of some beer");
		Item medkit = new Item(3, "healing in the form of a medkit");
		Item kfcBucket = new Item(4, "some food in the form of a kfc bucket");


		// And add them to the Rooms
		bedroom.Chest.Put("Key", key);
		kitchen.Chest.Put("sword", sword);
		lab.Chest.Put("bible", bible);
		pub.Chest.Put("beer", beer);
		office.Chest.Put("medkit", medkit);
		theatre.Chest.Put("kfc bucket", kfcBucket);		

		// Start game outside(Nuh-uhhh)
		player.CurrentRoom = pub;
	}

	//  Main play routine. Loops until end of play.
public void Play()
{
    PrintWelcome();

    // Enter the main command loop. Here we repeatedly read commands and
    // execute them until the player wants to quit or dies.
    bool finished = false;
    while (!finished && player.IsAlive()) // Check if the player is alive before processing each command
    {
        Command command = parser.GetCommand();
        finished = ProcessCommand(command);
    }

    if (!player.IsAlive())
    {
        Console.WriteLine("You died!"); // Inform the player that they have died
    }

    Console.WriteLine("Thank you for playing.");
    Console.WriteLine("Press [Enter] to continue.");
    Console.ReadLine();
}



	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'kutzooi' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}


	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "kutzooi":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "status":
				Console.WriteLine(player.showStatus());
				break;
			case "look":
				Look();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.Damage(5);
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
	private void Look(){
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
	private void Take(Command command){
		if(!command.HasSecondWord()){
			Console.WriteLine("what do you want to pick up");
			return;
		}
		player.TakeFromChest(command.SecondWord);
	}
	private void Drop(Command command){
		if(!command.HasSecondWord()){
			Console.WriteLine("what do you want to drop");
			return;
			}
		player.DropToChest(command.SecondWord);
	}
}
