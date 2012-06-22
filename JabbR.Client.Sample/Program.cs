﻿using System;
using System.Diagnostics;
using JabbR.Client.Models;
using ExtensionMethods;
using System.Net;

namespace JabbR.Client.Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            




            string server = "http://jabbr.net/";
            string roomName = "test";

            var appDomain = "";






            var client = new JabbRClient(server, new SignalR.Client.Transports.LongPollingTransport());

            client.Connect("c47bba43-4992-4725-abc8-4dc9a6f3723d")
                .Then(logOnInfo =>
                {

                })
                    .OnError(ex =>
                    {

                    })
                    .Then(() =>
                    {

                    });

            //// Subscribe to new messages
            //client.MessageReceived += (message, room) =>
            //{
            //    Console.WriteLine("[{0}] {1}: {2}", message.When, message.User.Name, message.Content);
            //};

            //client.UserJoined += (user, room) =>
            //{
            //    Console.WriteLine("{0} joined {1}", user.Name, room);
            //};

            //client.UserLeft += (user, room) =>
            //{
            //    Console.WriteLine("{0} left {1}", user.Name, room);
            //};

            //client.PrivateMessage += (from, to, message) =>
            //{
            //    Console.WriteLine("*PRIVATE* {0} -> {1} ", from, message);
            //};

            //// Connect to chat
            //client.Connect(userName, password)
            //    .ContinueWith(task =>
            //{
            //    LogOnInfo info = task.Result;


            //    Console.WriteLine("Logged on successfully. You are currently in the following rooms:");
            //    foreach (var room in info.Rooms)
            //    {
            //        Console.WriteLine(room.Name);
            //        Console.WriteLine(room.Private);
            //    }

            //    Console.WriteLine("User id is {0}. Don't share this!", info.UserId);

            //    Console.WriteLine();

            //    // Get my user info
            //    User myInfo = client.GetUserInfo().Result;

            //    Console.WriteLine(myInfo.Name);
            //    Console.WriteLine(myInfo.LastActivity);
            //    Console.WriteLine(myInfo.Status);
            //    Console.WriteLine(myInfo.Country);


            //    client.JoinRoom(roomName);

            //    Console.WriteLine();

            //    // Join a room called test
            //    client.JoinRoom(roomName).ContinueWith(_ =>
            //    {
            //        // Get info about the test room
            //        client.GetRoomInfo(roomName).ContinueWith(t =>
            //        {
            //            Room roomInfo = t.Result;

            //            Console.WriteLine("Users");

            //            foreach (var u in roomInfo.Users)
            //            {
            //                Console.WriteLine(u.Name);
            //            }

            //            Console.WriteLine();

            //            foreach (var u in roomInfo.Users)
            //            {
            //                if (u.Name != userName)
            //                {
            //                    client.SendPrivateMessage(u.Name, "hey there, this is private right?");
            //                }
            //            }

            //        });
            //    });

            //    // Set the flag
            //    client.SetFlag("bb");

            //    // Set the user note
            //    client.SetNote("This is testing a note");

            //    // Mark the client as typing
            //    client.SetTyping(roomName);

            //    // Clear the note
            //    client.SetNote(null);

            //    // Say hello to the room
            //    client.Send("Hello world", roomName);

            //    Console.WriteLine("Press any key to leave the room and disconnect");
            //    Console.Read();
            //    client.LeaveRoom(roomName).ContinueWith(_ =>
            //    {
            //        client.Disconnect();
            //    });
            //});

            Console.ReadKey();
        }
    }
}
