using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Data.Helper
{
    public enum EventType
    {
        Add = 0,
        Update = 1, 
        Delete = 2,
        Edit = 3,
        Get = 4,
        GetAll = 5,
        Login = 6, 
        Logout = 7,
        Search = 8,
        ChangePassowrd = 9,
        ForgotEmail = 10,
        VerifyEmail = 11,
        VerifyForgotPasswordLink = 12,
        ResetPassword = 13,
    }
}
