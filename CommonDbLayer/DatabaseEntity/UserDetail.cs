using System;
using System.Collections.Generic;

namespace CommonDbLayer.DatabaseEntity
{
    public partial class UserDetail
    {
        public long UserId { get; set; }
        public string? UserEmail { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string? UserType { get; set; }
    }
}
