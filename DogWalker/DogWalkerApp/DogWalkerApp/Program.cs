using System;

namespace DogWalkerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            WalkerRepository walkers = new WalkerRepository();

            System.Collections.Generic.List<Walker> allWalkers = walkers.GetAllWalkers();

            Console.WriteLine("Walkers:");
            foreach(Walker walker in allWalkers)
            {
                Console.WriteLine($"{walker.WalkerName}");
            }

            Console.WriteLine();

            Walker foundWalker = walkers.GetWalkerByNeighborhoodId(3);
            Console.WriteLine("All walkers on the east side:");
            Console.WriteLine($"{foundWalker.WalkerName}");

            Console.WriteLine();

            Walker newWalker = new Walker
            {
                WalkerName = "Holden Parker",
                NeighborhoodId = 1
            };

            walkers.AddWalker(newWalker);
            Console.WriteLine($"Added new walker: {newWalker.WalkerName}");

            Console.WriteLine();

            OwnerRepository owners = new OwnerRepository();
            System.Collections.Generic.List<Owner> allOwners = owners.GetAllOwnersWithNeighborhood();
            Console.WriteLine("All owners and their neighborhood:");
            foreach(Owner owner in allOwners)
            {
                Console.WriteLine($"{owner.DogOwnerName}: {owner.Neighborhood.NeighborhoodName}");
            }

            Console.WriteLine();

            Owner newOwner = new Owner
            {
                DogOwnerName = "Wily Metcalf",
                DogOwnerAddress = "6256 Premier Dr",
                NeighborhoodId = 2,
                Phone = "615-555-5555"
            };

            owners.AddOwner(newOwner);
            Console.WriteLine($"Added new owner: {newOwner.DogOwnerName}");

            Console.WriteLine();

            Owner updatedOwner = new Owner
            {
                Id = newOwner.Id,
                DogOwnerName = newOwner.DogOwnerName,
                DogOwnerAddress = newOwner.DogOwnerAddress,
                NeighborhoodId = 3,
                Phone = newOwner.Phone
            };
            owners.UpdateOwner(updatedOwner.Id, updatedOwner);
            Console.WriteLine($"Updated {newOwner.DogOwnerName}'s NeighborhoodId from {newOwner.NeighborhoodId} to {updatedOwner.NeighborhoodId}");

            Console.WriteLine();

            Walker updatedWalker = new Walker
            {
                Id = newWalker.Id,
                WalkerName = newWalker.WalkerName,
                NeighborhoodId = 2
            };
            walkers.UpdateWalker(updatedWalker.Id, updatedWalker);
            Console.WriteLine($"Updated {updatedWalker.WalkerName}'s NeighborhoodId from {newWalker.NeighborhoodId} to {updatedWalker.NeighborhoodId}");


        }
    }
}
