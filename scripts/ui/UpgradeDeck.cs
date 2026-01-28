using Godot;
using System;

public partial class UpgradeDeck : CanvasLayer
{
	[Export] int DeckSize = 3;
	[Export] private PackedScene[] chooseCards;
	[Export] HBoxContainer Deck;
	private ChooseCard[] currentDeck;
	private RandomNumberGenerator rng;
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		rng.Randomize();

		for (int i = 0; i < DeckSize; i++)
		{
			throw new NotImplementedException();
		}
	}

}
