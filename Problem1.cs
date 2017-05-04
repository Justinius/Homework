using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianSolitaire
{
    class BulgarianSolitaire
    {
        static void Main(string[] args)
        {
            int numCards = 45; //number of cards we are playing with

            List<int> piles = initPiles(numCards);  //create the initial piles
            bool isDone = checkPiles(piles); //maybe we got lucky and we got it by random chance
            printPiles(piles); //what it says on the tin
                  
            if (isDone)  //if we did get lucky then exit
            {
                Console.WriteLine("Found the final configuration");
                Console.ReadLine();
                return;
            }

            while(!isDone)  //keep doing this until we are finished.
            {
                solitaireStep(ref piles);  //peform the Bulgarian Solitaire step
                isDone = checkPiles(piles); //check if we are finished, will exit the loop if we are
                printPiles(piles); //print it out
            }

            Console.WriteLine("Found the final configuration"); //print exit message
            Console.ReadLine(); //wait for acknowledgement
        }

        static void printPiles(List<int> piles)
        {
            foreach(int p in piles) //for each value in the piles print it out
            {
                Console.Write(p + " ");
            }
            Console.WriteLine(); //print a newline at the end
        }

        static List<int> initPiles(int numCards)
        {
            Random r = new Random();  //random number generator to create the piles
            List<int> initPiles = new List<int>(); //list of piles we will be creating

            int numCardsSoFar = 0; //how many card we have added into piles
            int numPulledCards = 0; //how many cards are we trying to put into the current pile

            while (numCardsSoFar != numCards) //if we haven't put all the cards into piles keep going
            {
                //this line of code generates a random number between 1 and the number of cards we have left
                numPulledCards = r.Next(1, numCards - numCardsSoFar + 1); //plus 1 so it becomes inclusive

                initPiles.Add(numPulledCards); //we add the number of cards we just grabbed to the piles
                numCardsSoFar += numPulledCards; //update our counter for the loop

                //this loop works because we add from one to the number of cards left so far
                //so as we pull closer to the limit we can only pull a small number of cards at a time
            }

            return initPiles; //return the piles we found
        }

        static void solitaireStep(ref List<int> piles)
        {
            int numPiles = piles.Count();  //by looking at the problem they take a card from each pile
                                           //and then add that as a pile
                                           //so we just get the number of piles in this line
            for(int j = 0; j < piles.Count(); j++)  //in this loop we remove a card from each pile
            {
                piles[j]--;  //shortcut for piles[j] = piles[j] - 1;
            }
            piles.Add(numPiles); //add the pile we created by pulling a card from every pile

            for (int j = 0; j < piles.Count(); j++) //since we can't have piles with 0 cards in them we are going to delete them
            {
                if (piles[j] == 0) //if the pile has 0 cards in it
                {
                    piles.RemoveAt(j); //remove that location
                    j--; //since the loop is on the number of piles, if we remove one j is out of sync and we must reset it
                }
            }
        }

        static bool checkPiles(List<int> piles)
        {
            List<int> clonedPiles = new List<int>(piles); //create a duplicate of the list
                                                          //this may not be necessary if you don't care about the list 
                                                          //always being sorted.
            clonedPiles.Sort(); //sort the list
            for(int j = 0; j < clonedPiles.Count(); j++) //loop over every pile we have
            {
                if(clonedPiles[j] != (j+1))  //since its sorted, the number of cards in the pile should equal its position
                                             //its checked agains (j+1) because the arrays are 0 based instead of 1 based like the cards
                {
                    return false;  //if we find a mismatch we can cancel early and leave the leave, false means its not right
                }
            }
            return true; //if we made it here then all the piles match so its correct.
        }


    }
}
