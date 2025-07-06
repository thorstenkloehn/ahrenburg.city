using System;
using System.ComponentModel.DataAnnotations;
using Markdig;

namespace ahrenburg.city.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Markdown-Content als HTML gerendert
        public string ContentHtml => Markdown.ToHtml(Content ?? "");
    }
}
