
using System;

namespace _123.Models
{
public class User
{
    public int user_id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phone_number { get; set; }
    public string address { get; set; }
    public int role_id { get; set; }
    public bool is_deleted { get; set; }
}
}