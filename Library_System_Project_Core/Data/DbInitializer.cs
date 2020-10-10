using Library_System_Project_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_System_Project_Core.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Faculty.
            if (context.Faculty.Any())
            {
                return;   // DB has been seeded
            }

            var faculty = new Faculty[]
            {
            new Faculty{Name="Faculty of Arts", Description="Established in 1995, the Faculty of Arts, one of the largest faculties of the university, consists of Eleven Departments."},
            new Faculty{Name="Faculty of Science",Description="SR University opened its doors to the students on the 1st July 1995 with only three faculties, namely the Faculty of Science, Faculty of Arts and the Faculty of Law. The Faculty of Science started its journey with only three departments namely, Physics, Mathematics and Chemistry."},
            new Faculty{Name="Faculty of Business Studies",Description="As you are reading this message, I assume that you are probably new to the University of SR and want to evaluate whether this is a place that will cater to all your requirements."},
            };

            foreach (Faculty f in faculty)
            {
                context.Faculty.Add(f);
            }
            context.SaveChanges();


            // Look for any Department.
            if (context.Department.Any())
            {
                return;   // DB has been seeded
            }

            var depertment = new Department[]
            {
                new Department{Name="Bangla", FacultyID = faculty.Single(f => f.Name == "Faculty of Arts").FacultyID},
                new Department{Name="Marketing", FacultyID = faculty.Single(f => f.Name == "Faculty of Business Studies").FacultyID },
                new Department{Name="English", FacultyID = faculty.Single(f => f.Name == "Faculty of Arts").FacultyID },
                new Department{Name="Finance", FacultyID = faculty.Single(f => f.Name == "Faculty of Business Studies").FacultyID },
                new Department{Name="Management", FacultyID = faculty.Single(f => f.Name == "Faculty of Business Studies").FacultyID },
                new Department{Name="Physics", FacultyID = faculty.Single(f => f.Name == "Faculty of Science").FacultyID },
                new Department{Name="Mathematics", FacultyID = faculty.Single(f => f.Name == "Faculty of Science").FacultyID },
                new Department{Name="Chemistry", FacultyID = faculty.Single(f => f.Name == "Faculty of Science").FacultyID },


            };
            foreach (Department d in depertment)
            {
                context.Department.Add(d);
            }
            context.SaveChanges();


            // Look for any Publisher.
            if (context.Publisher.Any())
            {
                return;   // DB has been seeded
            }

            var publisher = new Publisher[]
            {
                new Publisher{Name="Somoy Prokashon", Establish_Date= DateTime.Parse("1993-01-01"), Address="Polton, Dhaka" },
                new Publisher{Name="Agamee Prakshani", Establish_Date= DateTime.Parse("1990-10-01"), Address="Kalshi, Mirpur" },
                new Publisher{Name="Arbordale Publishing", Establish_Date= DateTime.Parse("1996-11-21"), Address="Vhulta, narayangang" },
                new Publisher{Name="Arcadia Publishing", Establish_Date= DateTime.Parse("1972-02-17"), Address="Gulshan, Dhaka" },
                new Publisher{Name="Arbor House", Establish_Date= DateTime.Parse("2005-08-23"), Address="Elephiend Road, Dhaka" },
            };
            foreach (Publisher p in publisher)
            {
                context.Publisher.Add(p);
            }
            context.SaveChanges();



            // Look for any Author.
            if (context.Author.Any())
            {
                return;   // DB has been seeded
            }

            var author = new Author[]
            {
                new Author{FirstName="G.B.", LastName="Shaw", Date_Of_Birth = DateTime.Parse("1856-07-26"), ImageUrl="~/Images/Authors/G.B.Shaw.jpg", Description="George Bernard Shaw, known at his insistence simply as Bernard Shaw, was an Irish playwright, critic, polemicist and political activist. His influence on Western theatre, culture and politics extended from the 1880s to his death and beyond." },
                new Author{FirstName="William", LastName="Shakespere", Date_Of_Birth = DateTime.Parse("1564-04-28"), ImageUrl="~/Images/Authors/William Shakespere.jpg", Description="'William Shakespeare was an English poet, playwright, and actor, widely regarded as the greatest writer in the English language and the world’s greatest dramatist.He is often called England’s national poet and the Bard of Avon." },
                new Author{FirstName="Rabindranath", LastName="Tagor", Date_Of_Birth = DateTime.Parse("1861-05-07"), ImageUrl="~/Images/Authors/Rabindranath Tagor.Jpg", Description="Rabindranath Tagore FRAS, also known by his pen name Bhanu Singha Thakur, and also known by his sobriquets Gurudev, Kabiguru, and Biswakabi, was a polymath, poet, musician, and artist from the Indian subcontinent." },
                new Author{FirstName="Toni", LastName="Morrison", Date_Of_Birth = DateTime.Parse("1931-02-18"), ImageUrl="~/Images/Authors/Toni Morrison.jpg", Description= "Chloe Anthony Wofford Morrison, known as Toni Morrison, was an American novelist, essayist, book editor, and college professor. Her first novel, The Bluest Eye, was published in 1970. The critically acclaimed Song of Solomon brought her national attention and won the National Book Critics Circle Award." },
                new Author{FirstName="Kazi Nazrul", LastName="Islam", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Kazi Nazrul Islam.jpg", Description="Kazi Nazrul Islam was a Bengali poet, writer, musician, anti-colonial revolutionary from the Indian subcontinent; and the national poet of Bangladesh. Popularly known as Nazrul, he produced a large body of poetry and music with themes that included religious devotion and rebellion against oppression." },
                new Author{FirstName="jim", LastName="arnosky", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/jim arnosky.jpg", Description="Jim Arnosky, a renowned naturalist, is the author and illustrator of many books for children about animals and nature, including the popular Crinkleroot books.He lives in South Ryegate, Vermont." },
                new Author{FirstName="Linus", LastName="Pauling", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Linus Pauling.jpg", Description="Linus Carl Pauling was an American chemist, biochemist, chemical engineer, peace activist, author, and educator. He published more than 1,200 papers and books, of which about 850 dealt with scientific topics." },
                new Author{FirstName="Eugene", LastName=" F.Brigham", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Eugene F.Brigham.jpg", Description="Eugene F. Brigham. Director Emeritus. Public Utility Research Center. Professor Brigham is the founding director of the Public Utility Research Center" },
                new Author{FirstName="Jonathan", LastName=" Clayden", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Jonathan Clayden.jpg", Description="Jonathan Paul Clayden is a Professor of organic chemistry at the University of Bristol." },
                new Author{FirstName="Charles", LastName="E. Menifield", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Charles E. Menifield.jpg", Description="Charles E. Menifield is the dean of the School of Public Affairs and Administration at Rutgers University-Newark. He has published widely within the area of budgeting and financial management and health policy"},
                new Author{FirstName="Boldt", LastName=" Mike", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Boldt, Mike.jpg", Description="Mike Boldt is an author and illustrator living in the countryside of Alberta, Canada. He has been illustrating for children for the past 15 years.His most recent picture book with Scholastic Canada is the wildly fun picture book Je ne suis pas une grenouill." },
                new Author{FirstName="Charless", LastName="T. Harnogren", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Charles Thomas Horngren.jpeg", Description="Charles Thomas Horngren was an American accounting scholar and professor of accounting at Stanford University, known for his work in 'pioneering modern-day management accounting."},
                new Author{FirstName="Jerry", LastName="J. Keygandt", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Jerry J. Keygandt.jpg", Description="is Arthur Andersen Alumni Professor of Accounting at the University of Wisconsin-Madison He holds a Ph.D. in accounting from the University of Illinois. Articles by Professor Weygandt have appeared in the Accounting Review Journal of Accounting Research, Accounting Horizons,Journal of Accountancy, and other academic and professional journals."},
                new Author{FirstName="Ray", LastName="H. Garrison", Date_Of_Birth = DateTime.Parse("1899-05-25"), ImageUrl="~/Images/Authors/Ray H. Garrison.jpg", Description="Born: February 23, 1933 (age 86 years), Waverly, Illinois, United States"},

            };
            foreach (Author a in author)
            {
                context.Author.Add(a);
            }
            context.SaveChanges();



            // Look for any Student.
            if (context.Student.Any())
            {
                return;   // DB has been seeded
            }

            var student = new Student[]
            {
                new Student {FirstName="Sharmin", LastName="Akter", Date_Of_Birth= DateTime.Parse("1998-04-30"), Gender = Gender.Female, Phone_Number ="01688-626368", Email = "sa@gmail.com", ImageUrl="~/Images/Students/U.jpg",  Address = "Narayanganj", DepartmentID = depertment.Single(d => d.Name == "English").DepartmentID},
                new Student {FirstName="Sharmin", LastName="Rumpa", Date_Of_Birth= DateTime.Parse("2000-05-23"), Gender = Gender.Female, Phone_Number ="01688-626237", Email = "srr@gmail.com", ImageUrl="~/Images/Students/Y.jpg", Address = "Dhaka", DepartmentID = depertment.Single(d => d.Name == "Finance").DepartmentID},
                new Student {FirstName="Afrajul", LastName="Islam", Date_Of_Birth= DateTime.Parse("1999-08-23"), Gender = Gender.Male, Phone_Number ="01713-626368", Email = "af@gmail.com", ImageUrl="~/Images/Students/E.jpg",  Address = "Borisal", DepartmentID = depertment.Single(d => d.Name == "Bangla").DepartmentID},
                new Student {FirstName="Akram", LastName="Hossain", Date_Of_Birth= DateTime.Parse("1996-06-25"), Gender = Gender.Male, Phone_Number ="01688-656368", Email = "ak@gmail.com", ImageUrl="~/Images/Students/F.jpg",  Address = "Comilla", DepartmentID = depertment.Single(d => d.Name == "Marketing").DepartmentID},
                new Student {FirstName="Obaidul", LastName="Limon", Date_Of_Birth= DateTime.Parse("2001-11-07"), Gender = Gender.Male, Phone_Number ="01677-626368", Email = "ob@gmail.com", ImageUrl="~/Images/Students/G.jpg",  Address = "Polton ", DepartmentID = depertment.Single(d => d.Name == "Physics").DepartmentID},
                new Student {FirstName="Israt", LastName="Jahan", Date_Of_Birth= DateTime.Parse("1997-10-20"), Gender = Gender.Female, Phone_Number ="01918-626368", Email = "is@gmail.com", ImageUrl="~/Images/Students/W.jpg",  Address = "Mirpor", DepartmentID = depertment.Single(d => d.Name == "Chemistry").DepartmentID},
                new Student {FirstName="Zifadul", LastName="Islam", Date_Of_Birth= DateTime.Parse("1998-12-16"), Gender = Gender.Male, Phone_Number ="01718-626366", Email = "zi@gmail.com", ImageUrl="~/Images/Students/P.jpg",  Address = "Narayanganj", DepartmentID = depertment.Single(d => d.Name == "English").DepartmentID},
                new Student {FirstName="Rakibul", LastName="Islam", Date_Of_Birth= DateTime.Parse("2000-09-29"), Gender = Gender.Male, Phone_Number ="01748-626168", Email = "ri@gmail.com", ImageUrl="~/Images/Students/S.jpg",  Address = "Dhaka", DepartmentID = depertment.Single(d => d.Name == "Management").DepartmentID},


            };

            foreach (Student s in student)
            {
                context.Student.Add(s);
            }
            context.SaveChanges();



            // Look for any Teacher.
            if (context.Teacher.Any())
            {
                return;   // DB has been seeded
            }

            var teacher = new Teacher[]
            {
                new Teacher {FirstName="Sharmin", LastName="Akter", Date_Of_Birth= DateTime.Parse("1993-05-01"), Gender = Gender.Female, Phone_Number ="01688-626368", Email = "sa@gmail.com", ImageUrl="~/Images/Teachers/X.jpg",  Address = "Narayanganj", Joning_Date= DateTime.Parse("1993-01-01")},
                new Teacher {FirstName="Md", LastName="Rakibul Islam", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "ri@gmail.com", ImageUrl="~/Images/Teachers/I.jpg",  Address = "Dhaka", Joning_Date= DateTime.Parse("1993-01-01")},
                new Teacher {FirstName="Anisur", LastName="Rahman", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "ar@gmail.com", ImageUrl="~/Images/Teachers/J.jpg",  Address = "Polton", Joning_Date= DateTime.Parse("1993-01-01")},
                new Teacher {FirstName="Md", LastName="Jahirul Islam", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "jr@gmail.com", ImageUrl="~/Images/Teachers/K.jpg",  Address = "Mirpor", Joning_Date= DateTime.Parse("1993-01-01")},
                new Teacher {FirstName="Afrajul Islam", LastName="Tanvir", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "at@gmail.com", ImageUrl="~/Images/Teachers/L.jpg",  Address = "Barisal", Joning_Date= DateTime.Parse("1993-01-01")},

            };

            foreach (Teacher t in teacher)
            {
                context.Teacher.Add(t);
            }
            context.SaveChanges();



            // Look for any Staff.
            if (context.Staff.Any())
            {
                return;   // DB has been seeded
            }

            var staff = new Staff[]
            {
                new Staff {FirstName="Sharmin", LastName="Akter", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Female, Phone_Number ="01688-626368", Email = "sa@gmail.com", ImageUrl="~/Images/Staff/C.jpg",  Address = "Narayanganj", Joning_Date= DateTime.Parse("1993-01-01")},
                 new Staff {FirstName="Sharmin", LastName="Akter", Date_Of_Birth= DateTime.Parse("1993-05-01"), Gender = Gender.Female, Phone_Number ="01688-626368", Email = "sa@gmail.com", ImageUrl="~/Images/Staff/T.jpg", Address = "Narayanganj", Joning_Date= DateTime.Parse("1993-01-01")},
                new Staff {FirstName="Md", LastName="Rakibul Islam", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "ri@gmail.com", ImageUrl="~/Images/Staff/M.jpg", Address = "Dhaka", Joning_Date= DateTime.Parse("1993-01-01")},
                new Staff {FirstName="Anisur", LastName="Rahman", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "ar@gmail.com", ImageUrl="~/Images/Staff/N.jpg",  Address = "Polton", Joning_Date= DateTime.Parse("1993-01-01")},
                new Staff {FirstName="Md", LastName="Jahirul Islam", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "jr@gmail.com", ImageUrl="~/Images/Staff/O.jpg", Address = "Mirpor", Joning_Date= DateTime.Parse("1993-01-01")},
                new Staff {FirstName="Afrajul Islam", LastName="Tanvir", Date_Of_Birth= DateTime.Parse("1993-01-01"), Gender = Gender.Male, Phone_Number ="01688-626368", Email = "at@gmail.com", ImageUrl="~/Images/Staff/R.jpg", Address = "Barisal", Joning_Date= DateTime.Parse("1993-01-01")},


            };

            foreach (Staff st in staff)
            {
                context.Staff.Add(st);
            }
            context.SaveChanges();


            // Look for any BooksType.
            if (context.BooksType.Any())
            {
                return;   // DB has been seeded
            }

            var booksType = new BooksType[]
            {
                new BooksType {Name ="Action and Adventure"},
                new BooksType {Name ="Classics"},
                new BooksType {Name ="Detective and Mystery"},
                new BooksType {Name ="Fantasy"},
                new BooksType {Name ="Horror"},
                new BooksType {Name ="Romance"},
                new BooksType {Name ="Science Fiction"},

            };

            foreach (BooksType t in booksType)
            {
                context.BooksType.Add(t);
            }
            context.SaveChanges();



            // Look for any Book.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }

            var book = new Book[]
            {
                new Book {Title="The Bluest Eye", ISBN="O-671-854216", NoOfPage= 200, ImageUrl="~/Images/Books/The Bluest Eye.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 1).AuthorID, DepartmentID = depertment.Single(d => d.Name == "English").DepartmentID, PublisherID = publisher.Single(d => d.Name == "Somoy Prokashon").PublisherID },
                new Book {Title="Me Before You", ISBN="O-14-0125867", NoOfPage= 200, ImageUrl="~/Images/Books/cover_me_before_you_mti.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true,AuthorID = author.Single(a => a.AuthorID == 5).AuthorID, BooksTypeID = booksType.Single(d => d.Name == "Romance").BooksTypeID, PublisherID = publisher.Single(d => d.Name == "Arcadia Publishing").PublisherID },
                new Book {Title="The Cellar", ISBN="O-14-0125867", NoOfPage= 200, ImageUrl="~/Images/Books/Cellar.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = false, AuthorID = author.Single(a => a.AuthorID == 1).AuthorID, BooksTypeID = booksType.Single(d => d.Name == "Horror").BooksTypeID, PublisherID = publisher.Single(d => d.Name == "Arbordale Publishing").PublisherID },
                new Book {Title="Financial Management", ISBN="O-12535-55", NoOfPage= 250, ImageUrl="~/Images/Books/Financial Management.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 2).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Management").DepartmentID,  PublisherID = publisher.Single(d => d.Name == "Agamee Prakshani").PublisherID },
                new Book {Title="Epic Content Marketing", ISBN="E-1269877", NoOfPage= 492, ImageUrl="~/Images/Books/Basic Principal of Marketing.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 1).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Marketing").DepartmentID, PublisherID = publisher.Single(d => d.Name == "Arbordale Publishing").PublisherID },
                new Book {Title="Organic Chemistry", ISBN="258-1585-84", NoOfPage= 177, ImageUrl="~/Images/Books/Organic Chemistry.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = false, AuthorID = author.Single(a => a.AuthorID == 4).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Chemistry").DepartmentID,  PublisherID = publisher.Single(d => d.Name == "Arcadia Publishing").PublisherID },
                new Book {Title="Mrityukshuda", ISBN="O-688-1433-4", NoOfPage= 235, ImageUrl="~/Images/Books/Mrityukshuda.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 5).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Bangla").DepartmentID, PublisherID = publisher.Single(d => d.Name == "Arbor House").PublisherID },
                new Book {Title="Agni Bani", ISBN="O-14-0125867", NoOfPage= 367, ImageUrl="~/Images/Books/Agni Bani.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 5).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Bangla").DepartmentID,  PublisherID = publisher.Single(d => d.Name == "Somoy Prokashon").PublisherID },
                new Book {Title="Principal of Management", ISBN="475-5686-5446", NoOfPage= 583, ImageUrl="~/Images/Books/Principal of Management.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 6).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Management").DepartmentID,  PublisherID = publisher.Single(d => d.Name == "Arcadia Publishing").PublisherID },
                new Book {Title="General Chemistry", ISBN="123-35353-62", NoOfPage= 426, ImageUrl="~/Images/Books/General Chemistry.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = false, AuthorID = author.Single(a => a.AuthorID == 5).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Chemistry").DepartmentID,  PublisherID = publisher.Single(d => d.Name == "Agamee Prakshani").PublisherID },
                new Book {Title="English Reading Skils", ISBN="O-14-0125867", NoOfPage= 119, ImageUrl="~/Images/Books/English Reading Skils.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 1).AuthorID, DepartmentID = depertment.Single(d => d.Name == "English").DepartmentID, PublisherID = publisher.Single(d => d.Name == "Somoy Prokashon").PublisherID },
                new Book {Title="Theretical concept in physics", ISBN="1255-36598-89", NoOfPage= 209, ImageUrl="~/Images/Books/Theretical concept in physics.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 1).AuthorID, DepartmentID = depertment.Single(d => d.Name == "Physics").DepartmentID, PublisherID = publisher.Single(d => d.Name == "Arbor House").PublisherID },
                new Book {Title="A Lady Most Lovely", ISBN="O-14-0125867", NoOfPage= 200, ImageUrl="~/Images/Books/Delamere A LadyMostLovely.JPG", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 3).AuthorID,  BooksTypeID = booksType.Single(d => d.Name == "Romance").BooksTypeID, PublisherID = publisher.Single(d => d.Name == "Arbor House").PublisherID },
                new Book {Title="Asylum", ISBN="O-14-0125867", NoOfPage= 200, ImageUrl="~/Images/Books/3c41f6f3040e88a3c4ef51c70c130511.jpg", Publish_Year = DateTime.Parse("1993-01-01"), IsAvailableForBorrow = true, AuthorID = author.Single(a => a.AuthorID == 2).AuthorID, BooksTypeID = booksType.Single(d => d.Name == "Horror").BooksTypeID, PublisherID = publisher.Single(d => d.Name == "Agamee Prakshani").PublisherID }




            };

            foreach (Book b in book)
            {
                context.Book.Add(b);
            }
            context.SaveChanges();



            // Look for any Book.
            if (context.IssueingBookDetails.Any())
            {
                return;   // DB has been seeded
            }

            var issueingBook = new IssueingBookDetails[]
            {
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 1).BookID, StudentID = student.Single(s => s.StudentID == 1).StudentID,   Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01"), Actual_Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 2).BookID, TeacherID = teacher.Single(s => s.TeacherID == 1).TeacherID,  Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01"), Actual_Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 3).BookID,  StaffID = staff.Single(s => s.StaffID == 4).StaffID, Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 4).BookID, StudentID = student.Single(s => s.StudentID == 2).StudentID,   Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 5).BookID, TeacherID = teacher.Single(s => s.TeacherID == 2).TeacherID,  Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01"), Actual_Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 6).BookID,  StaffID = staff.Single(s => s.StaffID == 3).StaffID, Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 1).BookID, StudentID = student.Single(s => s.StudentID == 4).StudentID,   Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 2).BookID, TeacherID = teacher.Single(s => s.TeacherID == 3).TeacherID,  Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-10"), Actual_Return_Date = DateTime.Parse("1993-01-07")},
                new IssueingBookDetails { BookID = book.Single(b => b.BookID == 4).BookID,  StaffID = staff.Single(s => s.StaffID == 1).StaffID, Borrow_Date = DateTime.Parse("1993-01-01"), Return_Date = DateTime.Parse("1993-01-01")},


            };

            foreach (IssueingBookDetails i in issueingBook)
            {
                context.IssueingBookDetails.Add(i);
            }
            context.SaveChanges();




        }
    }
}
