using System;

namespace personalProjectBattleSim {
    class Program {
        static void Main(string[] args) {
            string doLoop = "Y";

            do {//do loop for multiple runs of the program
                double health = 25.0;
                double championHealth = 25.0;
                double championStamina = 10.0;
                double stamina = 10.0;
                double specialAttack = 10.0;
                double attack = 5.0;
                double blockStaminaCost = 1.0;
                double specialAttackStaminaCost = 6.0;
                double attackStaminaCost = 3.0;
                bool blockYes = false;
                int selection = 0;
                int championMoveSelection = 0;

                //intro message
                Console.WriteLine("Welcome challenger. Can you best the champion?\n");

                //loops game while both players have health
                while (health > 0.0 && championHealth > 0.0) {   
                    //if your health is positive the health and stamina values are updated and you can make another move selection
                    if (health > 0.0) {                       
                        HealthUpdate(health, stamina, championHealth, championStamina);
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        selection = MoveSelection(stamina);
                    } else {
                        selection = 0;
                    }//end if

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("");

                    //player move selection menu
                    if (selection == 1) {
                        Console.WriteLine("You have used a special attack on the champion.\n");
                        championHealth = SpecialAttack(specialAttack, championHealth, blockYes);
                        stamina = stamina - specialAttackStaminaCost;
                        blockYes = false;

                    } else if (selection == 2) {
                        Console.WriteLine("You have attacked the champion.\n");
                        championHealth = BasicAttack(attack, championHealth, blockYes);
                        stamina = stamina - attackStaminaCost;
                        blockYes = false;

                    } else if (selection == 3) {
                        blockYes = true;
                        stamina = stamina - blockStaminaCost;
                        Console.WriteLine("You have blocked the next attack.\n");

                    } else if (selection == 4) {
                        Console.WriteLine("You have replenished your stamina.\n");
                        stamina += 5;
                        blockYes = false;
                    }//end if 

                    //if champion health is positive the health and stamina values are updated and they can make another move selection
                    if (championHealth > 0.0) {
                        HealthUpdate(health, stamina, championHealth, championStamina);
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        championMoveSelection = ChampionMoveSelcetion(championStamina);
                    } else {
                        championMoveSelection = 0;
                    }//end if

                    Console.ForegroundColor = ConsoleColor.Green;

                    //champion move selction menu
                    if (championMoveSelection <= 50 && championMoveSelection != 0) {
                        Console.WriteLine("The champion has used a special attack on you.\n");
                        health = SpecialAttack(specialAttack, health, blockYes);
                        championStamina = championStamina - specialAttackStaminaCost;
                        blockYes = false;

                    } else if (championMoveSelection > 50 && championMoveSelection <= 75 && championMoveSelection != 0) {
                        Console.WriteLine("The champion has attacked you.\n");
                        health = BasicAttack(attack, health, blockYes);
                        championStamina = championStamina - attackStaminaCost;
                        blockYes = false;

                    } else if (championMoveSelection > 75 && championMoveSelection != 100 && championMoveSelection != 0) {
                        blockYes = true;
                        championStamina = championStamina - blockStaminaCost;
                        Console.WriteLine("The champion has blocked the next attack.\n");

                    } else if (championMoveSelection == 100 && championMoveSelection != 0) {
                        Console.WriteLine("The champion has replenished their stamina.\n");
                        championStamina += 5;
                        blockYes = false;
                    }//end if                  
                }//end while                
                
                // win/loss message
                if (health > 0.0) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Congratulations you have beaten the champion.\n");
                } else if (championHealth > 0.0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have lost to the champion. Try again next time.\n");
                }//end if

                Console.ForegroundColor = ConsoleColor.White;

                //ask to run the program again
                doLoop=RunAgain();
                Console.Clear();

            } while (doLoop == "Y");
        }//end main
        static int MoveSelection(double stamina) {
            //prompt for move selection
            int selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Stamina]: ");
            //Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tdamage-6, damage-3, half damage next attack, charge 5 stamina");
           // Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\tstamina cost-10, stamina cost-5");

            //validates for anything entered besides 1, 2, 3, or 4
            while (selection != 1 && selection != 2 && selection != 3 && selection != 4) {
                Console.WriteLine("That is not a valid selection.");
                selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Stamina]: ");
            }//end while

            //validates each move to make sure there is enough stamina available to execute 
            while (selection == 1 && stamina < 6) {
                Console.WriteLine("");
                Console.WriteLine("You don't have enough mana for that move.");
                selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Stamina]: ");
                //second validation for anything entered besides 1, 2, 3, or 4
                while (selection != 1 && selection != 2 && selection != 3 && selection != 4 && stamina < 6) {
                    Console.WriteLine("That is not a valid selection.");
                    selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Stamina]: ");
                }//end while
            }//end while
            while (selection == 2 && stamina < 3) {
                Console.WriteLine("");
                Console.WriteLine("You don't have enough mana for that move.");
                selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                while (selection != 1 && selection != 2 && selection != 3 && selection != 4 && stamina < 3) {
                    Console.WriteLine("That is not a valid selection.");
                    selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                }//end while
            }//end while 
            while (selection == 3 && stamina < 1) {
                Console.WriteLine("");
                Console.WriteLine("You don't have enough mana for that move.");
                selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                while (selection != 1 && selection != 2 && selection != 3 && selection != 4 && stamina < 1) {
                    Console.WriteLine("That is not a valid selection.");
                    selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                }//end while
            }//end while 
            while (selection == 4 && stamina > 5) {
                Console.WriteLine("You have more than enough mana. No need to charge it.");
                selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                while (selection != 1 && selection != 2 && selection != 3 && selection != 4 && stamina > 5) {
                    Console.WriteLine("That is not a valid selection.");
                    selection = PromptInt("Make your move selection using the following numbers [1: Special Attack, 2: Attack, 3: Block, 4: Charge Mana]: ");
                }//end while
            }//end while
            return selection;
        }//end MoveSelection
        static void HealthUpdate(double health, double stamina, double championHealth, double championStamina) {
            //updates health and stamina values for both players
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Your Health: {health}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Your stamina: {stamina}");

            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Champion health: {championHealth}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Champion stamina {championStamina}");
        }//end HealthUpdate
        static double BasicAttack(double attack, double championHealth, bool blockYes) {
            //if the previous player blocked then this attack has half damage
            if (blockYes == true) {
                attack = attack / 2;
            }
            championHealth = championHealth - attack;
            return championHealth;
        }//end BasicAttack
        static double SpecialAttack(double specialAttack, double championHealth, bool blockYes) {
            //if the previous player blocked then this attack has half damage
            if (blockYes == true) {
                specialAttack = specialAttack / 2;
            }
            championHealth = championHealth - specialAttack;
            return championHealth;
        }//end SpecialAttack    
        static int ChampionMoveSelcetion(double championStamina) {  
            //randomly determines which move the champion will use
            Random random = new Random();
            int championSelection = random.Next(1, 100);
            //if a move is picked but enough stamina is not available the champion will replenish stamina 
            if (championSelection <= 50 && championStamina < 6) {
                championSelection = 100;
            }else if (championSelection > 50 && championSelection <= 75 && championStamina < 3) {
                championSelection = 100;
            }else if (championSelection > 75 && championStamina < 1 && championSelection != 100) {
                championSelection = 100;
            }//end if

            return championSelection;
        }//end ChampionMoveSelection
        static string Prompt(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }//end prompt
        static int PromptInt(string message) {
            int parsedValue = 0;
            while (int.TryParse(Prompt(message), out parsedValue) == false) {
                Console.WriteLine("\nInvalid Value. Please enter an integer.\n");
            }//end while
            return parsedValue;
        }//end PromptDecimal
        static string RunAgain() {
            string doLoop = "Y";
            Console.Write("Would you like to play again? [Y/N]: ");
            doLoop = Console.ReadLine().Trim().ToUpper();
            while (doLoop != "Y" && doLoop != "N") {
                Console.WriteLine("\nThat is not valid input.");
                Console.Write("Please enter 'Y' to run again or 'N' to quit: ");
                doLoop = Console.ReadLine().Trim().ToUpper();
                return doLoop;
            }//end while
            return doLoop;
        }//end RunAgain

    }//end class
}//end namespace
