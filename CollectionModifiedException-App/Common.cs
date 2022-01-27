using CollectionModifiedException_App.InventoryWareHouse;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CollectionModifiedException_App
{
    public static class Common
    {
        public static List<DummyModel> PrepareSomeData()
        {
            return new List<DummyModel> {
               new DummyModel {
                  Id = 1,
                  Name = "Doraemon"
               },
               new DummyModel {
                  Id = 0,
                  Name = "Joker"
               },
               new DummyModel {
                  Id = 2,
                  Name = "Rey"
               }
            };
        }

        public static void PrintListInConsole(List<InventoryModel> listItems)
        {
            foreach (var item in listItems)
            {
                Console.WriteLine("Text: {0} - Value: {1}", item.Text, item.Value);
            }
        }

        public static void PrintListInConsole(ConcurrentBag<InventoryModel> listItems)
        {
            foreach (var item in listItems)
            {
                Console.WriteLine("Text: {0} - Value: {1}", item.Text, item.Value);
            }
        }

    }
}
