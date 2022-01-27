using CollectionModifiedException_App.InventoryWareHouse;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionModifiedException_App
{
    public static class Startup
    {
        public const int LoopLimit = 100;
        public static void Invoker()
        {
            // TestMyException_v1();
            // TestMySolution_v1();

            //=============================================
            TestProdException_v2();
            TestProdSolution_v2();

        }

        public static void TestMyException_v1()
        {
            try
            {
                List<DummyModel> studentsList = Common.PrepareSomeData();
                foreach (var student in studentsList)
                {
                    if (student.Id <= 0)
                    {
                        studentsList.Remove(student);
                    }
                    else
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadLine();
            }
        }

        public static void TestMySolution_v1()
        {
            try
            {
                List<DummyModel> studentsList = Common.PrepareSomeData();
                foreach (var student in studentsList.ToList())
                {
                    if (student.Id <= 0)
                    {
                        studentsList.Remove(student);
                    }
                    else
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadLine();
            }
        }

        public static void TestProdException_v2()
        {
            var listItems = new List<InventoryModel>();
            Parallel.Invoke(
                () =>
                {
                    //AddForEach Loop Here Later
                    for(int i = 0; i < LoopLimit; i++)
                    {
                        var item = new InventoryModel
                        {
                            Text = "Some Text From First Thread",
                            Value = i % 2 != 0 ? "Some Value From First Thread " + i : "Common Value",
                            Identifier = 100,
                            SysId = "001",
                            Data = new Dictionary<string, string>()
                        };
                        item.Data.Add("isEmbargo", "Embargo From First Thread");
                        item.Data.Add("iCMExtendedUserEntity_ICMApprovalrequiredfromAdvisor", "ICMApprovalrequiredfromAdvisor From First Thread");

                        listItems.Add(item);
                    }
                },
                () =>
                {
                    //AddForEach Loop Here Later
                    for (int i = 0; i <= LoopLimit; i++)
                    {
                        var item = new InventoryModel
                        {
                            Text = "Some Text From Second Thread",
                            Value = i % 2 != 0 ? "Some Value From Second Thread " + i : "Common Value", 
                            Identifier = 100,
                            SysId = "001",
                            Data = new Dictionary<string, string>()
                        };
                        item.Data.Add("isEmbargo", "Embargo From Second Thread");
                        item.Data.Add("iCMExtendedUserEntity_ICMApprovalrequiredfromAdvisor", "ICMApprovalrequiredfromAdvisor From Second Thread");

                        if (!listItems.Any(x => x.Value.Equals(item.Value)))
                        {
                            listItems.Add(item);
                        }
                    }
                }
            );

            listItems = listItems.DistinctBy(item => item.Value).OrderBy(item => item.Text).ToList();
            Common.PrintListInConsole(listItems);
        }

        public static void TestProdSolution_v2()
        {
            //var listItems = new List<CustomSelectListItem>();
            var concurrentItems = new ConcurrentBag<InventoryModel>();
            Parallel.Invoke(
                () =>
                {
                    //Console.WriteLine("FIRST Method=beta, Thread={0}", Thread.CurrentThread.ManagedThreadId);
                    //AddForEach Loop Here Later
                    for (int i = 1; i <= LoopLimit; i++)
                    {
                        var item = new InventoryModel
                        {
                            Text = "Some Text From First Thread",
                            Value = i % 2 != 0 ? "Some Value From First Thread " + i : "Common Value",
                            Identifier = 100,
                            SysId = "001",
                            Data = new Dictionary<string, string>()
                        };
                        item.Data.Add("isEmbargo", "Embargo From First Thread");
                        item.Data.Add("iCMExtendedUserEntity_ICMApprovalrequiredfromAdvisor", "ICMApprovalrequiredfromAdvisor From First Thread");

                        concurrentItems.Add(item);
                    }
                },
                () =>
                {
                    //Console.WriteLine("SECOND Method=beta, Thread={0}", Thread.CurrentThread.ManagedThreadId);
                    //AddForEach Loop Here Later
                    for (int i = 1; i <= LoopLimit; i++)
                    {
                        var item = new InventoryModel
                        {
                            Text = "Some Text From Second Thread",
                            Value = i % 2 != 0 ? "Some Value From Second Thread " + i : "Common Value",
                            Identifier = 100,
                            SysId = "001",
                            Data = new Dictionary<string, string>()
                        };
                        item.Data.Add("isEmbargo", "Embargo From Second Thread");
                        item.Data.Add("iCMExtendedUserEntity_ICMApprovalrequiredfromAdvisor", "ICMApprovalrequiredfromAdvisor From Second Thread");

                        //Console.WriteLine("=============== listItems COUNT: {0}", listItems.Count);

                        if (!concurrentItems.Any(x => x.Value.Equals(item.Value)))
                        {
                            concurrentItems.Add(item);
                        }
                    }
                }
            );

            //listItems = listItems.DistinctBy(item => item.Value).OrderBy(item => item.Text).ToList(); //Problem

            var listItems_ByGT = concurrentItems.ToList().DistinctBy(item => item.Value).OrderBy(item => item.Text).ToList(); //Solution - 1
            var listItems = concurrentItems.GroupBy(item => item.Value).Select(sel => sel.FirstOrDefault()).OrderBy(ord => ord.Text).ToList(); //Solution - 2

            Common.PrintListInConsole(concurrentItems);
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Common.PrintListInConsole(listItems_ByGT);
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Common.PrintListInConsole(listItems);

            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("=================================================================================================================");
            Console.WriteLine("concurrentItems COUNT: {0}", concurrentItems.Count);
            Console.WriteLine("listItems_ByGT COUNT: {0}", listItems_ByGT.Count);
            Console.WriteLine("listItems COUNT: {0}", listItems.Count);
            //Common.PrintListInConsole(listItems);
        }

    }


}
