using System;
using System.Collections.Generic;

namespace racetrack
{
    class Program
    {

        List<dynamic> list = new List<dynamic>();
        List<dynamic> mylist = new List<dynamic>();
        int totalRevenue, vipRevenue=0;


        static void Main(string[] args)
        {
            
            int input=0;
            Program myprog = new Program();
            while(input !=5)
            {
                myprog.UserMenu();
                Console.WriteLine("Please provide input");
                input = Convert.ToInt32(Console.ReadLine());
                myprog.getUserInput(input);


            }
        }

        public void UserMenu()
        {
            Console.WriteLine("To add an entry Press       : 1");
            Console.WriteLine("To View all entries Press   : 2");
            Console.WriteLine("To add additional time Press: 3");
            Console.WriteLine("To Exit Press                :5");

        }

        public void viewDetails()
        {
            totalRevenue = 0;
            Console.WriteLine("-----------------------------------------\n");

            for(int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i].IsAdditional == 0?"Book":"Additional");
                Console.Write("\t"+ list[i].VehicleType);
                Console.Write("\t" + list[i].vehicleNumber);
                Console.Write("\t" + list[i].Time);
                Console.Write("\t" + list[i].IsValidMessage);
                Console.WriteLine();
                if (list[i].IsValid==1)
                {
                    mylist.Add(list[i]);
                }


                if(list[i].VehicleType == "BYKE" && list[i].IsAdditional==0 && list[i].IsValid==1)
                {
                    totalRevenue += 180;
                }
                else if (list[i].VehicleType == "CAR" && list[i].IsAdditional == 0 && list[i].IsValid == 1)
                {
                    totalRevenue += 360;
                }
                else if (list[i].VehicleType == "SUV" && list[i].IsAdditional == 0 && list[i].IsValid == 1)
                {
                    totalRevenue += 600;
                }else if(list[i].VehicleType == "BYKE" && list[i].IsAdditional == 1 && list[i].IsValid == 1)
                {
                    var elem = mylist.Find(x=> x.vehicleNumber== list[i].vehicleNumber);

                    int hours1 = int.Parse(elem.Time.Substring(0,2));
                    int min1 = int.Parse(elem.Time.Substring(3, 2));
                    int hours2 = int.Parse(list[i].Time.Substring(0, 2));
                    int min2 = int.Parse(list[i].Time.Substring(3, 2));

                    hours1 = hours1 + 3;
                    int min = min2 - min1;
                    if (min > 15)
                    {
                        totalRevenue +=50;
                    }

                }


            }


            Console.Write("Total Revenue \t \t" + totalRevenue + "   " + vipRevenue + "\n");
            Console.WriteLine("----------------------------------------------");
        }


        public void getUserInput(int input)
        {
            switch(input)
            {
                case 1:
                    dynamic myDynamic = new BookingDetails();
                    myDynamic.acceptDetails();
                    list.Add(myDynamic);
                    break;
                case 2:
                    viewDetails();
                    break;
                case 3:
                    dynamic myDynamic1 = new BookingDetails();
                    myDynamic1.acceptDetails(1);
                    list.Add(myDynamic1);
                    break;
                case 5:
                    Environment.Exit(2);
                    break;

            }
        }




    }


    public class BookingDetails
    {

        public string VehicleType, TrackType, vehicleNumber, Time, IsValidMessage;
        public int noofVeh, getInput, Track, VehicleTypeNumber, IsValid, IsAdditional;

        public void acceptDetails(int additional=0)
        {
            if(additional!=0)
            {
                IsAdditional = 1;
            }

            Console.WriteLine("Please enter Track details Press 1 for Regular 2 for VIP");

            var getInput = Console.ReadLine();
            int option;


            while((!int.TryParse(getInput, out option)))
            {
                Console.WriteLine("Incorrect Input Type. Please Try again");
                getInput= Console.ReadLine();
            }


            Track = Convert.ToInt32(getInput);
            if(Track==1)
            {
                TrackType = "Regular";
            }
            else
            {
                TrackType = "VIP";
            }

            Console.WriteLine("Please enter track details Press 1 for bike 2 for car 3 for SUV");

            var getVehicleTypeInput = Console.ReadLine();
            
            while((!int.TryParse(getVehicleTypeInput,out option)))
            {
                Console.WriteLine("Incorrect input Type please try again");
                getVehicleTypeInput = Console.ReadLine();
            }

            VehicleTypeNumber = Convert.ToInt32(getVehicleTypeInput);
            if(VehicleTypeNumber==1)
            {
                VehicleType = "BYKE";
            }else if(VehicleTypeNumber==2)
            {
                VehicleType = "CAR";

            }
            else
            {
                VehicleType = "SUV";
            }

            Console.WriteLine("Enter your vehicle number");
            vehicleNumber = Console.ReadLine();

            Console.WriteLine("Enter your Time");
            Time = Console.ReadLine();
            int opt;

            while(Time.Length !=5 || Time.IndexOf(":",2)!=2 || !int.TryParse(Time.Substring(0,2),out opt)|| !int.TryParse(Time.Substring(3, 2), out opt))
            {
                Console.WriteLine("Incorrect Input Type, Please Try again");
                Time= Console.ReadLine();
            }

            CheckIfValidEntry();



        }


        public void CheckIfValidEntry()
        {
            IsValid = 1;
            IsValidMessage = "Success";

            if(Convert.ToInt32(Time.Substring(0,2))>=17)
            {
                IsValid = 0;
                IsValidMessage = "Invalid Time";
            }

            int hours = Convert.ToInt32(Time.Substring(0,2));
            if(hours<13 || hours>20)
            {
                IsValid = 0;
                IsValidMessage = "Invalid Time";
            }

            if(IsAdditional==1 && (hours>13 && hours <20))
            {
                IsValid = 1;
                IsValidMessage = "Success";
            }

        }

    }
}
