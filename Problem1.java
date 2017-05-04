import java.util.ArrayList;
import java.util.Random;
import java.util.Collections;

public class MyClass {
    public static void main(String args[]) {
        int numCards = 45; //number of cards we are playing with

            ArrayList<Integer> piles = initPiles(numCards);  //create the initial piles
            boolean isDone = checkPiles(piles); //maybe we got lucky and we got it by random chance
            printPiles(piles); //what it says on the tin
                  
            if (isDone)  //if we did get lucky then exit
            {
                System.out.println("Found the final configuration");
               // Console.ReadLine();
                return;
            }

            while(!isDone)  //keep doing this until we are finished.
            {
                solitaireStep(piles);  //peform the Bulgarian Solitaire step
                isDone = checkPiles(piles); //check if we are finished, will exit the loop if we are
                printPiles(piles); //print it out
            }

            System.out.println("Found the final configuration"); //print exit message
            //Console.ReadLine(); //wait for acknowledgement
    }
    
    static void solitaireStep(ArrayList<Integer> piles)
    {
            int numPiles = piles.size();  //by looking at the problem they take a card from each pile
                                           //and then add that as a pile
                                           //so we just get the number of piles in this line
            for(int j = 0; j < piles.size(); j++)  //in this loop we remove a card from each pile
            {
                int val = piles.get(j);
                val--;
                piles.set(j, val);
                //shortcut for piles[j] = piles[j] - 1;
            }
            piles.add(numPiles); //add the pile we created by pulling a card from every pile

            for (int j = 0; j < piles.size(); j++) //since we can't have piles with 0 cards in them we are going to delete them
            {
                if (piles.get(j) == 0) //if the pile has 0 cards in it
                {
                    piles.remove(j); //remove that location
                    j--; //since the loop is on the number of piles, if we remove one j is out of sync and we must reset it
                }
            }
    }
    
    static boolean checkPiles(ArrayList<Integer> piles)
    {
            ArrayList<Integer> clonedPiles = new ArrayList<Integer>(piles); //create a duplicate of the list
                                                          //this may not be necessary if you don't care about the list 
                                                          //always being sorted.
            Collections.sort(clonedPiles); //sort the list
            for(int j = 0; j < clonedPiles.size(); j++) //loop over every pile we have
            {
                if(clonedPiles.get(j) != (j+1))  //since its sorted, the number of cards in the pile should equal its position
                                             //its checked agains (j+1) because the arrays are 0 based instead of 1 based like the cards
                {
                    return false;  //if we find a mismatch we can cancel early and leave the leave, false means its not right
                }
            }
            return true; //if we made it here then all the piles match so its correct.
    }
    
    static void printPiles(ArrayList<Integer> piles)
    {
            for(Integer p : piles) //for each value in the piles print it out
            {
                System.out.print(p + " ");
            }
            System.out.println(); //print a newline at the end
    }
    
    static ArrayList<Integer> initPiles(int numCards)
    {
            Random r = new Random();  //random number generator to create the piles
            ArrayList<Integer> initPiles = new ArrayList<Integer>(); //list of piles we will be creating

            int numCardsSoFar = 0; //how many card we have added into piles
            int numPulledCards = 0; //how many cards are we trying to put into the current pile

            while (numCardsSoFar != numCards) //if we haven't put all the cards into piles keep going
            {
                //this line of code generates a random number between 1 and the number of cards we have left
                numPulledCards = r.nextInt(numCards - numCardsSoFar) + 1; //plus 1 so it becomes inclusive

                initPiles.add(numPulledCards); //we add the number of cards we just grabbed to the piles
                numCardsSoFar += numPulledCards; //update our counter for the loop

                //this loop works because we add from one to the number of cards left so far
                //so as we pull closer to the limit we can only pull a small number of cards at a time
            }

            return initPiles; //return the piles we found
    }
}
