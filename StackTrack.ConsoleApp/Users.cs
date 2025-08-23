using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Collections;

namespace Users;

public class User
{
    public string userName { get; set; } // The name the user input upon account creation.
    public string userPassword { get; set; }
    public string userID { get; set; } // A One-Time randomly generated userID that stays with the user forever.
    public Dictionary<string, DateTime> userBookStack { get; set; } = new Dictionary<string, DateTime>(); // Dynamic User Book Repo (Checkout, Check In, Late Fees, Extensions)
    public double userBalance { get; set; } // Balance that the user owes
}