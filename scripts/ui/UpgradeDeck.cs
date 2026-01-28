using Godot;
using System;

public partial class UpgradeDeck : CanvasLayer
{
	[Export] int DeckSize = 3;
	[Export] private PackedScene[] chooseCards;
	[Export] HBoxContainer Deck;
	private ChooseCard[] currentDeck;
	private RandomNumberGenerator rng;
	private Unit Player;
	private FirstMap firstMap;
	public override void _Ready()
	{
		Player = GetTree().GetFirstNodeInGroup("player") as Unit;
		firstMap = GetTree().GetFirstNodeInGroup("map") as FirstMap;
		rng = new RandomNumberGenerator();
		rng.Randomize();
		ShuffleDeck();
	}

	public void ShuffleDeck()
	{
		ClearDeck();
		for (int i = 0; i < DeckSize; i++) {
			var randomIndex = rng.RandiRange(0, chooseCards.Length - 1);
			var cardScene = chooseCards[randomIndex];
            
			if (cardScene.Instantiate() is not ChooseCard cardInstance) continue;
			Deck.AddChild(cardInstance);
			cardInstance.GuiInput += (ev) => HandleCardInput(ev, cardInstance);
		}
	}
	
	private void HandleCardInput(InputEvent @event, ChooseCard card)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
			OnPick(card);
	}

	void OnPick(ChooseCard card)
	{
		var txt = card.GetNode<TextureRect>("VBoxContainer/Texture").Texture;
		var face = new Sprite2D() {
			Texture = txt,
			Offset = new Vector2(-1, -32),
			Position = new Vector2(0, 1) };
		face.AddToGroup("mask");
		Player.AddChild(face);
		Player.AddResource(card.upgradeResource);
		firstMap.mobAmplifier.Concat(card.downgradeResource);
		ClearDeck();

		currentDeck = null;
		Visible = false;
	}
	
	private void ClearDeck()
	{
		foreach (var child in Deck.GetChildren())
			child.QueueFree();
		
	}

}
