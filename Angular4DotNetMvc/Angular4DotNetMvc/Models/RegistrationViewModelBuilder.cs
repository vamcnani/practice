using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Angular4DotNetMvc.Models
{
    public class RegistrationViewModelBuilder
    {    

        public CourseViewModel[] GetCourseViewModels()
        {
            var courses = new[]
            {
                new CourseViewModel{ Number="CF900", Name="Introduction to SPA", Instructor="Jason Smith"},
                new CourseViewModel{Number="FS9000",Name="Angular Js",Instructor="Joe Eames"},
                new CourseViewModel{Number="HLKJ990", Name="Candlestick Analysis", Instructor="Steve Nison"},
                new CourseViewModel{Number="KL993", Name="Pivot point Analysis", Instructor="Roger Federer"},
                new CourseViewModel{Number="KLK83293", Name="Risk Analysis", Instructor="Vamsi Jyoshna"}
            };

            return courses;

        }

        public InstructorViewModel[] GetInstructorViewModel()
        {
            var instructors = new[]
            {
                new InstructorViewModel{ RoomNumber =86, Name="Vamsi", Email="vamc_nani@hotmail.com"},
                new InstructorViewModel{ RoomNumber =748, Name="Jyoshna", Email="jo@hotmail.com"},
                new InstructorViewModel{ RoomNumber =44, Name="Raja", Email="raj@hotmail.com"},
                new InstructorViewModel{ RoomNumber =854, Name="Lokesh", Email="loks@hotmail.com"},
                new InstructorViewModel{ RoomNumber =43, Name="Sam", Email="sam@hotmail.com"}
            };

            return instructors;

        }
    }
}