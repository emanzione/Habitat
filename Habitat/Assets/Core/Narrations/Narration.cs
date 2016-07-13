using System;
using System.Collections.Generic;
using UnityEngine;

public class Narration
{
	public static string NARRATOR_NAME = "Scientist Macro";
	protected static Dictionary<string, string> narrations = new Dictionary<string, string>(){
		{"INTRODUCTION_01", "Hey, little buddy! Use the slider with your mouse to keep listening to my ramblings or skip them all by furiously tapping on the ENTER key.\nAlright, you are sentient! I was waiting for this moment! I'm soooo excited!\nYou are the first specimen of a new race! How do you feel? Happy? Excited?\nI wonder...do you even feel?"},
		{"INTRODUCTION_02", "Oh, who cares! Let's move on!\nMy name is " + NARRATOR_NAME + " and I am a brilliant scientist. In our laboratory we created a little synthetic world and populated it with a few new organisms... some weird stuff like you basically.\nWe are participating in a SCIENCE SHOWDOWN and this is our project! My colleagues have their own specimens and you are mine...how cool is that?!\nNow, I should do my best to guide you through your journey but I am already ahead on my drinking schedule for today and I don't really feel like I care...\nJust try not to be a disappointment and win this competition for me, would you?"},
		{"INTRODUCTION_03", "You can do this, you just have to grow! Yeah, you understood correctly (which is surprising enough...). Try to eat something within the environment to grow your mass.\nOnce big enough, you might also want to try and eat other specimens to earn DNA POINTS!"},
		{"INTRODUCTION_04", "Move in all directions with WASD, holding your right mouse button will also help you look around. Use the mouse scroll to zoom out for a clear view of your surroundings and remember... if the controls feel weird and uncomfortable it's because YOU ARE BAD.\nNow go! Fulfill your destiny! Get big or die trying! (Damn, this thing is so stupid...it needs more tutoring than the average Ubi customer...)"},
		{"INTRODUCTION_05", "Sooooo where were we? Oh right, the top bar indicates your stomach's capacity. Don't worry if it fills up rapidly: your metabolism (Thanks to my genetic engineering) is incredibly fast and it allows you to properly exploit all the calories you assimilate! (I wonder if this thing can decipher any of the words I speak... it hopefully has some sort of brain functions...)"},
		{"INTRODUCTION_06", "The other bar indicates your acquired calories: they will be digested so you can increase your mass. Go on, gorge yourself on all you can find! (I'm not paying anyway...)"},

		{"FIRST_FOOD_EAT", "Oooh good job boyo, very nice! You ate your first artificially created pseudo-synthetic food! Look at your bars now: those calories will be digested soon!"},
		{"FIRST_POISONED_FOOD_EAT", "Oh nooo, for the great Lord I DO NOT believe in... Do not eat that crap! That is poisonous synthetic food! (Great... full marks on the stupidity test...)\nThose are purple mushrooms, PURPLE! Who in the universe eats purple? Isn’t it the third law of nature or something? Kids learn this before walking:\n\"If the color is bright is NOT a delight\nyou'll lay on your bed and you'll wish you were dead!\"\nMy colleagues placed those purple 'shrooms around to slow down your evolution!\nDon't eat them again! They might fill your stomach, but they will decrease your calories!"},
		{"FIRST_ENEMY_SPOTTED_01", "Hey, listen pal! I spotted an enemy organism near you... Those creatures have been created by my lab mates, they will eat you to evolve themselves!"},
		{"FIRST_ENEMY_SPOTTED_02", "That should not happen, you hear me? You have to eat them first!\nJust remember: you can only eat specimens with less mass than you. Now, go devour a small one and make me proud!"},
		{"FIRST_ENEMY_SPOTTED_03", "When you eat an enemy organism, you will receive DNA POINTS! Stack them up to prove you are the best specimen around here! Our target is " + Settings.DNA_POINTS_TO_WIN + " DNA POINTS. Now go get them! Let's remind everyone who is the GENIUS around here!"},

		{"FIRST_ENEMY_EATEN", "Tasty, isn't it? Do you like the feeling? Did you thoroughly enjoy eating a lesser being for the sake of your own meaningless survival within a small, mediocre and overall irrelevant universe?\nYeah.. you get it now... that's the meaning of life in all its dumb magnificence.\nBut you have been lollygagging enough. Step it up now! You might be worth something after all..."},

		{"GAME_COMPLETED_1", "Wow, you made it! I knew it! My specimen is the best! Well done, my little friend! That proves you are not just a waste of tridimensional space and I am a freaking GENIUS!!!"},
		{"GAME_COMPLETED_2", "*Yo dudes! YO... Yeah you morons in the lab coat, my specimen just finished dominating yours! Ahahahhahahaha! In your FACE!*"},
		{"GAME_COMPLETED_3", "And you, I suppose I should thank you... oh well, you can continue to explore the world around you as a prize... go on, eat it all up... see if I care!"},
		{"GAME_COMPLETED_4", "*Yo losers what's with the long face? Aw come on, don't be sad! Let's go to Dirty Harry I'll buy you guys a beer!*"},
		{"GAME_COMPLETED_5", "*CLUNK*"},

		{"GAMEOVER_1", "Oh, great job! You proved yourself to be bad at literally the only thing your genetics enables you to do, it surely require skills..."},
		{"GAMEOVER_2", "Mhm it seems you can't survive that long... Perhaps my calculations are wrong... or maybe... you just SUCK AT BEING ALIVE, YOU WORTHLESS WASTE OF CUBIC MICRONS! Alright, I need a drink... Where is my scotch?"},
		{"GAMEOVER_3", "OH NO!  For the love of Mendeleev, HOW BAD ARE YOU? You just allowed Gilbert to win this competition! Seriously though, this cannot happen. That guy has an IQ of only 159... he is basically THE SMARTEST AMONG THE MENTALLY CHALLENGED!"},
		{"GAMEOVER_4", "You are not working properly... I should probably add some new molecules to your configuration... perhaps SOMETHING LIKE A BRAIN!"},
		{"GAMEOVER_5", "*Yo guys stop it! Someone is cheating here! We agreed, NO INVINCIBLE SPECIMENS!!!... Mine WHAT? What did you just say?... WELL, YOUR GRANDMA DOES THAT TOO!... Oh no? ASK GILBERT THEN, IT WAS HIS FINEST CATCH!*"},

		{"FIRST_PAUSE", "Do you need a break already? Tell me then, would you also like a drink? A croissant? A massage maybe?\nGet back here and dominate this habitat before I genetically reconfigure your ass!"},
		{"PAUSE_1", "What? A PAUSE AGAIN? What the hell else you might have of such importance in your life? I’ll tell you what, NOTHING!\nNow get back here, we got to win at SCIENCE!"},
		{"PAUSE_2", "You got to be kidding me!\nYou know... if I were YOU I’d ask ME to check your bladder genetic configuration since it appears to be SERIOUSLY MESSED UP!"},

	};

	public static string GetNarration(string key)
	{
		return (narrations.ContainsKey(key)) ? narrations[key] : "";
	}


	public string Who;
	public string Text;
	public float Duration;
	public DateTime Expiration;
	public bool DisappearIfEnterPressed;
	public bool FreezePlayer;
	public Action Callback;

	public Narration(string who, string text, float duration, bool disappearOnEnterPressed, bool freeze, Action callback = null)
	{
		Who = who;
		Text = text;
		Duration = duration;
		Expiration = DateTime.UtcNow.AddSeconds(duration);
		Callback = callback;
		DisappearIfEnterPressed = disappearOnEnterPressed;
		FreezePlayer = freeze;
	}

	public void Activate()
	{
		Expiration = DateTime.UtcNow.AddSeconds(Duration);
		if (Callback != null) {
			Callback.Invoke ();
		}
	}
}

