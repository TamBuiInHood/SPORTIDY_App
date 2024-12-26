using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class UpdateCommentRequest
    {
        [Required]
        public int commentId {  get; set; }
        public string? content { get; set; }

        [FileFormat(".jpg", ".jpeg", ".png", ".pdf", ErrorMessage = "Please upload a valid file format (.jpg, .jpeg, .png, .pdf).")]
        public IFormFile? image { get; set; }
    }
}
