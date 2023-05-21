using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageReplacer.WindowsContextMenu
{
    public class APIResponse
    {
        public int Category { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Service { get; set; }
        public int Author { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
    }
}
