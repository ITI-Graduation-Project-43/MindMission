﻿using Microsoft.AspNetCore.Mvc;

namespace MindMission.API.Utilities
{
    public class ResonposeObjectL<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int ItemNumberPerPages { get; set; }
        public int TotalPages { get; set; }

        public void ReturnedResponse (bool _Success, string _Message, List<T> _Items, int _PageNumber, int _ItemNumberPerPages, int _TotalPages)
        {
            Success = _Success;
            Message = _Message;
            Items = _Items;
            PageNumber = _PageNumber;
            ItemNumberPerPages = _ItemNumberPerPages;
            TotalPages = _TotalPages;
        }

    }
}
