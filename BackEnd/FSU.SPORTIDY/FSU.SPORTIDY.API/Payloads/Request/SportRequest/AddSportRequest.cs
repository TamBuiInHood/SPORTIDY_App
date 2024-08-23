﻿using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.SportRequest
{
    public class AddSportRequest
    {
        [Required]
        public string? SportName { get; set; }
        [Required]
        public string? SportImage { get; set; }
    }
}