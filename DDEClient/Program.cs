﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DdeNet.Client;

namespace DDEClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId); 
                using (DdeClient client = new DdeClient("CoDeSys", @"D:\Project\discreteOut.pro"))
                try
                {
                    client.Disconnected += Client_Disconnected;
                    client.Connect();
                    //var s = client.Request("myitem1", 60000);
                    client.Advise += OnAdvise;
                    client.StartAdvise("T1", 1, true, 600);
                    client.StartAdvise("T2", 1, true, 600);
                    
                    
                }
                catch (DdeNet.DdeException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (client.IsConnected == true) client.Disconnect();
                }

            /*}
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            Console.ReadLine();
            
        }

        private static void Client_Disconnected(object sender, DdeDisconnectedEventArgs args)
        {
            Console.WriteLine("IsServerInitiated=" + args.IsServerInitiated.ToString() + " " +
                "IsDisposed=" + args.IsDisposed.ToString());
        }

        static void OnAdvise(object sender,DdeAdviseEventArgs e)
        {
            Console.WriteLine(e.Text);
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs r)
        {
            Console.WriteLine("Exit");
        }
    }
}