using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_System_Project_Core.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Faculty name cannot be longer than 200 characters.")]
        [Display(Name = "Faculty Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Faculty Description")]
        public string Description { get; set; }

        
    }


    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Department name cannot be longer than 200 characters.")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        public int FacultyID { get; set; }

        public virtual Faculty Faculties { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Student> Students { get; set; }



    }



    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be 5 charecter & cannot be longer than 50 characters.")]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime Date_Of_Birth { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Author Description")]
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }



    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Publication name cannot be longer than 100 characters.")]
        [Display(Name = "Publication Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Establish Date")]
        public DateTime Establish_Date { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Publication address cannot be longer than 500 characters.")]
        [Display(Name = "Publication Address")]
        public string Address { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }


    public enum Gender
    {
        Male, Female, Other
    }


    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime Date_Of_Birth { get; set; }


        [DisplayFormat(NullDisplayText = "No grade")]
        public Gender Gender { get; set; }

        [Required]
        public string Phone_Number { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public int DepartmentID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual Department Departments { get; set; }

        public virtual ICollection<IssueingBookDetails> IssueingBookDetailss { get; set; }


    }




    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime Date_Of_Birth { get; set; }


        [DisplayFormat(NullDisplayText = "No grade")]
        public Gender Gender { get; set; }

        [Required]
        public string Phone_Number { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joning Date")]
        public DateTime Joning_Date { get; set; }


        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual ICollection<IssueingBookDetails> IssueingBookDetailss { get; set; }


    }



    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime Date_Of_Birth { get; set; }


        [DisplayFormat(NullDisplayText = "No grade")]
        public Gender Gender { get; set; }

        [Required]
        public string Phone_Number { get; set; }

        [Required]
        public string Email { get; set; }


        [Required]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joning Date")]
        public DateTime Joning_Date { get; set; }


        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual ICollection<IssueingBookDetails> IssueingBookDetailss { get; set; }


    }

    public class BooksType
    {
        [Key]
        public int BooksTypeID { get; set; }

        [Required]
        [Display(Name = "Types of Books")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }


    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Book-Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Number Of Page")]
        public int NoOfPage { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish Year")]
        public DateTime Publish_Year { get; set; }

        [Display(Name = "Is Available For Borrow")]
        public bool IsAvailableForBorrow { get; set; }

        public int AuthorID { get; set; }

        public int? DepartmentID { get; set; }

        public int? BooksTypeID { get; set; }

        public int PublisherID { get; set; }

        public virtual Author Author { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Departments { get; set; }

        public virtual BooksType BookTypes { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<IssueingBookDetails> IssueingBookDetailss { get; set; }

    }


    public class IssueingBookDetails
    {
        [Key]
        public int SerialID { get; set; }

        public int BookID { get; set; }

        public int? StudentID { get; set; }

        public int? TeacherID { get; set; }

        public int? StaffID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Borrow Date")]
        public DateTime Borrow_Date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime Return_Date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Actual Return Date")]
        public DateTime? Actual_Return_Date { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Books { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Students { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher Teachers { get; set; }

        [ForeignKey("StaffID")]
        public virtual Staff Staffs { get; set; }

    }

}
