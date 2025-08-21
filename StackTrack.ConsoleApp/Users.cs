using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Collections;

namespace Users;

public class User
{
    public string Name { get; set; } // The name the user input upon account creation.
    public string userID { get; set; } // A 1 time randomly generated userID that stays with the user forever.
    public Dictionary<string, DateTime> bookList { get; set; } = new Dictionary<string, DateTime>(); // Dynamic User Book Repo (Checkout, Check In, Late Fees, Extensions)
    
}