using Android.Graphics;
using System;

namespace InterviewApp.Models
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Text { get; set; } = "";

        public string Description { get; set; } = "";

        public string imagePath { get; set; } = "";
    }
}