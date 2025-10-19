using System.Numerics;
using System.Security.Cryptography;

int level = 10;

Console.WriteLine("Welcome to Guess A Number!");

string CampaignOrSettings()
{
	while (true)
	{
		Console.WriteLine("Would you like to play Campaign or with Settings? (\"c\", \"s\")");
		string choice = Console.ReadLine().ToLower();
		if (choice == "c" || choice == "s")
		{
			return choice;
		}
		else
		{
			Console.WriteLine("Invalid input. Please enter 'c' for Campaign or 's' for Settings.");
		}
	}
}
string Game()
{

	return "This is a simple number guessing game.";
}

(string difficulty, int attempts) DifficultyLevel()
{
	Console.WriteLine("Select difficulty level: Easy, Medium, Hard");
	string difficulty = Console.ReadLine();
	Console.WriteLine($"You selected {difficulty} difficulty.");
	Console.WriteLine("Select amount of attempts");
	while (true)
	{
		string attemptsInput = Console.ReadLine();
		bool isValid = int.TryParse(attemptsInput, out int attempts);
		if (isValid)
		{
			Console.WriteLine($"You have {attempts} attempts.");
			return (difficulty, attempts);
		}
		else
		{
			Console.WriteLine("Invalid input. Please enter a valid number for attempts.");
		}
	}
}


long RandomNumber()
{
	Random random = new Random();
	return random.NextInt64(1, Range() + 1);
}

long Range()
{
	if (level == 100) return long.MaxValue - 1; // final boss

	// Smooth exponential growth: start = 100, scale = 1.15 per level
	double start = 100;
	double growth = 1.3; // adjust for how fast you want it to grow
	return (long)(start * Math.Pow(growth, level - 1));
}








long Guessing()
{
	while (true)
	{
		Console.Write("Enter your guess: ");
		string input = Console.ReadLine();
		bool isValid = long.TryParse(input, out long guess);
		if (isValid)
		{
			if (guess < 1)
			{
				Console.WriteLine("Your guess is too low. Please enter a number greater than or equal to 1.");
			}
			else if (guess > Range())
			{
				Console.WriteLine($"Your guess is too high. Please enter a number less than or equal to {Range()}.");
			}
			else
			{
				return guess;
			}
		}
		else
		{
					Console.WriteLine("Invalid input. Please enter a valid number.");
		}
	}
}
long HigherOrLower(long targetNumber,int maxGuesses)
{
	if (maxGuesses != int.MaxValue)
	{
		Console.WriteLine($"You have {maxGuesses} attempts to guess the number.");
		Console.WriteLine($"The chance to win is {WinProbability(Range(), maxGuesses)}%");
	}
	int limitedAttempts = maxGuesses;
	int attempts = 0;

	while (true)
	{
		long guess = Guessing();
		if (guess < targetNumber)
		{
			Console.WriteLine("Higher!");
			attempts++;
		}
		else if (guess > targetNumber)
		{
			Console.WriteLine("Lower!");
			attempts++;
		}

		else
		{
			attempts++;
			Console.WriteLine($"Correct! You've guessed the number! in {attempts} attempts");
			return LevelUp();
		}

		if (attempts == limitedAttempts)
		{
			Console.WriteLine($"Out of attempts! The correct number was {targetNumber}.");
			return -1;
		}
		if (limitedAttempts - attempts <= 3)
		{
			Console.WriteLine($"You have {limitedAttempts - attempts} attempts left.");
		}

	}
}

double WinProbability(long range, int guesses)
{
	double covered = (long)Math.Pow(2, guesses);
	double probability = covered / range;
	return Math.Min((probability * 100), 100);
}

int LevelUp()
{
	level++;
	Console.WriteLine($"Congratulations! You've advanced to level {level}!");
	return level;
}


int LimitAttempts(int level)
{
	switch (level)
	{
		case 10:
			return 9;
		case 20:
			return 12;
		case 30:
			return 10;
		case 40:
			return 8;
		case 50:
			return 6;
		case 60:
			return 5;
		case 70:
			return 4;
		case 80:
			return 3;
		case 90:
			return 2;
		case 100:
			return 60;
		default:
			return int.MaxValue;
	}

}


void TestMain()
{
	string mode = CampaignOrSettings();
	if (mode == "c")
	{
		Campaign();
	}
	else if (mode == "s")
	{
		Console.WriteLine("Starting Settings Mode...");
		var (difficulty, attempts) = DifficultyLevel();
	}
}
void Campaign()
{
	Console.WriteLine($"You are on level {level}.");
	Console.WriteLine($"Guess a number between 1 and {Range()}.");
	if (level % 10 == 0)
	{
		Console.WriteLine("Boss Level! You have limited attempts!");
		int maxAttempts = LimitAttempts(level);
		long range = RandomNumber();
		long result = HigherOrLower(range, maxAttempts);
		if (result != -1)
		{
			Campaign();
		}
		else
		{
			Console.WriteLine("Game Over! You have failed the boss level.");
		}

	}

	else
	{
		long range = RandomNumber();
		HigherOrLower(range, int.MaxValue);
	}

}
while (true)
{
	TestMain();
}

