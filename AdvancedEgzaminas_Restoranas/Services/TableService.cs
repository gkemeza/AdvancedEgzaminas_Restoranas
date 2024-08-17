﻿using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class TableService : ITableService
    {
        private List<Table> _tables;
        private readonly IDataAccess _dataAccess;
        private readonly string _filePath;

        public TableService(IDataAccess dataAccess, string filePath)
        {
            _dataAccess = dataAccess;
            _filePath = filePath;
            _tables = _dataAccess.ReadCsv<Table>(_filePath);
        }

        private List<Table> LoadTables()
        {
            try
            {
                return _dataAccess.ReadCsv<Table>(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Failed to load tables from CSV -> {ex}");
                return new List<Table>();
            }
        }

        public void PrintTables()
        {
            Console.WriteLine("***** Tables *****\n");
            foreach (var table in _tables)
            {
                Console.Write($"{table.Number}. {table.Seats} seats - ");
                if (table.IsOccupied)
                {
                    Console.WriteLine($"(taken)");
                }
                else
                {
                    Console.WriteLine($"(free)");
                }
            }
        }

        // Reload tables explicitly if needed
        public void ReloadTables()
        {
            _tables = LoadTables();
        }

        public int ChooseTable()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter table number (1-10):");
                if (int.TryParse(Console.ReadLine(), out int tableNumber))
                {
                    if (tableNumber >= 1 && tableNumber <= _tables.Count)
                    {
                        if (IsTableAvailable(tableNumber))
                        {
                            return tableNumber;
                        }
                        else
                        {
                            Console.WriteLine($"Table {tableNumber} is taken!");
                            Console.WriteLine("\nPress 'Enter' to continue...");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Table {tableNumber} doesn't exist!");
                        Console.WriteLine("\nPress 'Enter' to continue...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Enter a whole number!");
                    Console.WriteLine("\nPress 'Enter' to continue...");
                    Console.ReadLine();
                }
            }

        }

        public Table? GetTable(int tableNumber)
        {
            return _tables.FirstOrDefault(t => t.Number == tableNumber);
        }

        public bool AreFreeTables()
        {
            return _tables.Any(t => !t.IsOccupied);
        }

        public bool IsTableAvailable(int tableNumber)
        {
            return _tables.Any(t => t.Number == tableNumber && !t.IsOccupied);
        }

        public bool FreeTable(int tableNumber)
        {
            var table = GetTable(tableNumber);

            if (table == null)
            {
                throw new ArgumentException($"Table {tableNumber} does not exist.");
            }
            if (!table.IsOccupied)
            {
                throw new InvalidOperationException($"Table {tableNumber} is already free.");
            }

            table.IsOccupied = false;
            return true;
        }

        public bool OccupyTable(int tableNumber)
        {
            var table = GetTable(tableNumber);

            if (table == null)
            {
                throw new ArgumentException($"Table {tableNumber} does not exist.");
            }
            if (table.IsOccupied)
            {
                throw new InvalidOperationException($"Table {tableNumber} is already occupied.");
            }

            table.IsOccupied = true;
            return true;
        }

        public void UpdateTablesInFile() => _dataAccess.WriteCsv<Table>(_filePath, _tables);

        public void SeedTables()
        {
            var tables = new List<Table>()
            {
                new Table (1, 2),
                new Table (2, 2),
                new Table (3, 2),
                new Table (4, 4),
                new Table (5, 4),
                new Table (6, 2),
                new Table (7, 12),
                new Table (8, 2),
                new Table (9, 2),
                new Table (10, 2),
            };

            _dataAccess.WriteCsv(_filePath, tables);
        }
    }
}
