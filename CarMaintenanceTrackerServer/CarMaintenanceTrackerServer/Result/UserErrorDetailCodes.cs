﻿using System.ComponentModel;

namespace CarMaintenanceTrackerServer.Result
{
    public enum UserErrorDetailCodes
    {
        [Description("User null reference error")]
        NULL_USER_ERROR,
        [Description("User registration error")]
        REGISTER_USER_ERROR,
        [Description("User login error")]
        LOGIN_USER_ERROR,
        [Description("User find error")]
        FIND_USER_ERROR,
        [Description("User update error")]
        UPDATE_USER_ERROR,
        [Description("User delete error")]
        DELETE_USER_ERROR
    }
}
