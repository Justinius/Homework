using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework2
{
    public static class Meal
    {
        public static int chicken = 0;
        public static int beef = 1;
        public static int veggie = 2;

        public static bool isValidMeal(int mealChoice)
        {
            bool isValid = false;

            if (mealChoice >= 0 && mealChoice <= 2)
            {
                isValid = true;
            }
            
            return isValid;

        }

        public static string mealString(int meal)
        {
            if (isValidMeal(meal))
            {
                if (meal == chicken)
                    return "Chicken";
                else if (meal == beef)
                    return "   Beef";
                else 
                    return " Veggie";
            }
            else
                return "XXXXXXX";
        }
    }

    public static class Destination
    {
        public static int Toledo = 0;
        public static int Houston = 1;
        public static int Boise = 2;

        public static double defaultToledoEconomyFare = 200;
        public static double defaultHoustonEconomyFare = 300;
        public static double defaultBoiseEconomyFare = 400;

        public static bool isValidDestination(int destChoice)
        {
            bool isValid = false;

            if (destChoice >= 0 && destChoice <= 2)
            {
                isValid = true;
            }

            return isValid;

        }
    }

    public static class SeatClass
    {
        public static int Economy = 0;
        public static int Business = 1;
        public static int First = 2;

        public static bool isValidClass(int classChoice)
        {
            bool isValid = false;

            if (classChoice >= 0 && classChoice <= 2)
            {
                isValid = true;
            }

            return isValid;

        }

        public static string printSeatClass(int classID)
        {
            StringBuilder sb = new StringBuilder();
            if (classID == Economy)
            {
                sb.Append("Economy");
            }
            else if (classID == Business)
            {
                sb.Append("Business");
            }
            else
                sb.Append("First");

            return sb.ToString();
        }
    }

    class Person
    {
        public string name;
        public int meal;
        public Person(string name, int mealChoice)
        {
            this.name = name;
            if (Meal.isValidMeal(mealChoice))
            {
                this.meal = mealChoice;
            }
            else //veggie by default
            {
                this.meal = Meal.veggie;
            }
        }

        public string printPerson()
        {
            return new StringBuilder().AppendFormat("{0},   {1}", name, Meal.mealString(meal)).ToString();
        }

    }

    class Seat
    {
        public int seatType;
        public Person customer;
        public bool isFilled;
        public int seatNumber;

        public Seat(Person customer, int seat, int seatNumber)
        {
            this.customer = customer;
            this.seatType = seat;
            isFilled = true;
            this.seatNumber = seatNumber;
        }

        public Seat(int seatNumber)
        {
            isFilled = false;
            this.seatNumber = seatNumber;
        }

        public string printSeatInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}.   {1}", seatNumber, customer.printPerson());
            return sb.ToString();
        }

    }

    class Plane
    {
        public int currDestination;
        public static int numEconomySeats = 8;
        public static int numBusinessSeats = 6;
        public static int numFirstClassSeats = 4;

        public static int numEconomySeatsAvailable;
        public static int numBusinessSeatsAvailable;
        public static int numFirstClassSeatsAvailable;

        double currEconomyFare;

        Seat[][] SeatArray;

        public string printManifest()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.AppendLine(SeatClass.printSeatClass(i));
                for (int j = 0; j < SeatArray[i].Length; j++)
                {
                    sb.AppendLine(SeatArray[i][j].printSeatInfo());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public string printClassInfo(int classID)
        {
            if (SeatClass.isValidClass(classID))
            {
                int numSold = numSeatsSold(classID);
                double classRevenue = 0;
                if (classID == SeatClass.Economy)
                    classRevenue = EconomyRevenue;
                else if (classID == SeatClass.Business)
                    classRevenue = BusinessRevenue;
                else
                    classRevenue = FirstClassRevenue;

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Number of seats Sold in {0} class: {1}\n\r", SeatClass.printSeatClass(classID), numSold);
                sb.AppendFormat("Revenue for {0} class: {1}\n\r", SeatClass.printSeatClass(classID), classRevenue);
                return sb.ToString();
            }
            return "";
        }

        public double costOfFlight
        {
            get
            {
                return .5 * (numBusinessSeats + numEconomySeats + numFirstClassSeats) * currEconomyFare;
            }
        }

        public double EconomyRevenue
        {
            get
            {
                double revenue = 0.0;
                for (int i = 0; i < numEconomySeats; i++)
                {
                    if (SeatArray[SeatClass.Economy][i].isFilled)
                        revenue += currEconomyFare;
                }
                return revenue;
            }
        }

        public double BusinessRevenue
        {
            get
            {
                double revenue = 0.0;
                for (int i = 0; i < numBusinessSeats; i++)
                {
                    if (SeatArray[SeatClass.Business][i].isFilled)
                        revenue += 2*currEconomyFare;
                }
                return revenue;
            }
        }

        public double FirstClassRevenue
        {
            get
            {
                double revenue = 0.0;
                for (int i = 0; i < numFirstClassSeats; i++)
                {
                    if (SeatArray[SeatClass.First][i].isFilled)
                        revenue += 4 * currEconomyFare;
                }
                return revenue;
            }
        }

        public double PlaneRevenue
        {
            get
            {
                return EconomyRevenue + BusinessRevenue + FirstClassRevenue;
            }
        }

        public double PlaneProfit
        {
            get
            {
                return PlaneRevenue - costOfFlight;
            }
        }


        public Plane(int dest)
        {
            initSeats();

            if (Destination.isValidDestination(dest))
            {
                if (dest == Destination.Toledo)
                    currEconomyFare = Destination.defaultToledoEconomyFare;
                if (dest == Destination.Houston)
                    currEconomyFare = Destination.defaultHoustonEconomyFare;
                if (dest == Destination.Boise)
                    currEconomyFare = Destination.defaultBoiseEconomyFare;
            }
            else //ship them to toledo
            {
                currEconomyFare = Destination.defaultToledoEconomyFare;
            }
        }

        public int numSeatsSold(int seatClass)
        {
            if(SeatClass.isValidClass(seatClass))
            {
                int numSold = 0;
                for (int i = 0; i < SeatArray[seatClass].Length; i++)
                {
                    if (SeatArray[seatClass][i].isFilled)
                        numSold++;
                }
                return numSold;
            }
            return 0;
        }

        public bool isValidSeatNum(int resClass, int seatNum)
        {
            if(seatNum < 1)
                return false;

            if (SeatClass.isValidClass(resClass))
            {
                if (resClass == SeatClass.Economy)
                {
                    if(seatNum <= (numEconomySeats+1))
                        return true;
                    else
                        return false;
                }
                if (resClass == SeatClass.Business)
                {
                    if (seatNum <= (numBusinessSeats + 1))
                        return true;
                    else
                        return false;
                }
                if (resClass == SeatClass.First)
                {
                    if (seatNum <= (numFirstClassSeats + 1))
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool isSeatAvailable(int resClass, int seatNum)
        {
            if (isValidSeatNum(resClass, seatNum))
            {
                if (SeatArray[resClass][seatNum - 1].isFilled == false) //seatNum - 1 since arrays are 0 based, seats are 1 based
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void changeDefaultFare(double newFare)
        {
            currEconomyFare = newFare;
        }

        public bool addCustomer(string name, int desClass, int desSeatNum, int desMeal)
        {
            if (isSeatAvailable(desClass, desSeatNum))
            {
                Person newPassanger = new Person(name, desMeal);
                Seat s = new Seat(newPassanger, desClass, desSeatNum);
                SeatArray[desClass][desSeatNum] = s;
                return true;
            }
            else
                return false;
        }

        public bool removeCustomer(int custSeatClass, int custSeatNum)
        {
            if(isValidSeatNum(custSeatClass, custSeatNum))
            {
                if (isSeatAvailable(custSeatClass, custSeatNum))
                    return false; //nobody was sitting there
                else
                {
                    //need the minus one since we are going from human normal to computer science
                    SeatArray[custSeatClass][custSeatNum-1].isFilled = false; 
                    SeatArray[custSeatClass][custSeatNum-1] = new Seat(custSeatNum);
                    return true;
                }
            }
            else
                return false; //can't remove from an imaginary seat
        }

        public bool removeCustomer(string name)
        {
            bool foundPassenger;
            int foundClass;
            int foundSeatIdx;
            foundPassenger = findPassengerByName(name, out foundClass, out foundSeatIdx);

            if (foundPassenger)
            {
                return removeCustomer(foundClass, foundSeatIdx + 1); //+1 because Idx is 0 based, instead of 1 based
            }
            
            return false;
        }

        private bool findPassengerByName(string name, out int foundClass, out int foundSeatIdx)
        {
            bool foundPassenger = false;
            foundClass = 0;
            foundSeatIdx = 0;
            for (int i = 0; i < numEconomySeats; i++)
            {
                if (SeatArray[SeatClass.Economy][i].customer.name == name)
                {
                    foundPassenger = true;
                    foundClass = SeatClass.Economy;
                    foundSeatIdx = i;
                }
            }
            for (int i = 0; i < numBusinessSeats; i++)
            {
                if (SeatArray[SeatClass.Business][i].customer.name == name)
                {
                    foundPassenger = true;
                    foundClass = SeatClass.Business;
                    foundSeatIdx = i;
                }
            }
            for (int i = 0; i < numFirstClassSeats; i++)
            {
                if (SeatArray[SeatClass.First][i].customer.name == name)
                {
                    foundPassenger = true;
                    foundClass = SeatClass.First;
                    foundSeatIdx = i;
                }
            }
            return foundPassenger;
        }

        private void initSeats()
        {
            SeatArray = new Seat[3][];
            SeatArray[SeatClass.Economy] = new Seat[numEconomySeats];
            SeatArray[SeatClass.Business] = new Seat[numBusinessSeats];
            SeatArray[SeatClass.First] = new Seat[numFirstClassSeats];
            
            for (int i = 0; i < numEconomySeats; i++)
            {
                SeatArray[SeatClass.Economy][i].isFilled = false;
                SeatArray[SeatClass.Economy][i].seatNumber = i+1;
            }

            for (int i = 0; i < numBusinessSeats; i++)
            {
                SeatArray[SeatClass.Business][i].isFilled = false;
                SeatArray[SeatClass.Business][i].seatNumber = i + 1;
            }

            for (int i = 0; i < numFirstClassSeats; i++)
            {
                SeatArray[SeatClass.First][i].isFilled = false;
                SeatArray[SeatClass.First][i].seatNumber = i + 1;
            }

            numEconomySeatsAvailable = numEconomySeats;
            numBusinessSeatsAvailable = numBusinessSeats;
            numFirstClassSeatsAvailable = numFirstClassSeats;
        }

        public bool changeSeat(int currClass, int currSeatNum, int newClass, int newSeatNum)
        {
            if (isValidSeatNum(currClass, currSeatNum) && isValidSeatNum(newClass, newSeatNum))
            {
                if (isSeatAvailable(currClass, currSeatNum) == true)
                    return false; //moving from empty seat
                else
                {
                    if (isSeatAvailable(newClass, newSeatNum))
                    {
                        //so to get here, we have a filled sit wanting to move
                        //to an open seat

                        Person cust = SeatArray[currClass][currSeatNum - 1].customer;
                        SeatArray[newClass][newSeatNum - 1] = new Seat(cust, newClass, newSeatNum);
                        removeCustomer(currClass, currSeatNum);
                        return true;
                    }
                    else
                        return false; //requested seat is full
                }
            }
            else
                return false; //either the the current seat doesn't exist or the new one doesn't 
        }

        public bool changeMeal(int custClass, int custSeatNum, int newMeal)
        {
            if (Meal.isValidMeal(newMeal) == false)
            {
                return false; //can't change meal
            }
            else
            {
                if (isValidSeatNum(custClass, custSeatNum) && !isSeatAvailable(custClass, custSeatNum))
                {
                    SeatArray[custClass][custSeatNum - 1].customer.meal = newMeal;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool changeMeal(string name, int newMeal)
        {
            bool foundPassenger;
            int foundClass;
            int foundSeatIdx;
            foundPassenger = findPassengerByName(name, out foundClass, out foundSeatIdx);

            if (foundPassenger)
            {
                return changeMeal(foundClass,foundSeatIdx+1, newMeal); //+1 because Idx is 0 based, instead of 1 based
            }
            return false;
        }
        
    }
}
