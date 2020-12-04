using Roommates43.Models;
using Roommates43.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates43
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        ShowAllRooms(roomRepo);
                        break;
                    case ("Search for room"):
                        ShowOneRoom(roomRepo);
                        break;
                    case ("Add a room"):
                        InsertRoom(roomRepo);
                        break;
                    case ("Update a room"):
                        UpdateRoom(roomRepo);
                        break;
                    case ("Delete a room"):
                        DeleteRoom(roomRepo);
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static void UpdateRoom(RoomRepository roomRepo)
        {
            roomRepo.GetAll().ForEach(r => Console.WriteLine($"[{r.Id}] {r.Name}"));
            
            Console.WriteLine();
            Console.Write("What is the ID of the room you want to delete? ");

            int roomId = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string newName = Console.ReadLine();

            Console.Write("Max Occupancy: ");
            int newMax = int.Parse(Console.ReadLine());

            Room updatedRoom = new Room()
            {
                Id = roomId,
                Name = newName,
                MaxOccupancy = newMax
            };

            roomRepo.Update(updatedRoom);
            Console.Write("Room has been successfully updated. Press any key to continue...");
            Console.ReadKey();
        }

        static void DeleteRoom(RoomRepository roomRepo)
        {
            roomRepo.GetAll().ForEach(r => Console.WriteLine($"[{r.Id}] {r.Name}"));

            Console.WriteLine();
            Console.Write("What is the ID of the room you want to delete? ");

            int roomId = int.Parse(Console.ReadLine());
            roomRepo.Delete(roomId);

            Console.Write("Room has been successfully removed. Press any key to continue...");
            Console.ReadKey();
        }

        static void InsertRoom(RoomRepository roomRepo)
        {
            Console.Write("Room Name: ");
            string name = Console.ReadLine();

            Console.Write("Max Occupancy: ");
            int max = int.Parse(Console.ReadLine());

            Room room = new Room()
            {
                Name = name,
                MaxOccupancy = max
            };

            roomRepo.Insert(room);

            Console.WriteLine($"{room.Name} has been added to the database and given the ID of {room.Id}");
            Console.ReadKey();
        }

        static void ShowAllRooms(RoomRepository roomRepo)
        {
            List<Room> rooms = roomRepo.GetAll();
            foreach (Room r in rooms)
            {
                Console.WriteLine($"[{r.Id}] {r.Name} Max Occ({r.MaxOccupancy})");
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void ShowOneRoom(RoomRepository roomRepo)
        {
            Console.Write("Room Id: ");
            int id = int.Parse(Console.ReadLine());

            Room room = roomRepo.GetById(id);

            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Update a room",
            "Delete a room",
            "Exit"
        };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }

        }
    }
}
