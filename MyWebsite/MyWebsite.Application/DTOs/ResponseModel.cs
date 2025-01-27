﻿using MyWebsite.Domain.Enums;

namespace MyWebsite.Application.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ActionType Action { get; set; } = ActionType.Get;
    }
}
