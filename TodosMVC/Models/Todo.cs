using System.ComponentModel.DataAnnotations;

namespace TodosMVC.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; } //khóa chính
        public string Title { get; set; } //Tiêu đề
        public string? Description { get; set; }//Mô tả

        public bool Status { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } //ngày tạo
    }
}
