//#region License
///*
//Copyright © 2014-2018 European Support Limited

//Licensed under the Apache License, Version 2.0 (the "License")
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
//*/
//#endregion

//using Amdocs.Ginger.CoreNET.Drivers.CommunicationProtocol;
//using GingerCoreNET.RunLib;
//using System;
//using System.Threading;

//namespace Amdocs.Ginger.GingerConsole
//{
//    public class GingerGridMenu
//    {
//        GingerGrid mGingerGrid;

//        MenuItem StartGridMenuItem;
//        MenuItem StopGridMenuItem;
//        MenuItem NodeListMenuItem;

//        public MenuItem GetMenu()
//        {
//            StartGridMenuItem = new MenuItem(ConsoleKey.D1, "Start Grid", () => StartGrid(), true);
//            StopGridMenuItem = new MenuItem(ConsoleKey.D2, "Stop Grid", () => StopGrid(), false);
//            NodeListMenuItem = new MenuItem(ConsoleKey.D3, "Node List", () => NodeList(), false);

//            MenuItem GingerGridMenu = new MenuItem(ConsoleKey.G, "Ginger Grid");
//            GingerGridMenu.SubItems.Add(StartGridMenuItem);
//            GingerGridMenu.SubItems.Add(StopGridMenuItem);
//            GingerGridMenu.SubItems.Add(NodeListMenuItem);

//            return GingerGridMenu;
//        }

//        void StartGrid()
//        {
//            Console.WriteLine("Starting Ginger Grid Hub");
//            mGingerGrid = new GingerGrid(SocketHelper.GetOpenPort());
//            mGingerGrid.Start();
//            Console.WriteLine("Ginger Grid started - " + mGingerGrid.Status);
//            Console.WriteLine("Port: " + mGingerGrid.Port);
//        }

//        void StopGrid()
//        {
//            mGingerGrid.Stop();
//            Console.WriteLine("Ginger Grid stopped");
//            // Loop until key press

//            Console.WriteLine("Ginger Grid Hub List");
//        }

//        void NodeList()
//        {
//            Console.WriteLine("Ginger Grid Hub List");
//            PrintGridInfo();

//            // Hook chnaged event for auto updates
//            // We print the grid info when something changed
//            mGingerGrid.NodeList.CollectionChanged += List_CollectionChanged;

//            // Wait for user 
//            Console.WriteLine("List will refresh automatically when list changed, Press any key to exit");

//            while (!Console.KeyAvailable)
//            {
//                Thread.Sleep(500);
//            }
//            Console.Read();
//        }

//        void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
//        {
//            PrintGridInfo();
//        }

//        void PrintGridInfo()
//        {
//            Console.WriteLine("Ginger Grid Info last update: " + DateTime.Now);
//            Console.WriteLine("Ginger Node Count=" + mGingerGrid.NodeList.Count);
//            Console.WriteLine("========================================================================");
//            Console.WriteLine("| # | Name       |     Host    |  OS       |   IP          |   Status   |");
//            Console.WriteLine("========================================================================");
//            int i = 0;
//            foreach (GingerNodeInfo GNI in mGingerGrid.NodeList)
//            {
//                i++;
//                Console.WriteLine("| " + i + " | " + GNI.Name + " | " + GNI.Host + " | " + GNI.OS + " | " + GNI.IP + " | " + GNI.Status + " |");
//                Console.WriteLine("----------------------------------------------------------");
//            }
//            Console.WriteLine("================================================================");
//        }
//    }
//}
